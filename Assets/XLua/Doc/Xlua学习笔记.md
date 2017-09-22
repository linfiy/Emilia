# 官方示例解读
### 示例1-HelloWorld

``` c#
LuaEnv luaenv = new LuaEnv();
luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
luaenv.Dispose();
```     
代码解析: lua代码运行时需要先启动Lua虚拟机,Xlua在lua虚拟机启动时会有一系列的设置，所以Xlua对于lua虚拟机进行了一层封装，==LuaEnv即为封装后的Lua运行环境==。一般一个项目有一个Lua运行环境即可.

与ulua slua相似，Xlua也保留着DoString函数用来运行lua代码，与其他两种不同的是Xlua不在保留DoFile接口；不过，虽然==Xlua只保留了DoString一个Lua代码启动入口==，但是，Xlua在DoString函数原来的基础之上扩展了Lua内置函数 require ，
==使 require 函数在unity中可以直接遍历查找Resources文件夹中对应的脚本==。如 LuaEnv.DoString(" require 'Mylua' ");
这句代码运行起来后，==require 函数会首先遍历Xlua内置Lua脚本，如果未找到，则遍历Unity中Resources文件夹中的Lua文件==(==后缀为.lua.txt==)

```lua
luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
```
这句代码中的字符串为示例1中唯一一句Lua代码，注意观察这句代码我们会发现Xlua调用C#函数的方式非常类似于C#语言写出来的C#代码。

事实上，==Xlua调用C#代码的方式非常简单，只需要在Lua中需要调用的c#函数，c#属性，或者c#类上方加上 LuaCallCsharp 特征==，然后通过：

==无命名空间的类使用 CS.[类名]==

==1. 调用类：CS.[命名空间].[类名]==

==2. 调用静态方法: CS.[命名空间].[类名].[静态方法名] (参数)==

==3. 调用静态属性： CS.[命名空间].[类名].[静态属性名称]==

==4. 调用类实例:    [对象名称]==

==5. 调用非静态函数： [对象名称].[非静态属性]==

==6. 调用非静态方法： [对象名称]：[非静态方法] (参数)==


### 示例2-U3DScripting

C#重点
```c#
scriptEnv = luaEnv.NewTable();

LuaTable meta = luaEnv.NewTable();
meta.Set("__index", luaEnv.Global);
scriptEnv.SetMetaTable(meta);
meta.Dispose();


Action luaAwake = scriptEnv.Get<Action>("awake");
scriptEnv.Get("start", out luaStart);
scriptEnv.Get("update", out luaUpdate);
scriptEnv.Get("ondestroy", out luaOnDestroy);
```
Lua代码
``` Lua
local speed = 10

function start()
	print("lua start...")
end

function update()
	local r = CS.UnityEngine.Vector3.up * CS.UnityEngine.Time.deltaTime * speed
	self.transform:Rotate(r)
end

function ondestroy()
    print("lua destroy")
end
```
观察上述代码我们会发现重点代码为：

scriptEnv.Get<Action>("awake");

scriptEnv.Get("start", out luaStart);

这两句代码LuaEnv.Get的方式可以获取到Lua代码中的函数,事实上，在Xlua官方文档上存在这样一个接口：

    LuaEnv.Global.Get<T>(Key)
    LuaEnv.Global.Get<Tkey,Tvalue>()
这样几个接口，具体接口参见附录
==Get<T>()这样的接口可以通过映射将Lua中的数据(包括函数，Table)映射到类型T上(类型T必须标记有CSharpCallLua)==

### 示例3-UIEvent_Ugui

C#重点代码

```
scriptEnv.Set("self", this);
```


Lua重点代码
```
function start()
	print("lua start...")

	self:GetComponent("Button").onClick:AddListener(function()
		print("clicked, you input is '" ..input:GetComponent("InputField").text .."'")
	end)
end
```
该示例重点讲解了两个知识点：
- ==给Lua中的变量赋值==
- ==UI事件绑定==

给Lua中的变量赋值我们可以通过LuaTable类的

Set(Tkey，Tvalue)

set(string,TValue)

方法进行赋值,其中TKey或String为Lua的变量，Tvalue为设置的值 (==若Lua不存在该变量，则会在lua中创建该变量并赋值==)

详情参考附录一中LuaTable类型


### ==示例4-InvokeLua __04_LuaObjectOrented==

