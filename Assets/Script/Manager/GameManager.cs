namespace Emilia {
  using UnityEngine;
  using UnityEngine.UI;

  public class GameManager: MonoBehaviour {
    public ResourceManager resMgr;

    public static GameManager instance;
    void Awake () {
      instance = this;

      DontDestroyOnLoad(gameObject);  //防止销毁自己

            // CheckExtractResource(); //释放资源
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
      Application.targetFrameRate = AppConst.GameFrameRate;
      
      resMgr = gameObject.AddComponent<ResourceManager>();
      OnResourceInited();
    }


    void OnInitialize () {
      resMgr.LoadPrefab("pic_bundle", new string[]{ "saber" }, (UnityEngine.Object[] objs) => {
        if (objs.Length > 0) {
          print(objs[0]);
          Image go = GameObject.Find("ImageSaber").GetComponent<Image>();
          go.sprite = objs[0] as Sprite;
        }
      });
    }


    void OnResourceInited() {
      resMgr.Initialize(AppConst.AssetDir, () => {
          Debug.Log("Initialize OK!!!");
          this.OnInitialize();
      });
    }
} 
}