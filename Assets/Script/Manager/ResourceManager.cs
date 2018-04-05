namespace Emilia {
  using UnityEngine;
  using System.Collections.Generic;
  using System.Collections;
  using XLua;
  using UObject = UnityEngine.Object;
  using System;
  using Util;

  public class AssetBundleDetail {
    public AssetBundle assetBundle { get; private set; }
    public int referencedCount;

    public AssetBundleDetail (AssetBundle assetBundle) {
      this.assetBundle = assetBundle;
      referencedCount = 0;
    }
  }
  public class ResourceManager: MonoBehaviour {
    string baseDownloadURI = string.Empty;
    string[] allManifest = null;
    AssetBundleManifest assetBundleManifest = null;

    Dictionary<string, string[]> dependencies = 
    new Dictionary<string, string[]>();
    Dictionary<string, AssetBundleDetail> loadedAssetBundles = 
    new Dictionary<string, AssetBundleDetail>();

    Dictionary<string, List<LoadAssetRequest>> loadRequests = new Dictionary<string, List<LoadAssetRequest>>();

    class LoadAssetRequest {
      public Type assetType;
      public string[] assetNames;
      public LuaFunction luaFunction;
      public Action<UObject[]> sharpFunction;

    }

    // 

    public void Initialize (string manifestName, Action done) {
      baseDownloadURI = Util.GetRelativePath();
      LoadAsset<AssetBundleManifest>(
        manifestName, 
        new string[]{ "AssetBundleManifest" }, 
        (UObject[] objs) => {
          if (objs.Length > 0) {
            assetBundleManifest = objs[0] as AssetBundleManifest;
            allManifest = assetBundleManifest.GetAllAssetBundles();
          }

          if (done != null) done();
        }
      );
    }

    public void LoadPrefab(string abName, string assetName, Action<UObject[]> func) {
      LoadPrefab(abName, new string[]{ assetName }, func);
    }
    public void LoadPrefab (string abName, string[] assetNames, Action<UObject[]> func) {
      LoadAsset<GameObject>(abName, assetNames, func);
    }

    public void LoadPrefab(string abName, string assetName, LuaFunction func) {
      LoadPrefab(abName, new string[]{ assetName }, func);
    }
    
    public void LoadPrefab (string abName, string[] assetNames, LuaFunction func) {
      LoadAsset<GameObject>(abName, assetNames, null, func);
    }

    string GetRealAssetPath (string abName) {
      if (abName.Equals(AppConst.ASSET_DIC)) return abName;
      abName = abName.ToLower();
      if (!abName.EndsWith(AppConst.EXT_NAME)) abName += AppConst.EXT_NAME;
      if (abName.Contains("/")) return abName;

      for (int i = 0; i < allManifest.Length; i++) {
        var manifest = allManifest[i];
        var index = manifest.LastIndexOf('/');
        var path = manifest.Remove(0, index + 1);
        if (path.Equals(abName)) return manifest;
      }

      Debug.LogError("GetRealAssetPath Error:>>" + abName);
      return null;
    }

    void LoadAsset<T> (
      string abName, string[] assetNames, Action<UObject[]> action = null,
      LuaFunction func = null
    ) where T: UObject {

      var req = new LoadAssetRequest();
      req.assetNames = assetNames;
      req.luaFunction = func;
      req.sharpFunction = action;
      req.assetType = typeof(T);

      List<LoadAssetRequest> requests = null;

      if (loadRequests.TryGetValue(abName, out requests)) {
        requests.Add(req);
      }
      else {
        requests = new List<LoadAssetRequest>();
        requests.Add(req);
        loadRequests.Add(abName, requests);
        StartCoroutine(OnLoadAsset<T>(abName));
      }
    }


    IEnumerator OnLoadAsset<T> (string abName) where T: UObject {
      var bundleDetail = GetLoadedAssetBundle(abName);

      if (bundleDetail == null) {
        yield return StartCoroutine(OnLoadAssetBundle(abName, typeof(T)));

        bundleDetail = GetLoadedAssetBundle(abName);
        if (bundleDetail == null) {
          loadRequests.Remove(abName);
          Debug.LogError("OnLoadAsset--->>>" + abName);
          yield break;
        }
      }

      List<LoadAssetRequest> reqList = null;
      if (!loadRequests.TryGetValue(abName, out reqList)) {
        loadRequests.Remove(abName);
        yield break;
      }

      for (int i = 0; i < reqList.Count; i++) {
        string[] assetNames = reqList[i].assetNames;
        var result = new List<UObject>();

        AssetBundle ab = bundleDetail.assetBundle;

        for (int j = 0; j < assetNames.Length; j++) {
          string assetPath = assetNames[j];
          var request = ab.LoadAssetAsync(assetPath, reqList[i].assetType);
          yield return request;
          result.Add(request.asset);
        }
        
        var reqItem = reqList[i];
        if (reqItem.sharpFunction != null) {
          reqItem.sharpFunction(result.ToArray());
          reqItem.sharpFunction = null;
        }

        if (reqItem.luaFunction != null) {
          reqItem.luaFunction.Call((object)result.ToArray());
          reqItem.luaFunction.Dispose();
          reqItem.luaFunction = null;
        }

        bundleDetail.referencedCount++;
      }

      loadRequests.Remove(abName);
    }

    IEnumerator OnLoadAssetBundle (string abName, Type type) {
      var url = baseDownloadURI + abName;

      WWW download = null;
      if (type == typeof(AssetBundleManifest)) download = new WWW(url);
      else {
        string[] deps = assetBundleManifest.GetAllDependencies(abName);
        if (deps.Length > 0) {
          dependencies.Add(abName, deps);
          for (int i = 0; i < deps.Length; i++) {
            string depName = deps[i];
            AssetBundleDetail bundleDetail = null;
            if (loadedAssetBundles.TryGetValue(depName, out bundleDetail)) {
              bundleDetail.referencedCount++;
            }
            else if (loadRequests.ContainsKey(depName)) {
              yield return StartCoroutine(OnLoadAssetBundle(depName, type));
            }
          }
        }

        download = WWW.LoadFromCacheOrDownload(url, assetBundleManifest.GetAssetBundleHash(abName), 0);
      }

      yield return download;

      var loadedBundle = download.assetBundle;
      if (loadedBundle != null) {
        loadedAssetBundles.Add(abName, new AssetBundleDetail(loadedBundle));
      }
    }

    //这个方法好像有问题??????
    AssetBundleDetail GetLoadedAssetBundle (string abName) {
      AssetBundleDetail detail = null;
      loadedAssetBundles.TryGetValue(abName, out detail);
      if (detail == null) return null;

      string[] deps = null;
      if (!dependencies.TryGetValue(abName, out deps)) return detail;

      foreach (var depName in deps) {
        AssetBundleDetail depDetail;
        loadedAssetBundles.TryGetValue(depName, out depDetail);
        if (depDetail == null) return null;
      }

      return detail;
    }

    /// <summary>
    /// 此函数交给外部卸载专用，自己调整是否需要彻底清除AB
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="isThorough"></param>
    public void UnloadAssetBundle (string abName, bool isThorough = false) {
      abName = GetRealAssetPath(abName);
      Debug.Log(loadedAssetBundles.Count + " assetbundle(s) in memory before unloading " + abName);
      UnloadAssetBundleInternal(abName, isThorough);
      UnloadDependencies(abName, isThorough);
      Debug.Log(loadedAssetBundles.Count + " assetbundle(s) in memory after unloading " + abName);
    }

    void UnloadDependencies (string abName, bool isThorough) {
      string[] deps = null;
      if (dependencies.TryGetValue(abName, out deps)) {
        return;
      }

      foreach (var depName in deps) {
        UnloadAssetBundleInternal(depName, isThorough);
      }

      dependencies.Remove(abName);
    }

    void UnloadAssetBundleInternal (string abName, bool isThorough) {
      var bundleDetail = GetLoadedAssetBundle(abName);

      if (bundleDetail == null) return;

      if (--bundleDetail.referencedCount <= 0) {
         //如果当前AB处于Async Loading过程中，卸载会崩溃，只减去引用计数即可
        if (loadRequests.ContainsKey(abName)) return;

        bundleDetail.assetBundle.Unload(isThorough);
        loadedAssetBundles.Remove(abName);
        Debug.Log(abName + " has been unloaded successfully");
      }
    }
  }
}