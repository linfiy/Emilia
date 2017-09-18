using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using ToolUtil = Util.Util;

public class BundlePacker : Editor
{
  static List<string> dirsList = new List<string>();
  static List<string> filesList = new List<string>();

  static string dataPath {
    get { return Application.dataPath.ToLower(); }
  }

  /*********************************************************\
  
  \*********************************************************/
  [MenuItem("AssetsBundle/BuildAssets")]
  public static void BuildBundle()
  {
    Caching.CleanCache();

    string outputPath = "Assets/" + AppConst.AssetDir;
    var type = BuildAssetBundleOptions.UncompressedAssetBundle;
    var targetPlatform = BuildTarget.StandaloneWindows;
    HandleLuaCode();
    // 原来的代码
    // BuildPipeline.BuildAssetBundles(outputPath, type, targetPlatform);
    // 测试资源打包
    HandleExampleBundle();
    BuildPipeline.BuildAssetBundles(outputPath, maps.ToArray(), type, targetPlatform);
    /* 生成索引文件 */
    CreateBundleIndexFile();
    AssetDatabase.Refresh();
  }
  [MenuItem("AssetsBundle/DeleteAssets")]
  public static void DeleteBundle()
  {
    if (Directory.Exists(AppConst.STREAMING_PATH)) Directory.Delete(AppConst.STREAMING_PATH, true);
    Caching.CleanCache();
    AssetDatabase.Refresh();
  }

  // [MenuItem("AssetsBundle/CreateBundleFile")]
  public static void CreateBundleIndexFile()
  {
    if (File.Exists(AppConst.BUNDLE_FILE_PATH)) File.Delete(AppConst.BUNDLE_FILE_PATH);

    dirsList.Clear(); filesList.Clear();
    GetDirAllFile(AppConst.STREAMING_PATH);

    var fs = new FileStream(AppConst.BUNDLE_FILE_PATH, FileMode.CreateNew);
    var sw = new StreamWriter(fs);

    for (int i = 0; i < filesList.Count; i++) {
        string file = filesList[i];
        string ext = Path.GetExtension(file);
        if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

        string md5 = ToolUtil.MD5(file);
        string key = file.Replace(AppConst.STREAMING_PATH, string.Empty);
        sw.WriteLine(md5 + "|" + key);
    }
    sw.Close(); 
    fs.Close();
  }
  static void HandleLuaCode()
  {
    if (Directory.Exists(AppConst.STREAMING_PATH)) Directory.Delete(AppConst.STREAMING_PATH, true);
    Directory.CreateDirectory(AppConst.STREAMING_PATH);
    dirsList.Clear(); filesList.Clear();
    GetDirAllFile(AppConst.LOCAL_LUA_PATH);
    foreach (string file in filesList)
    {
      if (Path.GetExtension(file).Equals(".meta")) continue;
      string name = file.Replace(Application.dataPath, "");
      string newPath = AppConst.STREAMING_PATH + name;
      string dir = Path.GetDirectoryName(newPath);
      if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
      File.Copy(file, newPath);
    }
  }
  //处理子类文件
  static void GetDirAllFile(string dirPath)
  {
    string[] dirs = Directory.GetDirectories(dirPath);
    string[] files = Directory.GetFiles(dirPath);
    foreach (string file in files)
    {
      if (file.EndsWith(".meta")) continue;
      filesList.Add(file.Replace('\\', '/'));
    }
    foreach (string dir in dirs)
    {
      dirsList.Add(dir.Replace('\\', '/'));
      GetDirAllFile(dir);
    }
  }

  /// <summary>
  /// 处理框架实例包
  /// </summary>
  static void HandleExampleBundle() {
    string resPath = dataPath + "/" + AppConst.AssetDir + "/";
    if (!Directory.Exists(resPath)) Directory.CreateDirectory(resPath);

    AddBuildMap("testSaber" + AppConst.EXT_NAME, "*.prefab", "Assets/HotRes/Saber");
    AddBuildMap("testSaber" + AppConst.EXT_NAME, "*.png", "Assets/HotRes/Saber");
}

  static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();
  static void AddBuildMap(string bundleName, string pattern, string path) {
    string[] files = Directory.GetFiles(path, pattern);
    if (files.Length == 0) return;

    for (int i = 0; i < files.Length; i++) {
        files[i] = files[i].Replace('\\', '/');
    }
    AssetBundleBuild build = new AssetBundleBuild();
    build.assetBundleName = bundleName;
    build.assetNames = files;
    maps.Add(build);
  }

}
