using UnityEngine;
using XLua;
using System.IO;
using System.Collections.Generic;
public class LuaManager : MonoBehaviour
{
  public int time = 1;
  Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();
  LuaEnv env;
  void Awake()
  {
    env = new LuaEnv();
    env.AddLoader(new LuaEnv.CustomLoader(LuaFileLoader));
  }
  byte[] LuaFileLoader(ref string path)
  {
    string file = Application.streamingAssetsPath + "/Lua/" + path + ".lua";
    string text = File.ReadAllText(file);
    if (text != null) return System.Text.Encoding.UTF8.GetBytes(text);
    return null;
  }
  void Update()
  {
    env.Tick();
  }

  void OnDestroy()
  {
    if (env != null)
      env.Dispose();
  }
  public void DoLuaScript(string file)
  {
    env.DoString(file);
  }
}
