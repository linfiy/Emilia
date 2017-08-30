using UnityEngine;
using System.Collections;
using XLua;
using System;
using UniRx;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
  LuaEnv lua = new LuaEnv();
  void OnNext(long a){
    print("sssssss");
  }
  void Start()
  {
		lua.DoString("require('Timer')");
    gameObject.AddComponent<Text>();
    // gameObject.GetComponent()
    // Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(Observer.Create<long>(OnNext));
    // Observer.Create(_=>{});
    // Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe();
    
  }
  void CallBack(){

  }
  // Update is called once per frame
  void Update()
  {
    if (lua != null) lua.Tick();
  }

  void OnDestroy()
  {
		lua.Dispose();
  }
}