重点代码

```
 [CSharpCallLua]
    public interface ICalc
    {
        int Add(int a, int b);
        int Mult { get; set; }
    }

    [CSharpCallLua]
    public delegate ICalc CalcNew(int mult, params string[] args);

    private string script = @"
                local calc_mt = {
                    __index = {
                        Add = function(self, a, b)
                            return (a + b) * self.Mult
                        end
                    }
                }

                Calc = {
	                New = function (mult, ...)
                        print(...)
                        return setmetatable({Mult = mult}, calc_mt)
                    end
                }
	        ";
    // Use this for initialization
    void Start()
    {
        LuaEnv luaenv = new LuaEnv();
        Test(luaenv);//调用了带可变参数的delegate，函数结束都不会释放delegate，即使置空并调用GC
        luaenv.Dispose();
    }

    void Test(LuaEnv luaenv)
    {
        luaenv.DoString(script);
        CalcNew calc_new = luaenv.Global.GetInPath<CalcNew>("Calc.New");
        ICalc calc = calc_new(10, "hi", "john"); //constructor
        Debug.Log("sum(*10) =" + calc.Add(1, 2));
        calc.Mult = 100;
        Debug.Log("sum(*100)=" + calc.Add(1, 2));
    }
```
该示例主要展示了两个知识点:

- ==T GetInPath<T>(string path) 函数==
- ==Lua函数映射至委托==

==GetInPath<T>(string path)== ==在获取Lua中复杂类型时，会将该复杂类型视为一个Table或者Class，所以访问时，路径可以按照obj.prop.field这样一个顺序来查找对应的类型(lua中函数也是一种类型)==

c#在获取Lua函数时可以通过 函数映射 或者 GetLuaFunction的方式来获取，相比GetLuaFunction的方式，函数映射的性能更高。

==Lua函数在映射到c#中时必须委托来接收== ，且==任何从lua中映射至c#的类型都必须加上==[==CsharpCallLua==]==特征==

详情参见附录二



# 附录一 Xlua_API

**==C# API==**

==LuaEnv类==

==object[] DoString(string chunk, string chunkName = "chuck", LuaTable env = null)==

描述：

执行一个代码块。

参数：

chunk: Lua代码；

chunkName： 发生error时的debug显示信息中使用，指明某某代码块的某行错误；

env ：为这个代码块；

返回值：

代码块里return语句的返回值;

比如：return 1, “hello”，DoString返回将包含两个object， 一个是double类型的1，

一个是string类型的“hello”

例子：

```
LuaEnv luaenv = new LuaEnv();
object[] ret = luaenv.DoString("print(‘hello’)\r\nreturn 1")
UnityEngine.Debug.Log("ret="+ret[0]);
luaenv.Dispose()

```



==T LoadString<T>(string chunk, string chunkName = "chunk", LuaTable env = null)==

描述：

加载一个代码块，但不执行，只返回类型可以指定为一个delegate或者一个LuaFunction

参数：

chunk: Lua代码；

chunkName： 发生error时的debug显示信息中使用，指明某某代码块的某行错误；

env ：为这个代码块；

返回值：

代表该代码块的delegate或者LuaFunction类；


==LuaTable Global;==

描述：

代表lua全局环境的LuaTable

==void Tick()==

描述：

==清除Lua的未手动释放的LuaBase（比如，LuaTable， LuaFunction），以及其它一些事情。==

==需要定期调用，比如在MonoBehaviour的Update中调用。==

==void AddLoader(CustomLoader loader)==

描述：

增加一个自定义loader

参数：

loader：就一个回调，其类型为delegate byte[] CustomLoader(ref string filepath)，

一个文件被require时，这个loader会被回调，其参数是require的参数，如果该loader找到文件，

可以将其读进内存，返回一个byte数组。如果需要支持调试的话，而filepath要设置成IDE能找到的路径（相对或者绝对都可以）

==void Dispose()==

描述：

Dispose该LuaEnv。

LuaEnv的使用建议：全局就一个实例，并在Update中调用GC方法，完全不需要时调用Dispose

**==LuaTable类==**

==T Get<T>(string key)==

描述：

获取在key下，类型为T的value，如果不存在或者类型不匹配，返回null；

T GetInPath<T>(string path)

描述：

