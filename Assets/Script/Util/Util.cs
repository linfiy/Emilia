namespace Util {
  using UnityEngine;
  public partial class Util {

    /// <summary>
    /// 取得数据存放目录
    /// </summary>
    public static string DataPath {
      get {
        string game = AppConst.AppName.ToLower();
        if (Application.isMobilePlatform) {
            return Application.persistentDataPath + "/" + game + "/";
        }
        if (AppConst.DebugMode) {
            return Application.dataPath + "/" + AppConst.AssetDir + "/";
        }
        if (Application.platform == RuntimePlatform.OSXEditor) {
            int i = Application.dataPath.LastIndexOf('/');
            return Application.dataPath.Substring(0, i + 1) + game + "/";
        }
        return "c:/" + game + "/";
      }
    }

    public static string GetRelativePath () {

      if (Application.isEditor)
          return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/") + "/Assets/" + AppConst.AssetDir + "/";
      else if (Application.isMobilePlatform || Application.isConsolePlatform)
          return "file:///" + DataPath;
      else // For standalone player.
          return "file://" + Application.streamingAssetsPath + "/";
    }
  }
}