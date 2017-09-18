namespace Util {
  using UnityEngine;
  using System.Collections;
  using System.IO;
  using System.Text;
  public partial class Util
  {
    public static string MD5(string path)
    {
      var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
      var fs = new FileStream(path, FileMode.Open);
      var builder = new StringBuilder();
      byte[] data = md5.ComputeHash(fs);

      foreach (byte b in data) builder.Append(b.ToString("x2"));
      fs.Dispose();
    
      return builder.ToString();
    }

    
  }

}