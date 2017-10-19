using UnityEngine;
using Emilia;
using System;
using System.Collections.Generic;
///指令控制
public class Invoker
{
  protected static volatile Invoker single;
  protected static readonly object m_staticSyncRoot = new object();
  protected Dictionary<CommandType, ICommand> commandMap;
  protected Dictionary<Type, object> managerMap;

  public static Invoker Instance
  {
    get
    {
      if (single == null)
      {
        lock (m_staticSyncRoot)
          if (single == null) single = new Invoker();
      }
      return single;
    }
  }
  private Invoker()
  {
    commandMap = new Dictionary<CommandType, ICommand>();
    managerMap = new Dictionary<Type, object>();
  }
  public void RegisterCommand(CommandType type, ICommand iCommand)
  {
    commandMap.Add(type, iCommand);
  }
  public void RemoveCommand(CommandType type)
  {
    if (commandMap.ContainsKey(type)) commandMap.Remove(type);
  }
  public void ExecuteCommand(CommandType type)
  {
    if (commandMap.ContainsKey(type)) commandMap[type].Execute();
  }
  public void AddManager<T>() where T : Component
  {
    Type type = typeof(T);
    object manager = null;
    managerMap.TryGetValue(type, out manager);
    if (manager != null) return;
    Component component = GameManager.instance.gameObject.AddComponent<T>();
    managerMap.Add(type, manager);
  }
  public T 	GetManager<T>() where T : Component
  {
    Type type = typeof(T);
    object manager = null;
    managerMap.TryGetValue(type, out manager);
    if (manager != null) return (T)manager;
    else
    {
      Debug.Log("???????");
    }
    // Component component = GameManager.instance.gameObject.AddComponent<T>();
    // managerMap.Add(type, manager);
    return (T)manager;
  }
}
