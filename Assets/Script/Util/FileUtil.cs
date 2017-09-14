using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using LitJson;
public class FileUtil
{
    private static FileUtil single;

    private FileUtil()
    {
        downloadList = new List<string>();
    }
    public static FileUtil Instance
    {
        get
        {
            if (single == null) single = new FileUtil();
            return single;
        }
    }
    //远端配置文件
    public BundleFile RemoteBundle
    {
        private set { }
        get
        {
            string content = FileGetContent(AppConst.DATA_PATH + "/bundle_file.txt");
            if (!content.Equals(string.Empty))
            {
                BundleFile file = JsonMapper.ToObject<BundleFile>(content);
                return file;
            }
            return null;
        }
    }
    //本地文件配置文件
    public BundleFile LocalBundle
    {
        private set { }
        get
        {
            string content = FileGetContent(AppConst.BUNDLE_FILE_PATH);
            if (!content.Equals(string.Empty))
            {
                BundleFile file = JsonMapper.ToObject<BundleFile>(content);
                return file;
            }
            return null;
        }
    }
    List<string> downloadList;//存储下载的文件信息
    public string FileGetContent(string path)
    {
        //不存在返回空
        if (!File.Exists(AppConst.DATA_PATH + "/bundle_file.txt")) return string.Empty;
        //读取文件内容
        FileStream stream = File.Open(path, FileMode.Open);
        byte[] bytes = new byte[stream.ReadByte()];
        stream.Read(bytes, 0, stream.ReadByte());
        stream.Dispose();
        string content = Encoding.UTF8.GetString(bytes);
        return content;
    }
    //比较
    public List<string> ComepareRemoteBundle()
    {
        downloadList.Clear();
        //判断是否已经被下载
        if (RemoteBundle == null) return downloadList;
        if (!RemoteBundle.bundle_version.Equals(LocalBundle.bundle_version))
        {
            foreach (string key in RemoteBundle.file_info.Keys)
            {
                if (LocalBundle.file_info.ContainsKey(key))
                {
                    downloadList.Add(key);
                }
            }
        }
        return downloadList;
    }
}
