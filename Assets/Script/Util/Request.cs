namespace Util {
  using UniRx;
  using XLua;
  public static class Request {

    public static void Get (
      string url, LuaFunction success, LuaFunction error
    ) {
      
      ObservableWWW.Get(url)
      .Subscribe(
        str => success.Call(str),
        e => error.Call(e.Message)
      );
    }
  }
}