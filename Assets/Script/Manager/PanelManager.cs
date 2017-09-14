// namespace Emilia {
//   using UnityEngine;
//   using System;
//   using System.Collections.Generic;
//   using XLua;
//   using UObject = UnityEngine.Object;
//   public class PanelManager: MonoBehaviour {

//     static PanelManager instance;
//     static PanelManager singleton {
//       get {
//         if (instance == null) {
//           instance = GameObject.Find("GameManager").AddComponent<PanelManager>();
//           instance.canvas = GameObject.Find("Canvas");
//           if (instance.canvas == null) throw new ApplicationException("[Panel] Canvas is null");
//           instance.Initialize();
//           gm = GameManager.instance;
//         }
//         return instance;
//       }  
//     }

//     static GameManager gm;
//     GameObject canvas;
//     Transform _parent;

//     static readonly Dictionary<string, PanelBase> panelStore = new Dictionary<string, PanelBase>();
//     static readonly Dictionary<PanelLayer, Transform> layerContainerStore = new Dictionary<PanelLayer, Transform>();
//     Transform parent {
//       get {
//         if (_parent == null) {
//           var go = GameObject.FindWithTag("GuiCamera");
//           if (go != null) _parent = go.transform; 
//         }

//         return parent;
//       }
//     }

//     void Initialize () {
//       foreach (PanelLayer layer in Enum.GetValues(typeof(PanelLayer))) {
//         var layerName = layer.ToString();
//         var container = canvas.transform.FindChild(layerName);
//         layerContainerStore.Add(layer, container);
//       }
//     }

//     public static void OpenPanel(string skinPath, LuaFunction func, params object[] args) {
//       if (panelStore.ContainsKey(name)) return;

//       PanelBase panel = singleton.canvas.AddComponent<T>();
//       panel.Init(args);
//       panelStore.Add(name, panel);
//       skinPath = skinPath.Equals("") ? panel.skinPath : skinPath;
//       string assetName = skinPath + "Panel";
//       gm.resMgr.LoadPrefab(skinPath, new string[]{ assetName }, (UObject[] objs) => {
//         if (objs.Length == 0) return;
//         var prefab = objs[0] as GameObject;
//         if (prefab == null) return;

//         var go = Instantiate(prefab, layerContainerStore[panel.layer]) as GameObject;
//         go.name = assetName;
//         go.transform.localScale = Vector3.one;
//         go.transform.localPosition = Vector3.zero;
//         go.AddComponent<LuaBehaviour>();

//         if (func != null) func.Call(go);
//         panel.skin = go;
//       });

//       var skin = Resources.Load<GameObject>(skinPath);
//       if (skin == null) throw new System.ApplicationException("[Panel] Open Panel Fail: without skin");

//       panel.skin = Instantiate(skin, layerContainerStore[panel.layer]) as GameObject;
      
//       panel.OnOpening();
//       panel.OnOpened();
//     }

//     public static void ClosePanel(string name) {
//       var panel = panelStore.ContainsKey(name) ? panelStore[name] : null;
//       if (panel == null) return;

//       panel.OnClosing();
//       panelStore.Remove(name);
//       panel.OnClosed();
//       GameObject.Destroy(panel.skin);
//       Component.Destroy(panel);
      
//     }
//   }
// }