和Get的区别是，这个会识别path里头的“.”，比如vari=tbl.GetInPath<int>(“a.b.c”)相当于在lua里头执行i = tbl.a.b.c

==void SetInPath<T>(string path, T val)==

描述：

和GetInPaht<T>对应的setter；

==void Get<TKey, TValue>(TKey key, out TValue value)==

描述：

上面的API的Key都只能是string，而这个API无此限制；

==void Set<TKey, TValue>(TKey key, TValue value)==

描述：

对应Get<TKey, TValue>的setter；

==T Cast<T>()==

描述：

把该table转成一个T指明的类型，可以是一个加了CSharpCallLua声明的interface，一个有默认构造函数的class或者struct，一个Dictionary，List等等。

==void SetMetaTable(LuaTable metaTable)==

描述：

设置metaTable为table的metatable

**==LuaFunction类==**

注意：

==用该类访问Lua函数会有boxing，unboxing的开销，为了性能考虑，需要频繁调用的地方不要用该类==。==建议通过table.Get<ABCDelegate>获取一个delegate再调用==（假设ABCDelegate是C#的一个delegate）。==在使用使用table.Get<ABCDelegate>之前，请先把ABCDelegate加到代码生成列表。==

==object[] Call(params object[] args)==

描述：

以可变参数调用Lua函数，并返回该调用的返回值。

==object[] Call(object[] args, Type[] returnTypes)==

描述：

调用Lua函数，并指明返回参数的类型，系统会自动按指定类型进行转换。

==void SetEnv(LuaTable env)==

描述：

相当于lua的setfenv函数。

**==Lua API==**

==CS对象==

==CS.namespace.class(...)==

描述：

新建一个C#对象实例，例如：


```
local v1=CS.UnityEngine.Vector3(1,1,1) 
```



==CS.namespace.class.field==

描述：

访问一个C#静态成员，例如：


```
Print(CS.UnityEngine.Vector3.one)
```


==CS.namespace.enum.field==

描述：

访问一个枚举值

==typeof函数==

==类似C#里头的typeof关键字，返回一个Type对象==，比如GameObject.AddComponent其中一个重载需要一个Type参数，这时可以这么用

```
newGameObj:AddComponent(typeof(CS.UnityEngine.ParticleSystem))
```



## ==无符号64位支持==

==uint64.tostring==

描述：==无符号数转字符串==。

==uint64.divide==

描述：==无符号数除法==。

==uint64.compare==

描述：==无符号比较，相对返回0，大于返回正数，小于返回负数==。

==uint64.remainder==

描述：==无符号数取模==。

==uint64.parse==

描述：==字符串转无符号数==。


==xlua.structclone==

描述：==克隆一个c#结构体==

==cast函数==

指明以特定的接口访问对象，这在实现类无法访问的时候（比如internal修饰）很有用，这时可以这么来（假设下面的calc对象实现了C#的PerformentTest.ICalc接口）：

```
cast(calc, typeof(CS.PerformentTest.ICalc))
```



然后就木有其它API了
==访问csharp对象和访问一个table一样，调用函数跟调用lua函数一样，也可以通过操作符访问c#的操作符==，下面是一个例子：

```
local v1=CS.UnityEngine.Vector3(1,1,1) 
local v2=CS.UnityEngine.Vector3(1,1,1) 
v1.x = 100 
v2.y = 100 
print(v1, v2)
local v3 = v1 + v2
print(v1.x, v2.x) 
print(CS.UnityEngine.Vector3.one)
print(CS.UnityEngine.Vector3.Distance(v1, v2))
```



## ==类型映射==

基本数据类型

	

C#类型 | Lua类型
---|---
sbyte，byte，short，ushort，int，uint，double，char，float | number
decimal | userdata
long，ulong | userdata/lua_Integer(lua53)
bytes[] | string
bool | boolean
string | string


## ==复杂数据类型==

C#类型 | Lua类型
---|---
LuaTable | table
LuaFunction | function
class或者 struct的实例 | userdata，table
method，delegate | function

**==LuaTable：==**

==C#侧指明从Lua侧输入==（包括C#方法的输入参数或者Lua方法的返回值）==LuaTable类型==，则==要求Lua侧为table。或者Lua侧的table==，==在C#侧未指明类型的情况下转换成LuaTable==。

**==LuaFunction:==**

