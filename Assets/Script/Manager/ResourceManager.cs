// namespace RUA {
//   using UnityEngine;
//   using System.Collections.Generic;
//   using XLua;
//   using UObject = UnityEngine.Object;
//   using System;
//   using Utils;

//   public class AssetBundleDetail {
//     public AssetBundle assetBundle { get; private set; }
//     public int referencedCount;

//     public AssetBundleDetail (AssetBundle assetBundle) {
//       this.assetBundle = assetBundle;
//       referencedCount = 0;
//     }
//   }
//   public class ResourceManager: MonoBehaviour {
//     string baseDownloadURI = string.Empty;
//     string[] allManifest = null;
//     AssetBundleManifest assetBundleManifest = null;

//     Dictionary<string, string[]> dependencies = 
//     new Dictionary<string, string[]>();
//     Dictionary<string, AssetBundleDetail> loadedAssetBundles = 
//     new Dictionary<string, AssetBundleDetail>();

//     Dictionary<string, List<LoadAssetRequest>> loadRequests = new Dictionary<string, List<LoadAssetRequest>>();

//     class LoadAssetRequest {
//       public Type assetType;
//       public string[] assetNames;
//       public LuaFunction luaFunction;
//       public Action<UObject[]> sharpFunction;

//     }

//     // 

//     public void Initialize (string manifestName, Action done) {
//       baseDownloadURI = Util.GetRelativePath();
//       LoadAsset<AssetBundleManifest>(
//         manifestName, 
//         new string[]{ "AssetBundleManifest" }, 
//         (UObject[] objs) => {
//           if (objs.Length > 0) {
//             assetBundleManifest = objs[0] as AssetBundleManifest;
//             allManifest = assetBundleManifest.GetAllAssetBundles();
//           }

//           if (done != null) done();
//         }
//       );
//     }

//     public void LoadPrefab (string abName, string[] assetNames, Action<UObject[]> func) {
//       LoadAsset<GameObject>(abName, assetNames, func);
//     }

//     public void LoadPrefab (string abName, string[] assetNames, LuaFunction func) {
//       LoadAsset<GameObject>(abName, assetNames, null, func);
//     }

//     string GetRealAssetPath (string abName) {
//       if (abName.Equals(AppConst.ASSET_DIC)) return abName;
//       abName = abName.ToLower();
//       if (!abName.EndsWith(AppConst.EXT_NAME)) abName += AppConst.EXT_NAME;
//       if (abName.Contains("/")) return abName;

//       for (int i = 0; i < allManifest.Length; i++) {
//         var manifest = allManifest[i];
//         var index = manifest.LastIndexOf('/');
//         var path = manifest.Remove(0, index + 1);
//         if (path.Equals(abName)) return manifest;
//       }

//       Debug.LogError("GetRealAssetPath Error:>>" + abName);
//       return null;
//     }

//     void LoadAsset<T> (
//       string absoluteName, string[] assetNames, Action<UObject[]> action = null,
//       LuaFunction func = null
//     ) where T: UObject {

//       var req = new LoadAssetRequest();
//       req.assetNames = assetNames;
//       req.luaFunction = func;
//       req.sharpFunction = action;
//       req.assetType = typeof(T);

//       List<LoadAssetRequest> requests = null;

//       if (loadRequests.TryGetValue(absoluteName, out requests)) {
//         requests = new List<LoadAssetRequest>();
//         requests.Add(req);
//         loadRequests.Add(absoluteName, requests);
//         StartCoroutine(OnLoadAsset<T>(absoluteName));
//       }
//       else {
//         requests.Add(req);
//       }
//     }


//     IEnumerator OnLoadAsset<T> (string abName) where T: UObject {
//       var 
//     }
//   }
// }