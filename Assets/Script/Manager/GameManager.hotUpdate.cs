namespace Emilia {
  using UnityEngine;
  using System;
  using System.Net;
  using System.IO;
  using System.Collections;
  using System.Collections.Generic;
  using Util;
  using UniRx;
  public partial class GameManager {

    void CheckHotResource () {
      bool isExists = 
      Directory.Exists(Util.DataPath) &&
      Directory.Exists(Util.DataPath + "Lua/") && 
      File.Exists(Util.DataPath + "bundle_index.txt");

      if (isExists || AppConst.DebugMode) {
        OnUpdateResource()
        .Subscribe(_ => {
          Debug.Log("更新成功");
        }).AddTo(this);
        // return;
      }

      //启动释放协成 
    }

    IObservable<Unit> OnUpdateResource () {

      return Observable.Create<Unit>((observer) => {
        if (!AppConst.updateMode) {
          // OnResourceInited();
          observer.OnError(new Exception("没有开启更新模式"));
          observer.OnCompleted();
          return Disposable.Empty;
        }

        var dataPath = Util.DataPath;
        var url = AppConst.WEB_URL;
        var message = string.Empty;
        var random = DateTime.Now.ToString("yyyymmddhhmmss");
        var listUrl = url + "bundle_index.txt?v=" + random;
        Debug.LogWarning("LoadUpdate---->>>" + listUrl);
        // 下载索引文件
        var indexSub = ObservableWWW.GetWWW(listUrl).Subscribe(res => {
          if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
          File.WriteAllBytes(dataPath + "bundle_index.txt", res.bytes);
          string filesText = res.text;
          string[] files = filesText.Split('\n');

          // 需要下载的文件列表
          var needDownFiles = new List<string>();
          for (int i = 0; i < files.Length; i++) {
            if (string.IsNullOrEmpty(files[i])) continue; // 空行跳过

            string[] keyValue = files[i].Split('|');
            string f = keyValue[1];
            string localfileURI = (dataPath + f).Trim();
            string path = Path.GetDirectoryName(localfileURI);
            // string fileRequestUrl = url + f + "?v=" + random;

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            
            var hasLocalFile = File.Exists(localfileURI);
            bool needUpdate = true;

            if (hasLocalFile) {
              
              string remoteMd5 = keyValue[0].Trim();
              string localMd5 = Util.MD5(localfileURI);
              print("localfileURI:" + localfileURI.Replace('\\', '/'));
              // print("localMd5:" + localMd5);
              needUpdate = !remoteMd5.Equals(localMd5);
              if (needUpdate) File.Delete(localfileURI);
            }

            if (needUpdate) {   //本地缺少文件
              needDownFiles.Add(localfileURI);
            }
          }

          // 没有需要下载的文件
          if (needDownFiles.Count == 0) {
            Debug.Log("不需要更新");
            observer.OnNext(Unit.Default);
            observer.OnCompleted();
          }
          // 下载资源文件
          else {
            Observable.Start(() => {
              using (WebClient client = new WebClient()) {
                for (int i = 0; i < needDownFiles.Count; i++) {
                  var f = needDownFiles[i];
                  string fileRequestUrl = url + f + "?v=" + random;
                  Debug.Log("downloading>>" + f);
                  client.DownloadFile(url, f);
                }
              }
              return;
            })
            .ObserveOnMainThread()
            .Subscribe(_ => {
              if (Application.isEditor) UnityEditor.AssetDatabase.Refresh();
              observer.OnNext(Unit.Default);
            }, observer.OnError, observer.OnCompleted)
            .AddTo(this);
          }

        }, observer.OnError).AddTo(this);

        return Disposable.Create(() => {
          indexSub.Dispose();
        });
      });
      
    }

    // void BeginDownload(string url, string file) {     //线程下载
    //   object[] param = new object[2] { url, file };

    //   ThreadEvent ev = new ThreadEvent();
    //   ev.Key = NotiConst.UPDATE_DOWNLOAD;
    //   ev.evParams.AddRange(param);
    //   ThreadManager.AddEvent(ev, OnThreadCompleted);   //线程下载
    // }
  }
}