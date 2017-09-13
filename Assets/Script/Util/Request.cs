namespace Util {
  using UniRx;
  using XLua;
  public static class Request {

    public static void Get (
      string url, LuaFunction success, LuaFunction error
    ) {
      
      ObservableWWW.Get(url)
      .Subscribe(
        str => {
          // format
          // var table = new LuaTable();
          success.Call(str);
        },
        e => {
          // format
          // var eTable = new LuaTable();
          error.Call(e.Message);
        }
      );
    }
  }
}