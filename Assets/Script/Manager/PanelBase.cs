// namespace Emilia {
//   using UnityEngine;
//   public class PanelBase: MonoBehaviour {
//     public string skinPath;
//     public GameObject skin;
//     public PanelLayer layer;

//     public object[] args;

//     public virtual void Init (params object[] args) {
//       this.args = args;
//     }

//     public virtual void OnOpening () {}
//     public virtual void OnOpened () {}
//     public virtual void Update () {}
//     public virtual void OnClosing () {}
//     public virtual void OnClosed () {}

//     protected virtual void Close () {
//       string name = this.GetType().ToString();
//       PanelManager.ClosePanel(name);
//     }
//   }

//   public enum PanelLayer {
//     Panel, Notice
//   }
// }