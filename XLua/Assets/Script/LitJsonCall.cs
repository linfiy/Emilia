using UnityEngine;
using System.Collections;
using LitJson;
using System;
using XLua;
using System.Text;
[LuaCallCSharp]
public class LitJsonCall : MonoBehaviour
{
	public static string json = "[0,2,4]";
  LuaEnv env = new LuaEnv();
  void Start()
  {
		
		// TextAsset txt = Resources.Load<TextAsset>("mahjong_data_feng");
		// json = txt.text;
		// Debug.LogError(LitJsonCall.JsonGetString(json));
    env.DoString(@"
		a = CS.LitJsonCall.JsonGetString(CS.LitJsonCall.json)
		");
  }
  public static int[] JsonGetString(string json)
  {
		Debug.LogWarning("!!!!!!!!!");
    return JsonMapper.ToObject<int[]>(json);
  }

  void Update()
  {
    if (env != null)
      env.Tick();
  }
	void OnDestroy()
	{
			// env.Dispose();
			env.Dispose();
	}
}
