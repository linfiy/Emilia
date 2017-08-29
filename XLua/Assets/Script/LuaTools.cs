using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class LuaTools
{
  public static string MD5(string path)
  {
    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
    FileStream fs = new FileStream(path, FileMode.Open);
    byte[] data = md5.ComputeHash(fs);
    StringBuilder builder = new StringBuilder();
    foreach (byte b in data) builder.Append(b.ToString("x2"));
    fs.Dispose();
    return builder.ToString();
  }
}
