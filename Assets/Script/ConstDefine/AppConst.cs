using UnityEngine;

public static class AppConst {

  // resource manager
  public const string ASSET_DIC = "1";
  public const string EXT_NAME = ".unity3d";
  public const int GameFrameRate = 30;
  public const string AssetDir = "StreamingAssets";           //素材目录 
  public const string WEB_URL = "http://192.168.0.101:3000/hotupdate/";


  public const string AppName = "Emilia";



  public const bool DebugMode = true;
  public const bool updateMode = true;



  // bundle use

  public static int BUNDLE_VERSION = 1;

  public static string SHOW_VERSION = "3.0.1";

  public static string STREAMING_PATH = Application.streamingAssetsPath + "/";

  public static string DATA_PATH = Application.dataPath + "/";

  public static string LOCAL_LUA_PATH = Application.dataPath + "/Lua/";

  public static string BUNDLE_FILE_PATH = Application.streamingAssetsPath + "/bundle_index.txt";

  

}