==C#侧指明从Lua侧输入==（包括C#方法的输入参数或者Lua方法的返回值）==LuaFunction类型==，则==要求Lua侧为function。或者Lua侧的function，在C#侧未指明类型的情况下转换成LuaFunction==。

==LuaUserData==：

对应非C# Managered对象的lua userdata。

**==class或者 struct的实例:==**

==从C#传一个class或者struct的实例，将映射到Lua的userdata，并通过__index访问该userdata的成员
C#侧指明从Lua侧输入指定类型对象，Lua侧为该类型实例的userdata可以直接使用；如果该指明类型有默认构造函数，Lua侧是table则会自动转换，转换规则是：调用构造函数构造实例，并用table对应字段转换到c#对应值后赋值各成员。==

**==method， delegate：==**

成员方法以及delegate都是对应lua侧的函数。

==C#侧的普通参数以及引用参数，对应lua侧函数参数；C#侧的返回值对应于Lua的第一个返回值；引用参数和out参数则按序对应于Lua的第2到第N个参数。==


# xLua教程

## ==Lua文件加载==

### 一、执行字符串

最基本是直接用LuaEnv.DoString执行一个字符串，当然，字符串得符合Lua语法

比如：luaenv.DoString("print('hello world')")

完整代码见XLua\Tutorial\LoadLuaScript\ByString目录

但这种方式并不建议，更建议下面介绍这种方法。

### 二、加载Lua文件

用lua的require函数即可

比如：DoString("require 'byfile'")

完整代码见XLua\Tutorial\LoadLuaScript\ByFile目录

==require实际上是调一个个的loader去加载，有一个成功就不再往下尝试，全失败则报文件找不到==。
目前==xLua除了原生的loader外，还添加了从Resource加载的loader，需要注意的是因为Resource只支持有限的后缀，放Resources下的lua文件得加上txt后缀==（见附带的例子）。

==建议的加载Lua脚本方式是：整个程序就一个DoString("require 'main'")，然后在main.lua加载其它脚本==（类似lua脚本的命令行执行：lua main.lua）。

有童鞋会问：要是我的Lua文件是下载回来的，或者某个自定义的文件格式里头解压出来，或者需要解密等等，怎么办？问得好，xLua的自定义Loader可以满足这些需求。

### 三、自定义Loader

在xLua加自定义loader是很简单的，只涉及到一个接口：

==public delegate byte[] CustomLoader(ref string filepath);==

==public void LuaEnv.AddLoader(CustomLoader loader)==

==通过AddLoader可以注册个回调，该回调参数是字符串，lua代码里头调用require时，参数将会透传给回调，回调中就可以根据这个参数去加载指定文件，如果需要支持调试，需要把filepath修改为真实路径传出。该回调返回值是一个byte数组，如果为空表示该loader找不到，否则则为lua文件的内容==。
有了这个就简单了，用IIPS的IFS？没问题。写个loader调用IIPS的接口读文件内容即可。文件已经加密？没问题，自己写loader读取文件解密后返回即可。。。

完整示例见XLua\Tutorial\LoadLuaScript\Loader

==C#访问Lua==

这里指的是C#主动发起对Lua数据结构的访问。

本章涉及到的例子都可以在XLua\Tutorial\CSharpCallLua下找到。

- ## 一、获取一个全局基本数据类型

==访问LuaEnv.Global就可以了，上面有个模版Get方法，可指定返回的类型==。


```
luaenv.Global.Get<int>("a")

luaenv.Global.Get<string>("b")

luaenv.Global.Get<bool>("c")
```


- ## 二、访问一个全局的table

也是用上面的Get方法，那类型要指定成啥呢？

##### 1、映射到普通class或struct

==定义一个class，有对应于table的字段的public属性，而且有无参数构造函数即可==，比如对于  =={f1 = 100, f2 = 100}== 可以定义一个包含 ==public int f1;public int f2;== 的class。

这种方式下==xLua会帮你new一个实例，并把对应的字段赋值过去==。

==table的属性可以多于或者少于class的属性。可以嵌套其它复杂类型==。

要注意的是，这个==过程是值拷贝==，如果class比较复杂代价会比较大。而且修改class的字段值不会同步到table，反过来也不会。

这个功能==可以通过把类型加到GCOptimize生成降低开销==，详细可参见配置介绍文档。

那有没有引用方式的映射呢？有，下面这个就是：

