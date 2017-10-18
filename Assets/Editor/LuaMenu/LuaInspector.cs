//根据选中的lua文件获取其path，然后读取内容，通过OnInspectorGUI来显示
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

[CustomAsset(".lua")]
public class LuaInspector : Editor
{
  private string content;

  void OnEnable()
  {
    string path = Application.dataPath + "/" + AssetDatabase.GetAssetPath(Selection.activeObject).Substring(7);
    try
    {
      TextReader tr = new StreamReader(path);

      content = tr.ReadToEnd();

      tr.Close();
    }
    catch (Exception e)
    {
      Debug.Log(e);
    }
  }

  public override void OnInspectorGUI()
  {
    GUILayout.Label(content);
  }
}