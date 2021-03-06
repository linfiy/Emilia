﻿using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using ToolUtil = Util.Util;
using System.Text;

public class BundlePacker : Editor
{
  static List<string> dirsList = new List<string>();
  static List<string> filesList = new List<string>();
  static List<AssetBundleBuild> assetList = new List<AssetBundleBuild>();//存储打包资源的list
  static string dataPath
  {
    get { return Application.dataPath.ToLower(); }
  }
  /*android */
  [MenuItem("AssetsBundle/BuildAndroidAssets")]
  public static void BuildAndroidAssets()
  {
    BuildBundle(BuildTarget.Android);
  }
  /*ios */
  [MenuItem("AssetsBundle/BuildIPhoneAssets")]
  public static void BuildIPhoneAssets()
  {
    BuildBundle(BuildTarget.iOS);
  }
  /*windows */
  [MenuItem("AssetsBundle/BuildWindowsAssets")]
  public static void BuildWindowsAssets()
  {
    BuildBundle(BuildTarget.StandaloneWindows);
  }
  /*打包流程 */
  public static void BuildBundle(BuildTarget target)
  {
    Caching.CleanCache();
    string outputPath = "Assets/" + AppConst.AssetDir;
    var type = BuildAssetBundleOptions.None;
    string streamingPath = Application.streamingAssetsPath;
    if (Directory.Exists(streamingPath)) Directory.Delete(streamingPath, true);
    Directory.CreateDirectory(streamingPath);
    assetList.Clear();
    if (AppConst.LUA_BUNDLE_MODE)//将lua代码打包成为AssetBundle
    {
      HandleLuaBundle();
    }
    else//只将lua代码复制到文件夹下
    {
      HandleLuaCode();
    }
    // // 测试资源打包
    HandleExampleBundle();
    // 原来的代码(打包所有AssetBundle)
    // BuildPipeline.BuildAssetBundles(outputPath, type, target);
    //AssetBundleBuilder 文件路徑必須為Assets/ 开头的
    BuildPipeline.BuildAssetBundles(outputPath, assetList.ToArray(), type, target);
    /* 生成索引文件 */
    CreateBundleIndexFile();
    if (Directory.Exists(Application.dataPath + "/CopyLua/")) Directory.Delete(Application.dataPath + "/CopyLua/", true);
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
    dirsList.Clear(); filesList.Clear();
    GetDirAllFile(AppConst.STREAMING_PATH);
    var fs = new FileStream(AppConst.BUNDLE_FILE_PATH, FileMode.CreateNew);
    var sw = new StreamWriter(fs);
    for (int i = 0; i < filesList.Count; i++)
    {
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
  static void HandleLuaBundle()
  {
    string copyLuaPath = Application.dataPath + "/CopyLua/";
    if (!Directory.Exists(copyLuaPath)) Directory.CreateDirectory(copyLuaPath);
    string luaPath = Application.streamingAssetsPath + "/Lua/";
    if (!Directory.Exists(luaPath)) Directory.CreateDirectory(luaPath);
    string[] luaDirs = new string[] { Application.dataPath + "/Lua/" };//打包lua的目录
    foreach (string dir in luaDirs)
    {
      CopyLuaBytesFiles(dir, copyLuaPath);
    }
    // 将复制的.bytes 文件打包成AssetBundle对象，准备打包
    string[] path_lua_dirs = Directory.GetDirectories(copyLuaPath);
    foreach (string dir in path_lua_dirs)
    {
      string name = dir.Replace(copyLuaPath, "");
      name = name + AppConst.EXT_NAME;
      AddBuildMap("Lua/" + name, "*.bytes", "Assets" + dir.Replace(Application.dataPath, string.Empty));
    }
    AddBuildMap("Lua/Lua" + AppConst.EXT_NAME, "*.bytes", "Assets" + copyLuaPath.Replace(Application.dataPath, string.Empty));
    //非lua文件处理
    foreach (string path in luaDirs)
    {
      dirsList.Clear(); filesList.Clear();
      GetDirAllFile(path);
      foreach (string file in filesList)
      {
        if (file.EndsWith(".meta") || file.EndsWith(".lua")) continue;
        string name = file.Replace(path, string.Empty);
        string copyPath = copyLuaPath + name;
        string dir = Path.GetDirectoryName(copyPath);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        File.Copy(file, copyPath, true);
      }
    }
    AssetDatabase.Refresh();
  }
  public static void CopyLuaBytesFiles(string sourceDir, string destDir, bool appendext = true, string searchPattern = "*.lua", SearchOption option = SearchOption.AllDirectories)
  {
    //判断文件夹是否存在
    //获取文件目录下子文件
    //拼接目标文件路径，以.bytes结尾
    //复制文件
    if (!Directory.Exists(sourceDir)) return;
    string[] files = Directory.GetFiles(sourceDir, searchPattern, option);
    int len = sourceDir.Length;
    if (sourceDir[len - 1].Equals('/') || sourceDir.EndsWith("\\"))
    {
      len--;
    }
    foreach (string file in files)
    {
      string fileName = file.Remove(0, len);
      string copyPath = destDir + "/" + fileName;
      if (appendext) copyPath = copyPath + ".bytes";
      string copyDir = Path.GetDirectoryName(copyPath);
      Directory.CreateDirectory(copyDir);
      File.Copy(file, copyPath, true);
    }
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
  static void HandleExampleBundle()
  {
    string resPath = dataPath + "/" + AppConst.AssetDir + "/";
    if (!Directory.Exists(resPath)) Directory.CreateDirectory(resPath);
    AddBuildMap("testSaber" + AppConst.EXT_NAME, "*.prefab", "Assets/HotRes/Saber");
    AddBuildMap("testSaber" + AppConst.EXT_NAME, "*.png", "Assets/HotRes/Saber");
  }
  //将目录下的（目標）文件转化为AssetBundleBuilder对象
  static void AddBuildMap(string bundleName, string pattern, string path)
  {
    string[] files = Directory.GetFiles(path, pattern);
    if (files.Length == 0) return;
    for (int i = 0; i < files.Length; i++)
    {
      files[i] = files[i].Replace('\\', '/');
    }
    AssetBundleBuild build = new AssetBundleBuild();
    build.assetBundleName = bundleName;
    build.assetNames = files;
    assetList.Add(build);
  }
  [MenuItem("AssetsBundle/UpLoadAssets")]
  static void UpLoad()
  {
    CopyFolder(AppConst.STREAMING_PATH, @"D:/Tocar/_TOOL/light-server/hotupdate/");
  }
  static void CopyFolder(string srcPath, string tarPath)
  {
    if (!Directory.Exists(srcPath))
    {
      Directory.CreateDirectory(srcPath);
    }
    if (!Directory.Exists(tarPath))
    {
      Directory.CreateDirectory(tarPath);
    }
    CopyFile(srcPath, tarPath);
    string[] directionName = Directory.GetDirectories(srcPath);
    foreach (string dirPath in directionName)
    {
      string directionPathTemp = tarPath + "\\" + dirPath.Substring(srcPath.Length);
      CopyFolder(dirPath, directionPathTemp);
    }
  }
  static void CopyFile(string srcPath, string tarPath)
  {
    string[] filesList = Directory.GetFiles(srcPath);
    foreach (string f in filesList)
    {
      string fTarPath = tarPath + "\\" + f.Substring(srcPath.Length);
      if (File.Exists(fTarPath))
      {
        File.Copy(f, fTarPath, true);
      }
      else
      {
        File.Copy(f, fTarPath);
      }
    }
  }

  /// <summary>
  /// 编码LUA文件用UTF-8
  /// </summary>
  [MenuItem("Lua/Encode LuaFile with UTF-8", false, 50)]
  public static void EncodeLuaFile()
  {
    string path = Application.dataPath + "/Lua";
    string[] files = Directory.GetFiles(path, "*.lua", SearchOption.AllDirectories);
    foreach (string f in files)
    {
      string file = f.Replace('\\', '/');

      string content = File.ReadAllText(file);
      using (var sw = new StreamWriter(file, false, new UTF8Encoding(false)))
      {
        sw.Write(content);
      }
      Debug.Log("Encode file::>>" + file + " OK!");
    }
  }
}
