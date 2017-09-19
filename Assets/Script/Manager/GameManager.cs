namespace Emilia {

  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;
  using UniRx;
  public partial class GameManager: MonoBehaviour {

    public ResourceManager resMgr;



    public static GameManager instance;

    void Awake () {

      instance = this;



      DontDestroyOnLoad(gameObject);  //防止销毁自己



            // CheckExtractResource(); //释放资源

      Screen.sleepTimeout = SleepTimeout.NeverSleep;

      Application.targetFrameRate = AppConst.GameFrameRate;

      

      
      CheckHotResource()
      .Subscribe(_ => {
        // 检查更新成功
        GameObject.Find("Lua").AddComponent<LuaManager>();
        OnResourceInited();
      });
      

    }
    

    void OnInitialize () {

      resMgr.LoadPrefab("testsaber.unity3d", new string[]{ "SaberPanel" }, (UnityEngine.Object[] objs) => {
        if (objs.Length > 0) {

          print(objs[0]);
          var saberImageObject = Instantiate(objs[0], GameObject.Find("Canvas").transform) as GameObject;
          var saberRect = saberImageObject.GetComponent<RectTransform>();
          saberRect.anchoredPosition = Vector2.zero;
          saberImageObject.transform.localScale = Vector3.one;
          saberRect.sizeDelta = new Vector2(1920, 1080);
        }
      });

    }

    void OnResourceInited() {
      resMgr = gameObject.AddComponent<ResourceManager>();
      resMgr.Initialize(AppConst.AssetDir, () => {

          Debug.Log("Initialize OK!!!");

          this.OnInitialize();

      });

    }

} 

}