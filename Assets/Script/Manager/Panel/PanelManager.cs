namespace Emilia {
  using UnityEngine;
  using System;
  using System.Collections.Generic;
  using XLua;
  using UObject = UnityEngine.Object;
  public class PanelManager: MonoBehaviour {

    static GameManager gm;
    GameObject canvas;
    Transform _parent;

    static readonly Dictionary<string, PanelBase> openingStore = new Dictionary<string, PanelBase>();
    static readonly Dictionary<PanelLayer, Transform> layerContainerStore = new Dictionary<PanelLayer, Transform>();
    Transform parent {
      get {
        if (_parent == null) {
          var go = GameObject.FindWithTag("GuiCamera");
          if (go != null) _parent = go.transform; 
        }

        return parent;
      }
    }

    public static PanelManager Initialize () {
      var ins = GameObject.Find("GameManager").AddComponent<PanelManager>();
      ins.canvas = GameObject.Find("Canvas");
      if (ins.canvas == null) throw new ApplicationException("[Panel] Canvas is null");
      
      foreach (PanelLayer layer in Enum.GetValues(typeof(PanelLayer))) {
        var layerName = layer.ToString();
        var container = ins.canvas.transform.FindChild(layerName);
        layerContainerStore.Add(layer, container);
      }
      
      return ins; 
    }

    public static void OpenPanel<T>(
      string panelName, string abName, LuaFunction success, LuaFunction error = null) 
    where T: LuaBehaviour
    {
      if (openingStore.ContainsKey(panelName)) return;

      GameManager.resMgr.LoadPrefab(abName, panelName + "Panel", (UnityEngine.Object[] objs) => {
        if (objs.Length > 0) {
          // 需要设置层级
          var panelObject = Instantiate(objs[0], GameObject.Find("Canvas").transform) as GameObject;
          panelObject.transform.localPosition = Vector3.zero;
          panelObject.transform.localScale = Vector3.one;

          var panel = panelObject.AddComponent<T>();
          success.Call(panel);
        }
        else {
          if (error != null) error.Call();
        }

        
      });
      // panel.Init(args);
      // panelStore.Add(name, panel);
      // skinPath = skinPath.Equals("") ? panel.skinPath : skinPath;
      // string assetName = skinPath + "Panel";
      // gm.resMgr.LoadPrefab(skinPath, new string[]{ assetName }, (UObject[] objs) => {
      //   if (objs.Length == 0) return;
      //   var prefab = objs[0] as GameObject;
      //   if (prefab == null) return;

      //   var go = Instantiate(prefab, layerContainerStore[panel.layer]) as GameObject;
      //   go.name = assetName;
      //   go.transform.localScale = Vector3.one;
      //   go.transform.localPosition = Vector3.zero;
      //   go.AddComponent<LuaBehaviour>();
      //   PanelBase panel = go.AddComponent<T>();
      //   if (func != null) func.Call(go);
      //   panel.skin = go;
      // });

      // var skin = Resources.Load<GameObject>(skinPath);
      // if (skin == null) throw new System.ApplicationException("[Panel] Open Panel Fail: without skin");

      // panel.skin = Instantiate(skin, layerContainerStore[panel.layer]) as GameObject;
      
      // panel.OnOpening();
      // panel.OnOpened();
    }

    public static void ClosePanel(string name) {
      var panel = openingStore.ContainsKey(name) ? openingStore[name] : null;
      if (panel == null) return;

      panel.OnClosing();
      openingStore.Remove(name);
      panel.OnClosed();
      GameObject.Destroy(panel.skin);
      Component.Destroy(panel);
      
    }
  }
}