using UnityEngine;
using System.Collections;
using XLua;
using System.IO;
using System.Collections.Generic;
[Hotfix]
public class LuaManager : MonoBehaviour
{
  public int time = 1;
  Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();
  LuaEnv env;
  void Awake()
  {
    env = new LuaEnv();
    env.AddLoader(new LuaEnv.CustomLoader((ref string file) =>
    {
      file = Application.streamingAssetsPath + "/Lua/Resources/" + file + ".lua";
      string text = File.ReadAllText(file);
      if (text != null) return System.Text.Encoding.UTF8.GetBytes(text);
      return null;
    }));
  }
  void Start()
  {

  }
  void Update()
  {
    // print("C#"+(time++));
    env.Tick();
  }

  void OnGUI()
  {
    if (GUI.Button(new Rect(0, 0, 400, 200), "热更新"))
    {
      env.DoString("require 'ShowTime'");
    }
  }
}
