using UnityEngine;

public static class AppConst
{

  // resource manager
  public const string ASSET_DIC = "1";
  public const string EXT_NAME = ".unity3d";
  public const int GameFrameRate = 30;
  public const string AssetDir = "StreamingAssets";           //素材目录 
  public const string WEB_URL = "http://192.168.1.254:3000/hotupdate/";


  public const string AppName = "Emilia";



  public const bool DebugMode = true;

  // 开启之后需要自行配置 WEB_URL 及其地址包含的文件
  public const bool updateMode = true;   



  // bundle use

  public static int BUNDLE_VERSION = 1;

  public static string SHOW_VERSION = "3.0.1";

  public static string STREAMING_PATH = Application.streamingAssetsPath + "/";

  public static string DATA_PATH = Application.dataPath + "/";

  public static string LOCAL_LUA_PATH = Application.dataPath + "/Lua/";

  public static string BUNDLE_FILE_PATH = Application.streamingAssetsPath + "/bundle_index.txt";

  public static bool LUA_BUNDLE_MODE = true;


}