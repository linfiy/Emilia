using UnityEngine;
using System.Collections;
using UniRx;
using System;
using XLua;
[LuaCallCSharp]
public class TimerCall
{
  public static void TimerFromScend(int seconds, LuaFunction func)
  {
    Observable.Timer(TimeSpan.FromSeconds(seconds)).Repeat()
    .Subscribe(_ =>
    {
      func.Call(null);
    });
  }
}
