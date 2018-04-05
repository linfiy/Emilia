namespace Emilia
{
  using UnityEngine;
  using System;
  using System.Net;
  using System.IO;
  using System.Collections;
  using System.Collections.Generic;
  using Util;
  using UniRx;
  public partial class GameManager
  {
    IObservable<Unit> CheckHotResource()
    {
      return Observable.Create<Unit>((observer) =>
      {
        Debug.Log("检测资源更新");
        bool isExists =
        Directory.Exists(Util.DataPath) &&
        Directory.Exists(Util.DataPath + "Lua/") &&
        File.Exists(Util.DataPath + "bundle_index.txt");
        IDisposable updateSub = null;
        if (isExists || AppConst.DebugMode)
        {
          //print(OnUpdateResource());
          updateSub = OnUpdateResource()
          .Subscribe(_ =>
          {
            observer.OnNext(Unit.Default);
          }, observer.OnError).AddTo(this);
          // return;
        }
        else
        {
          //启动释放协成 
          observer.OnError(new Exception("启动释放协成 还没做"));
        }
        return Disposable.Create(() =>
        {
          if (updateSub != null) updateSub.Dispose();
        });
      });
    }
    IObservable<Unit> OnUpdateResource()
    {
      return Observable.Create<Unit>((observer) =>
      {
        if (!AppConst.updateMode)
        {
          Debug.Log("没有开启更新模式，AppConst.updateMode = false");
          observer.OnNext(Unit.Default);
          return Disposable.Empty;
        }
        var dataPath = Util.DataPath;
        var url = AppConst.WEB_URL;
        var message = string.Empty;
        var random = DateTime.Now.ToString("yyyymmddhhmmss");
        var listUrl = url + "bundle_index.txt?v=" + random;
        Debug.LogWarning("LoadUpdate---->>>" + listUrl);
        // 下载索引文件进行比对
        var indexSub = ObservableWWW.GetWWW(listUrl).Subscribe(res =>
        {
          if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
          File.WriteAllBytes(dataPath + "bundle_index.txt", res.bytes);
          string filesText = res.text;
          string[] files = filesText.Split('\n');
          // 需要下载的文件列表
          var needDownFiles = new List<string>();
          for (int i = 0; i < files.Length; i++)
          {
            if (string.IsNullOrEmpty(files[i])) continue; // 空行跳过
            string[] keyValue = files[i].Split('|');
            string f = keyValue[1];
            string localfileURI = (dataPath + f).Trim();
            string path = Path.GetDirectoryName(localfileURI);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var hasLocalFile = File.Exists(localfileURI);
            bool needUpdate = true;
            if (hasLocalFile)
            {
              string remoteMd5 = keyValue[0].Trim();
              string localMd5 = Util.MD5(localfileURI);
              needUpdate = !remoteMd5.Equals(localMd5);
              if (needUpdate) File.Delete(localfileURI);
            }
            //本地缺少文件
            if (needUpdate) needDownFiles.Add(f);
          }
          // 没有需要下载的文件
          if (needDownFiles.Count == 0)
          {
            Debug.Log("比对远程文件完成，不需要更新");
            observer.OnNext(Unit.Default);
          }
          // 下载资源文件
          else
          {
            Debug.Log("资源有更新,开始更新");
            Debug.Log("需要更新的资源个数:" + needDownFiles.Count);
            Observable.Start(() =>
            {
              for (int i = 0; i < needDownFiles.Count; i++)
              {
                var f = needDownFiles[i];
                // 之后要改成异步的，获取下载进度
                using (var client = new WebClient())
                {
                  Debug.Log("DOADING:" + url + " To >>>> " + dataPath + f);
                  client.DownloadFile((url).Trim(), (dataPath + f).Trim());
                }
              }
              return;
            })
            .ObserveOnMainThread()
            .Subscribe(_ =>
            {
              observer.OnNext(Unit.Default);
            }, observer.OnError)
            .AddTo(this);
          }
        },
        e => observer.OnError(
          new ApplicationException("获取更新信息失败, 请检查网络参数" + listUrl)
        )
        ).AddTo(this);
        return Disposable.Create(() => indexSub.Dispose());
      });
    }
  }
}