##### 2、映射到一个interface

这种方式==依赖于生成代码==（==如果没生成代码会抛InvalidCastException异常==），代码生成器会生成这个interface的实例，如果get一个属性，生成代码会get对应的table字段，如果set属性也会设置对应的字段。甚至可以通过interface的方法访问lua的函数。

##### 3、更轻量级的by value方式：映射到Dictionary<>，List<>

不想定义class或者interface的话，可以考虑用这个，前提table下key和value的类型都是一致的。

##### 4、另外一种by ref方式：映射到LuaTable类

这种方式好处是不需要生成代码，但也有一些问题，比如慢，比方式2要慢一个数量级，比如没有类型检查。

- ## 三、访问一个全局的function

==仍然是用Get方法，不同的是类型映射。==

- **1、映射到delegate**

这种是建议的方式，==性能好很多，而且类型安全==。==缺点是要生成代码==（如果没生成代码会抛InvalidCastException异常）。

delegate要怎样声明呢？

==对于function的每个参数就声明一个输入类型的参数。==

==**多返回值要怎么处理？从左往右映射到c#的输出参数，输出参数包括返回值，out参数，ref参数。
参数、返回值类型支持哪些呢？都支持，各种复杂类型，out，ref修饰的，甚至可以返回另外一个delegate。**==

delegate的使用就更简单了，直接像个函数那样用就可以了。

- **2、映射到LuaFunction**

这种方式的==优缺点刚好和第一种相反==。

使用也简单，==LuaFunction上有个变参的Call函数，可以传任意类型，任意个数的参数，返回值是object的数组，对应于lua的多返回值。==

- ## 四、使用建议

- 1、==访问lua全局数据，特别是table以及function，代价比较大，建议尽量少做==，比如==在初始化时把要调用的lua function获取一次（映射到delegate）后，保存下来，后续直接调用该delegate即可==。==table也类似==。
- 2、==**++如果lua测的实现的部分都以delegate和interface的方式提供，使用方可以完全和xLua解耦：由一个专门的模块负责xlua的初始化以及delegate、interface的映射，然后把这些delegate和interface设置到要用到它们的地方++**==。

# Lua调用C#

本章节涉及到的实例均在XLua\Tutorial\LuaCallCSharp下

## new C#对象

你在C#这样new一个对象：


```
var newGameObj = new UnityEngine.GameObject();
```


对应到Lua是这样：

```
local newGameObj = CS.UnityEngine.GameObject()
```


基本类似，除了：

1、==lua里头没有new关键字==；

2、==所有C#相关的都放到CS下，包括构造函数，静态成员属性、方法==；

==如果有多个构造函数呢？放心，xlua支持重载==，比如你要调用GameObject的带一个string参数的构造函数，这么写：

```
local newGameObj2 = CS.UnityEngine.GameObject('helloworld')
```


## 访问C#静态属性，方法

读静态属性


```
CS.UnityEngine.Time.deltaTime
```


写静态属性


```
CS.UnityEngine.Time.timeScale = 0.5
```


调用静态方法


```
CS.UnityEngine.GameObject.Find('helloworld')
```


小技巧：如果==需要经常访问的类，可以先用局部变量引用后访问，除了减少敲代码的时间，还能提高性能==：


```
local GameObject = CS.UnityEngine.GameObject
GameObject.Find('helloworld')
```

## 访问C#成员属性，方法

读成员属性


```
testobj.DMF
```


写成员属性


```
testobj.DMF = 1024
```


调用成员方法

注意：调用成员方法，第一个参数需要传该对象，建议用冒号语法糖，如下


```
testobj:DMFunc()
```


- 父类属性，方法

==xlua支持（通过派生类）访问基类的静态属性，静态方法，（通过派生类实例）访问基类的成员属性，成员方法==

### 参数的输入输出属性（out，ref）

Lua**调用测的参数处理**规则：==C#的普通参数算一个输入形参，**ref修饰的算一个输入形参**，**out不算**，然后**从左往右对应lua 调用测的实参列表**==；

Lua**调用测的返回值处理**规则：==C#函数的**返回值（如果有的话）算一个返回值**，**out算一个返回值**，**ref算一个返回值**，然后**从左往右对应lua的多返回值**==。

- 重载方法

直接通过不同的参数类型进行重载函数的访问，例如：


