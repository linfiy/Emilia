using UnityEngine;
using System.Collections;
using XLua;
public class CsCallLua : MonoBehaviour
{
  LuaEnv env = new LuaEnv();
  void Start()
  {
    // env.DoString("ShowTime.lua");
    env.DoString("require 'ShowTIme'");
  }

  void Update()
  {
    env.Tick();
  }
  void OnDestroy()
  {
    env.Dispose();
  }
}
