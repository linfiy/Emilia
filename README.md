

- [XLua](https://github.com/Tencent/xLua)
- [RxLua](https://github.com/bjornbytes/RxLua)
- [UniRx](https://github.com/neuecc/UniRx)
- [json.lua](https://github.com/rxi/json.lua)

#说明
1. Lua代码在Assets/Lua/下编写（可以在Lualoader中设置）    

2. 修改Lua代码需要执行 AssetsBundle/BuildWindowsAssets 才能生效

3. 需要与 lua 想关联的 c# 代码需要重新执行
XLua/GenerateCode。第一次运行项目也需要点击此选项

## 项目目前的使用方法

Asset/Script/ConstDefine/AppConst.cs

updateMode 设置为 false, 加载本地文件, 按一下步骤操作即可

1. `XLua` - `Generate Code`
2. `AssetsBundle` - `BuildAsset`
3. Run

updateMode 设置为 true，需要配置 WEB_URL 并开启对应的服务器，存有更新文件，然后可以进行

## 完成度

待完成

- 消息系统(wby)
- PanelManager/PanelBase(wby)


简单实现:
- 资源文件打包
- 热更新检查
  * next: 异步下载文件并提供下载进度
- ResourceManager

已完成
- lua 侧 http 封装