```
testobj:TestFunc(100)

testobj:TestFunc('hello')
```


将分别访问整数参数的TestFunc和字符串参数的TestFunc。

注意：==xlua只一定程度上支持重载函数的调用，因为lua的类型远远不如C#丰富，存在一对多的情况==，比==如C#的int，float，double都对应于lua的number==，上面的例子中TestFunc==如果有这些重载参数，第一行将无法区分开来==，==只能调用到其中一个==（==生成代码中排前面的那个==）

- 操作符

#### 支持的操作符有：

###  +，-，*，/，= =，一元-，<，<=， %，[] 



- 参数带默认值的方法

==和C#调用有默认值参数的函数一样，如果所给的实参少于形参，则会用默认值补上==。

可变参数方法

对于C#的如下方法：


```
void VariableParamsFunc(int a, params string[] strs)
```


可以在lua里头这样调用：


```
testobj:VariableParamsFunc(5, 'hello', 'john')
```


- 使用Extension methods(扩展方法)

==在C#里定义了，lua里就能直接使用。==

- ==**泛化（模版）方法**==

==不直接支持，可以通过Extension methods功能进行封装后调用==。

枚举类型

==枚举值就像枚举类型下的静态属性一样==。


```
testobj:EnumTestFunc(CS.Tutorial.TestEnum.E1)
```


上面的EnumTestFunc函数参数是Tutorial.TestEnum类型的

另外，==如果枚举类加入到生成代码的话，枚举类将支持__CastFrom方法，可以实现从一个整数或者字符串到枚举值的转换==，例如：


```
CS.Tutorial.TestEnum.__CastFrom(1)
CS.Tutorial.TestEnum.__CastFrom('E1')
```


- delegate使用（调用，+，-）

==C#的delegate调用：和调用普通lua函数一样==

+操作符：==对应C#的+操作符，把两个调用串成一个调用链，右操作数可以是同类型的C#
delegate或者是lua函数。==

-操作符：==和+相反，把一个delegate从调用链中移除==。

Ps：==delegate属性可以用一个luafunction来赋值==。

- event

==比如testobj里头有个事件定义是这样：public event Action TestEvent;==

==增加事件回调==


```
testobj:TestEvent('+', lua_event_callback)
```


==移除事件回调==


```
testobj:TestEvent('-', lua_event_callback)
```


- 64位整数支持

==Lua53版本64位整数（long，ulong）映射到原生的64未整数==，而==luajit版本，相当于lua5.1的标准，本身不支持64位，xlua做了个64位支持的扩展库，C#的long和ulong都将映射到userdata==：

1、==支持在lua里头进行64位的运算，比较，打印==

2、==支持和lua number的运算，比较==

3、要注意的是，==在64扩展库中，实际上只有int64，ulong也会先强转成long再传递到lua，而对ulong的一些运算，比较，我们采取和java一样的支持方式，提供一组API，详情请看**API文档(附录一)**==。

- C#复杂类型和table的自动转换

==对于一个有无参构造函数的C#复杂类型==，==在lua侧可以直接用一个table来代替==，该==table对应复杂类型的public字段有相应字段即可==，==支持函数参数传递，属性赋值等==，例如：

C#下B结构体（class也支持）定义如下：


```
public struct A
{
    public int a;
}

public struct B
{
    public A b;
    public double c;
}
```

某个类有成员函数如下：


```
void Foo(B b)
```


在lua可以这么调用


```
obj:Foo({b = {a = 100}, c = 200})
```

获取类型（相当于C#的typeof）

比如要获取UnityEngine.ParticleSystem类的Type信息，可以这样


```
typeof(CS.UnityEngine.ParticleSystem)
```

“强”转

==lua没类型，所以不会有强类型语言的“强转”==，但有个有点像的东西：==告诉xlua要用指定的生成代码去调用一个对象==，这在什么情况下能用到呢？有的时候第三方库对外暴露的是一个interface或者抽象类，实现类是隐藏的，这样我们无法对实现类进行代码生成。该实现类将会被xlua识别为未生成代码而用反射来访问，如果这个调用是很频繁的话还是很影响性能的，这时我们就可以把这个interface或者抽象类加到生成代码，然后指定用该生成代码来访问：

==cast(calc, typeof(CS.Tutorial.Calc))==

上面就是==指定用CS.Tutorial.Calc的生成代码来访问calc对象==。


