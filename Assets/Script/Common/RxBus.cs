using UniRx;
using System;
/// 全局对象传送工具
public class RxBus<T>
{
  protected static volatile RxBus<T> single;
  protected static readonly object m_staticSyncRoot = new object();
  public static RxBus<T> Instance
  {
    get
    {
      if (single == null)
        lock (m_staticSyncRoot)
          if (single == null) single = new RxBus<T>();
      return single;
    }
  }
  RxBus()
  {
    bus = new Subject<T>();
  }
  private Subject<T> bus;
  public void Post(T obj)
  {
    bus.OnNext(obj);
  }
  public IObservable<T> toObserverable()
  {
    return bus.AsObservable<T>();
  }
}
