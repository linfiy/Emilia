using UnityEngine;
using UniRx;
using System.IO;
using System.Collections.Generic;
public class WWWUtil
{
    public static void DownloadFile(string path)
    {
        ObservableWWW.GetAndGetBytes(AppConst.WEB_URL + "/" + path)
        .Subscribe(
        bytes =>
        {
            if (File.Exists(AppConst.DATA_PATH + "/" + path)) File.Delete(AppConst.DATA_PATH + "/" + path);
            FileStream stream = File.Create(path);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Dispose();
        },
        e => Debug.LogError("DowloadFile Erro ---->" + path + "\n" + e.ToString())
        );

    }
	//下载配置文件
    public static void DownloadBundleFile()
    {
        DownloadFile("bundle_file.txt");
    }
	//下载所有修改的资源
    public static void DownloadResources(List<string> resources)
    {
        resources.ForEach(
            path => DownloadFile(path)
        );
    }
}
