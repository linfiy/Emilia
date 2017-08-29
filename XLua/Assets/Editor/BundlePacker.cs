using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;
public class BundlePacker : Editor
{
  static List<string> dirsList = new List<string>();
  static List<string> filesList = new List<string>();
  static string DataPath = Application.dataPath;
  [MenuItem("AssetsBundle/BuildAssets")]
  public static void BuildBundle()
  {
    HandleLuaCode();
    BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
  }
  [MenuItem("AssetsBundle/CreateBundleFile")]
  public static void CreateBundleFile()
  {
    if (File.Exists(AppConst.BUNDLE_FILE_PATH)) File.Delete(AppConst.BUNDLE_FILE_PATH);

    dirsList.Clear(); filesList.Clear();
    GetDirAllFile(AppConst.STREAMING_PATH);

    FileStream fs = new FileStream(AppConst.BUNDLE_FILE_PATH, FileMode.CreateNew);
    StreamWriter sw = new StreamWriter(fs);
    BundleFile bundle = new BundleFile();
    bundle.bundle_version = AppConst.BUNDLE_VERSION;
    bundle.show_version = AppConst.SHOW_VERSION;
    for (int i = 0; i < filesList.Count; i++) {
        string file = filesList[i];
        string ext = Path.GetExtension(file);
        if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

        string md5 = LuaTools.MD5(file);
        string key = file.Replace(AppConst.STREAMING_PATH, string.Empty);
        bundle.file_info.Add(key,md5);
    }
    sw.Write(JsonMapper.ToJson(bundle));
    sw.Close(); fs.Close();
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
}
