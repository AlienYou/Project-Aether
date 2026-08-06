# Project Aether 编码规范

> **文件名：** `04_CodingStandard.md`  
> **文档编号：** PAS-004  
> **版本：** v1.0  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **Unity 版本：** Unity 2022.3.51f1c1  
> **最后更新：** 2026-08-06  

---

## 1. 文档目的

本文档定义 Project Aether 的统一编码规范，适用于 Runtime、Editor、Tools、Tests、Build Pipeline 以及 AI 生成代码。

本规范的目标是：

- 保证代码风格统一。
- 保证模块边界清晰。
- 保证代码易于阅读、维护、调试和测试。
- 降低多人协作成本。
- 降低 AI 生成代码与项目现有实现不一致的风险。
- 保证代码、架构、设计和测试文档能够长期同步。
- 为 Project Aether 的商业化开发建立稳定、可执行的工程基线。

---

## 2. 适用范围

本规范适用于：

- C# Runtime 代码。
- Unity `MonoBehaviour`。
- Unity `ScriptableObject`。
- Editor 工具。
- 构建脚本。
- 自动化测试。
- 配置系统。
- 资源系统。
- 网络系统。
- 战斗系统。
- UI 系统。
- AI 生成或修改的代码。
- 第三方库适配层。

本规范不直接约束：

- Shader 代码。
- 原生插件源码。
- 第三方包内部源码。

这些内容应使用对应的专项规范。

---

## 3. 核心原则

### 3.1 Architecture First

代码必须服从已批准的 Architecture 文档。

当代码与架构文档冲突时：

1. 不得静默选择其中一方。
2. 必须明确指出冲突。
3. 由项目负责人确认当前基线。
4. 同步修正文档和代码。
5. 必要时创建 RFC 或 Decision Log。

禁止为了快速实现功能而绕过模块边界。

---

### 3.2 Single Responsibility

一个类、方法或模块只应承担一个明确职责。

正确：

```csharp
public sealed class ResourceManager
{
    public UniTask<ResourceHandle<T>> LoadAsync<T>(AssetKey assetKey, CancellationToken cancellationToken);
    public void Retain(ResourceHandleBase handle);
    public void Release(ResourceHandleBase handle);
}
```

不推荐：

```csharp
public sealed class ResourceManager
{
    public void LoadResource();
    public void ParseConfig();
    public void OpenUI();
    public void SavePlayerData();
}
```

---

### 3.3 Explicit Over Implicit

优先使用显式依赖、显式状态和显式生命周期。

避免：

- 隐式全局状态。
- 隐藏依赖。
- 魔法数字。
- 无法追踪的静态事件。
- 依赖调用顺序但未文档化的逻辑。
- 通过异常控制正常流程。

---

### 3.4 Readability First

代码首先服务于团队成员，而不是只服务于编译器。

应优先保证：

- 代码意图清晰。
- 状态变化可追踪。
- 命名准确。
- 控制流简单。
- 错误路径明确。
- 复杂逻辑可以测试。

禁止为了减少代码行数而牺牲可读性。

---

### 3.5 Consistency Over Preference

当个人偏好与项目规范冲突时，以项目规范为准。

同一项目内的一致性比个人风格更重要。

---

### 3.6 Correctness Before Optimization

先保证正确，再进行优化。

优化前应明确：

- 性能瓶颈。
- 测量数据。
- 影响范围。
- 优化目标。
- 回归风险。

禁止仅凭感觉进行复杂优化。

---

### 3.7 Fail Fast, Recover Clearly

非法状态应尽早暴露。

可以恢复的问题应提供明确恢复策略。

无法恢复的问题应保留足够上下文，并交由上层处理。

---

### 3.8 Code Is Not the Only Source of Truth

代码必须与以下内容保持一致：

- Architecture。
- Design。
- RFC。
- Test。
- Decision Log。
- Project Standard。

---

## 4. 语言与运行环境

Project Aether 当前基线：

```text
Unity 2022.3.51f1c1
C#
.NET Standard 2.1 / Unity 支持范围
```

新增语法前必须确认：

- Unity 当前 C# 编译器是否支持。
- IL2CPP 是否支持。
- 目标平台是否支持。
- 第三方库是否兼容。
- AOT 环境是否安全。

禁止仅因为 IDE 不报错就假设 Unity 构建一定可用。

---

## 5. 文件规范

### 5.1 一个文件一个主要类型

推荐一个 `.cs` 文件只包含一个主要类型。

文件名必须与主要类型名一致。

正确：

```text
ResourceManager.cs
ResourceHandle.cs
IGameModule.cs
```

不推荐：

```text
ResourceSystem.cs
```

文件中同时定义：

```csharp
ResourceManager
ResourceHandle
ResourceCache
ResourceState
```

---

### 5.2 辅助类型

以下类型可以与主要类型放在同一文件：

- 仅由该类型使用的私有嵌套类型。
- 简单私有枚举。
- 与主要类型强绑定且不会独立使用的内部结构。

可以：

```csharp
public sealed class ResourceManager
{
    private enum RequestState
    {
        None,
        Loading,
        Completed,
        Failed
    }
}
```

---

### 5.3 文件编码

所有文本文件统一使用：

```text
UTF-8
```

禁止混用本地编码。

---

### 5.4 文件换行

团队应统一 Git 换行策略。

推荐仓库使用 `.gitattributes` 管理文本文件，避免 Windows 与 macOS/Linux 产生无意义 Diff。

---

## 6. 代码排版

### 6.1 基本风格

使用正常、紧凑、清晰的排版。

不要为了展示而频繁拆行。

方法调用、简单条件表达式和简单赋值尽量保持在一行，仅在以下情况换行：

- 单行明显过长。
- 参数较多。
- 逻辑层级需要突出。
- 换行能显著提高可读性。

正确：

```csharp
var localVelocity = transform.InverseTransformDirection(_rigidbody.velocity);
```

不推荐：

```csharp
var localVelocity =
    transform
        .InverseTransformDirection(
            _rigidbody
                .velocity
        );
```

---

### 6.2 大括号

使用 Allman 风格。

```csharp
public void Initialize()
{
    _state = ModuleState.Initialized;
}
```

---

### 6.3 单行语句

即使语句只有一行，也必须使用大括号。

正确：

```csharp
if (_isInitialized)
{
    return;
}
```

禁止：

```csharp
if (_isInitialized)
    return;
```

---

### 6.4 空行

使用空行分隔逻辑块。

不要在连续相关语句之间插入过多空行。

推荐：

```csharp
ValidateState();

var request = CreateRequest(assetKey);
_requests.Add(assetKey, request);

await request.LoadAsync(cancellationToken);
```

---

### 6.5 行长度

不设置绝对硬限制，但应避免难以阅读的超长行。

当方法参数过多时：

```csharp
public ResourceRequest(
    AssetKey assetKey,
    Type assetType,
    ResourceLoadPolicy loadPolicy,
    CancellationToken cancellationToken)
{
}
```

简单调用可保持单行：

```csharp
_logger.Info($"[Resource] Load success: {assetKey}");
```

---

### 6.6 链式调用

短链保持一行：

```csharp
var module = _modules.FirstOrDefault(item => item.ModuleName == moduleName);
```

复杂链拆分：

```csharp
var activeModules = _modules
    .Where(module => module.State == ModuleState.Running)
    .OrderBy(module => module.Priority)
    .ToArray();
```

---

## 7. using 规范

### 7.1 顺序

推荐顺序：

1. `System`。
2. 第三方库。
3. Unity。
4. Project Aether。

示例：

```csharp
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ProjectAether.Framework.Logging;
using ProjectAether.Framework.Resource;
```

---

### 7.2 未使用 using

提交前必须移除未使用的 `using`。

---

### 7.3 using alias

仅在类型冲突或显著提高可读性时使用。

```csharp
using Object = UnityEngine.Object;
```

禁止滥用别名隐藏真实类型。

---

## 8. Namespace 规范

### 8.1 基本格式

统一使用：

```text
ProjectAether.<Layer>.<Module>
```

示例：

```csharp
namespace ProjectAether.Framework.Resource
{
}
```

```csharp
namespace ProjectAether.Gameplay.Combat
{
}
```

---

### 8.2 层级建议

常见层级：

```text
ProjectAether.Framework
ProjectAether.Gameplay
ProjectAether.Presentation
ProjectAether.Editor
ProjectAether.Tools
ProjectAether.Tests
```

---

### 8.3 Namespace 与目录一致

Namespace 应与目录和 asmdef 保持一致。

例如：

```text
Assets/ProjectAether/Framework/Resource/Runtime/
```

推荐：

```csharp
namespace ProjectAether.Framework.Resource
{
}
```

---

### 8.4 禁止事项

禁止：

- 使用无意义 Namespace。
- 将所有代码放入 `ProjectAether` 根 Namespace。
- 让 Framework 依赖 Gameplay。
- 通过 Namespace 掩盖错误的模块边界。

---

## 9. Assembly Definition 规范

### 9.1 模块隔离

核心模块必须使用 asmdef 隔离。

示例：

```text
ProjectAether.Framework
ProjectAether.Framework.Resource
ProjectAether.Framework.Config
ProjectAether.Gameplay.Combat
ProjectAether.Presentation.UI
```

---

### 9.2 依赖方向

推荐依赖方向：

```text
Presentation
    ↓
Gameplay
    ↓
Framework
```

Framework 不依赖 Gameplay。

Gameplay 不依赖具体 UI 实现。

Editor asmdef 不得被 Runtime asmdef 依赖。

---

### 9.3 禁止循环依赖

发现循环依赖时必须重新设计边界。

常见处理方式：

- 提取接口。
- 提取共享数据类型。
- 使用事件。
- 使用中介服务。
- 调整模块职责。

禁止通过合并所有 asmdef 来逃避循环依赖。

---

### 9.4 Editor 隔离

Editor 代码必须放入 Editor asmdef 或 `Editor/` 目录。

Runtime 代码不得直接引用 `UnityEditor`。

---

## 10. 命名规范

### 10.1 类

使用 PascalCase。

```csharp
ResourceManager
ConfigLoader
CombatController
```

---

### 10.2 接口

使用 `I` 前缀。

```csharp
IGameModule
IResourceLoader
IConfigProvider
```

接口名应描述能力或契约。

---

### 10.3 抽象类

使用 PascalCase，不强制添加 `Base`。

当 `Base` 能明确表达继承用途时可以使用：

```csharp
ResourceHandleBase
ModuleBase
```

禁止所有抽象类都机械添加 `Base`。

---

### 10.4 结构体

使用 PascalCase。

```csharp
AssetKey
DamageResult
NetworkEntityId
```

---

### 10.5 枚举

枚举类型和成员使用 PascalCase。

```csharp
public enum ModuleState
{
    None,
    Created,
    Initialized,
    Running,
    Shutdown
}
```

---

### 10.6 方法

使用 PascalCase，并以动词开头。

```csharp
Initialize()
LoadAsync()
Release()
TryGetHandle()
```

---

### 10.7 异步方法

异步方法必须使用 `Async` 后缀。

```csharp
InitializeAsync()
LoadAsync()
ShutdownAsync()
```

例外：

- C# 语言约定接口。
- Unity 固定消息。
- 第三方接口要求。

---

### 10.8 属性

使用 PascalCase。

```csharp
public ModuleState State { get; private set; }
public bool IsLoaded { get; }
```

---

### 10.9 私有字段

使用 `_camelCase`。

```csharp
private readonly Dictionary<AssetKey, ResourceHandleBase> _handles;
private ModuleState _state;
```

---

### 10.10 静态字段

静态私有字段同样使用 `_camelCase`。

```csharp
private static ResourceManager _instance;
```

不推荐使用 `s_` 前缀，除非项目未来统一修改规范。

---

### 10.11 常量

使用 PascalCase。

```csharp
private const int MaxRetryCount = 3;
private const float DefaultTimeoutSeconds = 10f;
```

---

### 10.12 参数

使用 camelCase。

```csharp
public void Release(ResourceHandleBase resourceHandle)
{
}
```

---

### 10.13 局部变量

使用 camelCase。

```csharp
var resourceHandle = CreateHandle(assetKey);
```

---

### 10.14 bool 命名

布尔值应表达真假含义。

推荐：

```csharp
isInitialized
hasAuthority
canRelease
shouldRetry
```

不推荐：

```csharp
flag
state
check
```

---

### 10.15 集合命名

集合使用复数名词。

```csharp
_modules
_loadedAssets
_pendingRequests
```

---

### 10.16 事件命名

事件使用 PascalCase。

```csharp
public event Action<ResourceHandleBase> Released;
```

当语义明确时可使用：

```csharp
Loaded
Failed
StateChanged
```

---

### 10.17 Try 方法

可能失败且不应抛异常的查询使用 `Try` 前缀。

```csharp
public bool TryGetModule(string moduleName, out IGameModule module)
{
}
```

---

## 11. 访问修饰符

所有成员应显式声明访问修饰符。

推荐优先使用最小权限。

顺序：

```text
private
protected
internal
public
```

只在确实需要时扩大可见性。

禁止为了方便调试将成员改为 `public`。

---

## 12. 类设计

### 12.1 sealed

不需要继承的类应优先声明为 `sealed`。

```csharp
public sealed class ResourceManager
{
}
```

好处：

- 明确设计意图。
- 避免非预期继承。
- 有助于维护和优化。

---

### 12.2 static class

仅用于：

- 无状态工具。
- 纯函数集合。
- 扩展方法。

禁止使用静态类承载复杂全局状态。

---

### 12.3 继承

优先组合，谨慎继承。

使用继承前应确认：

- 是否存在稳定的 is-a 关系。
- 基类是否真正提供共享语义。
- 子类是否能遵守基类契约。
- 是否会产生脆弱基类问题。

---

### 12.4 构造函数

构造函数应：

- 建立有效对象状态。
- 接收必要依赖。
- 避免执行耗时操作。
- 避免异步操作。
- 避免访问未初始化的 Unity 对象。

---

### 12.5 不变式

类必须维护自身不变式。

例如，引用计数不得小于零：

```csharp
public void Release()
{
    if (_referenceCount <= 0)
    {
        throw new InvalidOperationException("Resource handle reference count is already zero.");
    }

    _referenceCount--;
}
```

---

## 13. 接口设计

接口应：

- 小而明确。
- 表达稳定能力。
- 避免暴露不必要细节。
- 不包含无关成员。
- 不为未来假设提前堆积方法。

正确：

```csharp
public interface IGameModule
{
    string ModuleName { get; }
    ModuleState State { get; }

    void Create();
    void Initialize();
    void Update();
    void Shutdown();
}
```

禁止创建万能接口：

```csharp
public interface IManager
{
    void Initialize();
    void Load();
    void Save();
    void Update();
    void Reset();
    void Release();
}
```

---

## 14. 字段与属性

### 14.1 readonly

构造后不再改变的字段使用 `readonly`。

```csharp
private readonly Dictionary<string, IGameModule> _modules;
```

---

### 14.2 属性优先

对外暴露状态时优先使用只读属性。

```csharp
public ModuleState State { get; private set; }
```

不推荐：

```csharp
public ModuleState state;
```

---

### 14.3 计算属性

计算成本高或有副作用的逻辑不应放在属性中。

不推荐：

```csharp
public CharacterData CharacterData => LoadCharacterDataFromDisk();
```

应改为：

```csharp
public CharacterData LoadCharacterData()
{
}
```

---

### 14.4 Unity 序列化字段

需要 Inspector 配置但不希望公开时：

```csharp
[SerializeField] private float _moveSpeed = 5f;
```

禁止为了 Inspector 暴露而使用 public 字段。

---

### 14.5 字段初始化

简单默认值可以内联初始化。

```csharp
private readonly List<IGameModule> _modules = new();
```

依赖外部状态的初始化应放在构造函数或明确生命周期方法中。

---

## 15. 方法设计

### 15.1 方法职责

一个方法只做一件事。

方法过长时，应按语义拆分，而不是机械拆分。

---

### 15.2 参数数量

参数过多通常说明职责不清或需要参数对象。

不推荐：

```csharp
public void Spawn(
    int id,
    string name,
    Vector3 position,
    Quaternion rotation,
    float health,
    float speed,
    int team,
    bool isPlayer)
{
}
```

推荐：

```csharp
public void Spawn(CharacterSpawnRequest request)
{
}
```

---

### 15.3 返回值

返回值应表达结果。

复杂结果使用结果对象。

```csharp
public ResourceLoadResult Load(AssetKey assetKey)
{
}
```

---

### 15.4 Guard Clause

优先使用 Guard Clause 减少嵌套。

推荐：

```csharp
public void Initialize()
{
    if (_state != ModuleState.Created)
    {
        throw new InvalidOperationException($"Cannot initialize module from state {_state}.");
    }

    _state = ModuleState.Initialized;
}
```

不推荐：

```csharp
public void Initialize()
{
    if (_state == ModuleState.Created)
    {
        if (!_isDisposed)
        {
            _state = ModuleState.Initialized;
        }
    }
}
```

---

### 15.5 out 参数

仅在 `Try` 模式或性能敏感场景使用。

```csharp
public bool TryGetHandle(AssetKey assetKey, out ResourceHandleBase handle)
{
}
```

复杂多结果应使用结果类型。

---

### 15.6 optional 参数

可选参数应有稳定、明确的默认值。

公共 API 的默认值变化可能造成兼容问题，修改前必须评估。

---

## 16. var 使用

当右侧类型明确时可以使用 `var`。

推荐：

```csharp
var handle = new ResourceHandle<GameObject>(assetKey);
var module = _modules[moduleName];
```

类型不明确或阅读成本高时写出类型：

```csharp
ResourceHandleBase handle = ResolveHandle(assetKey);
```

禁止为了统一而强制所有局部变量使用或禁止 `var`。

---

## 17. null 处理

### 17.1 明确 null 语义

API 必须明确：

- 是否允许传入 null。
- 是否可能返回 null。
- null 表示什么。
- 调用方如何处理。

---

### 17.2 Unity Object null

Unity `Object` 重载了 null 判断。

使用 Unity 对象时必须理解：

```csharp
if (gameObject == null)
{
}
```

可能表示原生对象已销毁，而托管引用仍存在。

---

### 17.3 参数校验

公共方法应在入口验证关键参数。

```csharp
public void Register(IGameModule module)
{
    if (module == null)
    {
        throw new ArgumentNullException(nameof(module));
    }
}
```

---

### 17.4 Null Object

仅在能够明显简化调用方且语义稳定时使用 Null Object。

禁止通过 Null Object 隐藏真正的初始化错误。

---

## 18. 枚举规范

枚举应：

- 从 `None = 0` 开始，除非协议明确要求其他值。
- 成员语义稳定。
- 不承担复杂状态机逻辑。
- 网络协议枚举值必须显式指定。

示例：

```csharp
public enum ResourceState
{
    None = 0,
    Loading = 1,
    Loaded = 2,
    Failed = 3,
    Released = 4
}
```

---

## 19. 集合规范

### 19.1 选择正确集合

- 顺序访问：`List<T>`。
- 键值查找：`Dictionary<TKey, TValue>`。
- 唯一集合：`HashSet<T>`。
- 队列：`Queue<T>`。
- 栈：`Stack<T>`。

禁止所有场景都使用 `List<T>`。

---

### 19.2 暴露只读集合

对外不应直接暴露可修改内部集合。

推荐：

```csharp
public IReadOnlyList<IGameModule> Modules => _modules;
```

---

### 19.3 集合修改

遍历中修改集合必须谨慎。

需要修改时可以：

- 使用索引倒序遍历。
- 收集待删除项。
- 使用专门队列。
- 在安全阶段统一处理。

---

### 19.4 容量

已知规模时预设容量。

```csharp
var modules = new List<IGameModule>(expectedCount);
```

避免无意义的过度预分配。

---

## 20. LINQ 规范

LINQ 可以用于：

- Editor。
- 初始化。
- 非热点逻辑。
- 可读性明显提高的代码。

Runtime 高频路径中谨慎使用：

- `Update`。
- `FixedUpdate`。
- 每帧 UI 刷新。
- 战斗循环。
- 网络 Tick。
- 资源批量处理。

原因：

- 可能产生 GC。
- 隐藏枚举成本。
- 调试路径不直观。

---

## 21. 字符串规范

热点路径避免频繁字符串拼接。

日志中可以使用插值：

```csharp
_logger.Info($"[Resource] Load success: {assetKey}");
```

高频日志应支持级别控制，避免即使日志关闭仍产生字符串分配。

---

## 22. 异常规范

### 22.1 异常用途

异常用于：

- 非法状态。
- 无法满足契约。
- 不应发生的系统错误。
- 无法在当前层恢复的失败。

异常不应用于正常分支控制。

---

### 22.2 捕获范围

只捕获能够处理的异常。

禁止：

```csharp
try
{
    Execute();
}
catch (Exception)
{
}
```

---

### 22.3 保留上下文

捕获后重新抛出时使用：

```csharp
throw;
```

不要：

```csharp
throw exception;
```

后者会破坏原始堆栈。

---

### 22.4 自定义异常

只有在调用方需要区分错误类型时才创建自定义异常。

不要为每个小问题都创建异常类型。

---

### 22.5 Unity 生命周期异常

`Awake`、`Start`、`Update` 中的异常可能影响整个运行流程。

核心模块应提供明确日志和失败状态。

---

## 23. Result 模式

可预期失败可以使用结果类型。

示例：

```csharp
public readonly struct ResourceLoadResult<T>
{
    public bool IsSuccess { get; }
    public T Asset { get; }
    public string Error { get; }
}
```

适合：

- 资源不存在。
- 配置校验失败。
- 网络请求失败。
- 用户输入错误。

不适合隐藏程序错误或非法状态。

---

## 24. 日志规范

### 24.1 日志格式

统一格式：

```text
[Module] Message
```

示例：

```text
[Resource] Load success: Player.prefab
[Config] Duplicate key detected: Character_1001
```

---

### 24.2 日志级别

推荐级别：

```text
Trace
Debug
Info
Warning
Error
Fatal
```

Release 构建应能够关闭或过滤低级别日志。

---

### 24.3 日志内容

日志应包含：

- 模块。
- 操作。
- 关键对象。
- 状态。
- 错误原因。
- 必要的上下文标识。

---

### 24.4 禁止事项

禁止：

```csharp
Debug.Log("here");
Debug.Log("test");
Debug.Log("111");
```

禁止在高频循环中无条件输出日志。

禁止记录敏感信息。

---

## 25. 注释规范

### 25.1 注释说明为什么

注释重点说明：

- 为什么这样设计。
- 有什么限制。
- 为什么不能使用更直观方案。
- 与外部系统有什么兼容要求。
- 哪些行为是刻意设计。

不推荐：

```csharp
// 增加引用计数
_referenceCount++;
```

推荐：

```csharp
// The cache keeps one internal reference while the asset remains reusable.
_referenceCount++;
```

---

### 25.2 XML 注释

以下成员建议使用 XML 注释：

- 公共接口。
- 公共类型。
- 复杂公共方法。
- 容易误用的 API。
- 框架扩展点。

---

### 25.3 TODO

TODO 必须包含责任或关联任务。

推荐：

```csharp
// TODO(PA-231): Replace linear lookup after module count exceeds threshold.
```

禁止：

```csharp
// TODO: fix later
```

---

### 25.4 注释掉的代码

禁止长期保留大段注释代码。

历史由 Git 管理。

---

## 26. Unity MonoBehaviour 规范

### 26.1 职责

`MonoBehaviour` 优先负责：

- Unity 生命周期桥接。
- Inspector 配置。
- Unity 组件引用。
- 场景事件转发。
- 表现层控制。

复杂业务逻辑应放入普通 C# 类。

---

### 26.2 生命周期方法顺序

推荐按以下顺序组织：

```text
Awake
OnEnable
Start
Update
LateUpdate
FixedUpdate
OnDisable
OnDestroy
```

仅实现实际需要的方法。

---

### 26.3 Awake

适合：

- 获取本地组件。
- 建立对象内部引用。
- 初始化不依赖其他对象的状态。

不适合：

- 执行复杂异步流程。
- 假设其他对象的 `Awake` 顺序。
- 访问尚未完成初始化的服务。

---

### 26.4 Start

适合：

- 执行依赖场景其他对象已经完成 `Awake` 的初始化。
- 启动明确的运行流程。

仍不应依赖无法保证的对象顺序。

---

### 26.5 OnEnable / OnDisable

事件订阅与取消订阅必须成对。

```csharp
private void OnEnable()
{
    _inputService.MoveChanged += OnMoveChanged;
}

private void OnDisable()
{
    _inputService.MoveChanged -= OnMoveChanged;
}
```

---

### 26.6 OnDestroy

用于：

- 释放本对象拥有的资源。
- 取消订阅。
- 取消异步任务。
- 注销服务。

不要在 `OnDestroy` 依赖其他对象仍然可用。

---

## 27. Update 规范

### 27.1 避免无意义 Update

没有每帧逻辑时不要保留空 `Update`。

---

### 27.2 高频逻辑

高频逻辑应：

- 避免 GC。
- 避免 LINQ。
- 避免重复查找组件。
- 避免字符串拼接。
- 避免不必要的虚方法调用链。
- 避免大量临时集合。

---

### 27.3 Update 管理

大量系统级 Update 应考虑统一调度。

例如：

```text
BootstrapRunner
  ↓
ModuleManager.UpdateAll()
```

统一调度必须有明确顺序和生命周期。

---

### 27.4 FixedUpdate

物理相关操作使用 `FixedUpdate`，但必须理解：

- `FixedUpdate` 频率与帧率不同。
- 输入采集通常在 `Update`。
- Rigidbody 力和速度操作应遵循物理更新节奏。

---

## 28. Unity 组件引用

优先：

1. Inspector 显式引用。
2. 构造或初始化注入。
3. `GetComponent` 缓存。
4. 服务解析。

避免：

```csharp
GameObject.Find()
Object.FindObjectOfType()
```

禁止每帧调用查找 API。

---

## 29. SerializeField 规范

Inspector 字段：

```csharp
[SerializeField] private Rigidbody _rigidbody;
```

字段应：

- 有明确名称。
- 有合理默认值。
- 必要时使用 `Tooltip`。
- 必要时使用 `Min`、`Range`。
- 在 `OnValidate` 中验证。

禁止通过 Inspector 配置关键安全信息。

---

## 30. OnValidate 规范

`OnValidate` 仅用于 Editor 下轻量验证。

禁止：

- 执行耗时操作。
- 修改大量场景对象。
- 访问未准备好的 Runtime 服务。
- 产生不可预测副作用。

---

## 31. ScriptableObject 规范

适合：

- 只读配置。
- 编辑器可配置数据。
- 可复用资源定义。
- 策划参数。

不适合：

- 保存运行时易变状态。
- 作为无约束全局单例。
- 承载复杂生命周期。
- 直接存储玩家存档。

---

## 32. Prefab 与 Scene 代码规范

脚本不得依赖脆弱的层级路径。

不推荐：

```csharp
transform.Find("Root/Body/Weapon/Socket");
```

优先显式引用或稳定组件标识。

场景对象依赖必须有验证机制。

---

## 33. 异步编程规范

Project Aether Runtime 异步统一优先使用 UniTask。

---

### 33.1 Async 后缀

所有异步方法使用 `Async` 后缀。

```csharp
public UniTask InitializeAsync(CancellationToken cancellationToken)
{
}
```

---

### 33.2 CancellationToken

长生命周期、场景生命周期或对象生命周期异步任务必须支持取消。

```csharp
public async UniTask LoadAsync(AssetKey assetKey, CancellationToken cancellationToken)
{
    cancellationToken.ThrowIfCancellationRequested();
}
```

---

### 33.3 生命周期 Token

Unity 对象异步任务应绑定对象生命周期。

可以使用：

```csharp
this.GetCancellationTokenOnDestroy()
```

但仍需明确取消语义。

---

### 33.4 禁止 async void

除以下情况外禁止 `async void`：

- Unity UI 事件桥接。
- 必须匹配第三方 `void` 委托。

即使使用，也必须在内部处理异常。

---

### 33.5 UniTaskVoid

只有明确 fire-and-forget 时使用 `UniTaskVoid`，并必须处理异常。

---

### 33.6 Forget

使用 `.Forget()` 前必须明确：

- 不需要等待结果。
- 异常如何处理。
- 生命周期如何取消。
- 重复调用是否安全。

---

### 33.7 并行任务

使用 `UniTask.WhenAll` 前应确认：

- 任务可以并行。
- 失败策略明确。
- 取消策略明确。
- 结果顺序明确。

---

### 33.8 超时

可能无限等待的任务必须考虑超时。

超时必须区分：

- 用户取消。
- 生命周期取消。
- 网络超时。
- 系统失败。

---

## 34. 线程规范

默认假设 Unity API 只能在主线程调用。

后台线程不得直接访问：

- GameObject。
- Transform。
- Component。
- UnityEngine.Object。
- 大多数 Unity API。

后台线程适合：

- 纯数据计算。
- 压缩解压。
- 不涉及 Unity API 的解析。
- 可验证的 CPU 密集任务。

切回主线程后再操作 Unity 对象。

---

## 35. VContainer 规范

### 35.1 依赖显式注入

优先构造函数注入。

```csharp
public sealed class CombatService
{
    private readonly ITimeService _timeService;

    public CombatService(ITimeService timeService)
    {
        _timeService = timeService;
    }
}
```

---

### 35.2 禁止服务定位滥用

不要在业务代码中到处解析容器。

容器配置应集中在 LifetimeScope 或 Composition Root。

---

### 35.3 生命周期

注册前必须明确：

- Singleton。
- Scoped。
- Transient。

禁止无意识地把所有服务注册为 Singleton。

---

### 35.4 MonoBehaviour 注入

场景组件注入必须考虑：

- 场景加载顺序。
- LifetimeScope 范围。
- 对象销毁。
- 动态实例化。

---

## 36. Addressables 规范

### 36.1 封装访问

业务模块不应直接到处调用 Addressables API。

应通过 Resource Module 统一访问。

推荐：

```text
Gameplay
  ↓
ResourceManager
  ↓
Addressables Adapter
```

---

### 36.2 Handle 生命周期

每次加载必须明确：

- 谁拥有 Handle。
- 谁增加引用。
- 谁释放。
- 何时卸载。
- 失败状态如何处理。

---

### 36.3 禁止重复释放

所有资源 Handle 必须防止重复释放和引用计数下溢。

---

### 36.4 异步失败

Addressables 失败必须保留：

- AssetKey。
- 资源类型。
- 原始错误。
- 当前状态。
- 请求来源。

---

### 36.5 Instantiate 与 LoadAsset

必须区分：

- 加载资源。
- 实例化对象。
- 释放实例。
- 释放资源。

禁止混淆 `ReleaseInstance` 与资源 Handle 的释放。

---

## 37. FishNet 规范

### 37.1 网络权威

任何网络逻辑必须明确：

- Server Authority。
- Client Authority。
- Prediction。
- Reconciliation。
- Ownership。

---

### 37.2 网络状态与本地状态

不得将本地表现状态直接等同于权威网络状态。

---

### 37.3 RPC

RPC 应：

- 名称明确。
- 参数尽量精简。
- 避免高频大对象传输。
- 明确调用方向。
- 明确权限检查。

---

### 37.4 网络序列化

网络结构必须：

- 字段顺序稳定。
- 版本兼容。
- 尽量避免引用类型。
- 避免不必要字符串。
- 明确精度和范围。

---

## 38. 配置系统规范

### 38.1 配置只读

Runtime 配置加载后应视为只读。

---

### 38.2 类型安全

配置访问应使用明确类型。

```csharp
CharacterConfig config = _configManager.Get<CharacterConfig>(characterId);
```

---

### 38.3 Key 唯一

导表阶段和运行时都必须验证 Key 唯一。

---

### 38.4 配置校验

配置必须验证：

- 必填字段。
- ID 范围。
- 引用关系。
- 枚举合法性。
- 数值范围。
- 重复项。
- 循环依赖。

---

## 39. 资源系统规范

### 39.1 单一入口

资源加载必须通过统一入口。

禁止业务模块绕过 ResourceManager 直接加载。

---

### 39.2 生命周期

统一流程：

```text
Request
  ↓
Load
  ↓
Retain
  ↓
Use
  ↓
Release
  ↓
Cache / Unload
```

---

### 39.3 状态

Handle 状态必须明确。

示例：

```csharp
public enum ResourceState
{
    None,
    Loading,
    Loaded,
    Failed,
    Released
}
```

状态转换必须可验证。

---

### 39.4 错误

失败 Handle 必须保留：

- AssetKey。
- Error。
- State。
- 请求类型。

禁止只返回 null 丢失错误原因。

---

## 40. Pool 规范

对象池必须明确：

- 创建方式。
- 获取方式。
- 归还方式。
- 重置方式。
- 容量限制。
- 销毁策略。
- 重复归还检测。

池对象必须在归还时恢复可复用状态。

---

## 41. 事件规范

### 41.1 订阅与取消

订阅和取消必须成对。

---

### 41.2 所有权

事件发布者和订阅者生命周期必须明确。

---

### 41.3 静态事件

谨慎使用静态事件。

风险：

- 生命周期泄漏。
- 隐式依赖。
- 场景切换后残留。
- 测试互相污染。

---

### 41.4 事件参数

复杂事件使用专用 EventArgs 或数据结构。

---

## 42. 状态机规范

状态机必须明确：

- 状态列表。
- 初始状态。
- 合法转换。
- 进入行为。
- 更新行为。
- 退出行为。
- 异常状态。
- 终止状态。

禁止通过多个 bool 组合隐式表达复杂状态。

---

## 43. 性能规范

### 43.1 先测量

性能优化必须基于：

- Profiler。
- Memory Profiler。
- Frame Debugger。
- Deep Profile。
- 自定义统计。
- 真机数据。

---

### 43.2 GC

高频路径避免：

- 装箱。
- LINQ。
- 闭包。
- 临时字符串。
- 临时数组。
- 频繁 new。
- 反复创建委托。

---

### 43.3 缓存

可以缓存：

- 组件引用。
- 常用查找结果。
- 稳定配置。
- 高频计算中间值。

禁止缓存可能失效但没有失效机制的数据。

---

### 43.4 对象池

适合：

- 高频创建销毁对象。
- 特效。
- 子弹。
- 飘字。
- 临时 UI。
- 网络实体表现对象。

不应为了“看起来专业”而给所有对象加池。

---

### 43.5 Update 数量

大量 MonoBehaviour Update 会增加调度成本。

系统级对象应评估统一 Tick。

---

### 43.6 Physics

物理性能应关注：

- Collider 数量。
- Layer Collision Matrix。
- Raycast 频率。
- Rigidbody Sleep。
- Fixed Timestep。
- MeshCollider 使用。
- Continuous Collision Detection。

---

## 44. 内存规范

### 44.1 所有权

所有长期对象必须明确由谁持有、由谁释放。

---

### 44.2 IDisposable

拥有非托管资源、订阅或显式生命周期资源的普通 C# 对象可以实现 `IDisposable`。

---

### 44.3 Unity Object

Unity 对象销毁使用 `Object.Destroy`，不能只清空托管引用。

---

### 44.4 资源泄漏检查

场景切换、模块关闭和重复进入流程必须检查：

- 资源 Handle。
- 事件订阅。
- CancellationToken。
- 静态缓存。
- 对象池。
- 网络对象。
- Addressables 实例。

---

## 45. 编辑器代码规范

Editor 代码必须：

- 与 Runtime 隔离。
- 支持 Undo。
- 避免破坏资源。
- 提供明确错误提示。
- 批处理前验证输入。
- 重要操作提供确认。
- 修改资源后正确标记 Dirty。

---

## 46. 自动化测试规范

代码设计应支持测试。

优先：

- 依赖接口。
- 纯 C# 逻辑。
- 可注入时间。
- 可注入随机数。
- 可替换资源加载器。
- 可替换网络层。

测试命名建议：

```csharp
MethodName_Condition_ExpectedResult
```

示例：

```csharp
Release_WhenReferenceCountIsZero_ThrowsException()
```

---

## 47. 测试代码质量

测试代码同样遵循：

- 命名规范。
- 单一职责。
- 清晰 Arrange / Act / Assert。
- 避免相互依赖。
- 避免共享污染状态。
- 失败信息明确。

---

## 48. 条件编译

条件编译必须有明确用途。

示例：

```csharp
#if UNITY_EDITOR
#endif
```

禁止通过大量条件编译拼接复杂业务逻辑。

平台差异应优先通过适配层隔离。

---

## 49. 第三方库规范

引入第三方库前必须评估：

- 功能必要性。
- License。
- 活跃度。
- Unity 版本兼容。
- IL2CPP 兼容。
- AOT 兼容。
- 平台支持。
- 包体影响。
- 性能影响。
- 替代方案。
- 更新和移除成本。

业务代码应尽量通过适配层依赖第三方库。

---

## 50. API 兼容性

修改公共 API 前必须确认：

- 调用方。
- 序列化数据。
- 存档。
- 网络协议。
- Editor 工具。
- 自动化测试。
- 文档。
- AI 上下文。

破坏性变更必须通过 RFC 或明确评审。

---

## 51. 序列化规范

Unity 序列化字段重命名时应评估：

```csharp
[FormerlySerializedAs("_oldName")]
[SerializeField] private float _newName;
```

禁止随意修改已用于 Prefab、Scene、ScriptableObject 的字段名。

---

## 52. 时间与随机数

业务逻辑不要直接依赖：

```csharp
Time.time
UnityEngine.Random
DateTime.Now
```

需要测试或网络一致性时，应通过接口注入：

```csharp
ITimeService
IRandomService
```

---

## 53. 浮点数

浮点比较避免直接使用 `==`。

```csharp
if (Mathf.Abs(a - b) <= tolerance)
{
}
```

阈值必须有语义，不使用无解释魔法数。

---

## 54. 魔法数字

禁止：

```csharp
if (speed > 17.5f)
{
}
```

推荐：

```csharp
private const float DriftActivationSpeed = 17.5f;
```

或配置化。

---

## 55. 锁与并发

只有明确存在多线程访问时才使用锁。

必须说明：

- 被保护的数据。
- 锁粒度。
- 死锁风险。
- 主线程影响。
- 是否能改为消息队列。

禁止在 Unity 主线程长时间持锁。

---

## 56. Debug 与 Release

Debug 功能必须：

- 可关闭。
- 不影响正式逻辑。
- 不泄露敏感信息。
- 不产生明显性能开销。
- 不改变网络权威结果。

---

## 57. 编译警告

项目代码应尽量保持无新增警告。

禁止通过全局关闭警告掩盖问题。

确实需要忽略时，应：

- 限定最小范围。
- 写明原因。
- 关联任务。
- 定期复审。

---

## 58. Obsolete 规范

废弃 API 使用：

```csharp
[Obsolete("Use LoadAsync instead.")]
public void Load()
{
}
```

废弃流程应包含：

1. 标记。
2. 提供替代 API。
3. 迁移调用方。
4. 更新文档。
5. 在后续版本删除。

---

## 59. AI 生成代码规范

AI 生成或修改代码前必须读取：

1. `00_ProjectStandard`。
2. 当前模块 Architecture。
3. 当前模块 Design。
4. 当前真实源码。
5. 相关测试。

---

### 59.1 禁止重新发明接口

AI 不得在未说明的情况下：

- 修改已有接口签名。
- 改变继承关系。
- 删除已有状态。
- 重命名公共成员。
- 改变生命周期。
- 替换已确定的第三方库。
- 引入新的全局单例。

---

### 59.2 增量修改

AI 应基于现有版本进行增量修改。

必须明确：

- 修改文件。
- 修改位置。
- 新增成员。
- 删除成员。
- 行为变化。
- 测试方式。

---

### 59.3 可编译声明

AI 只有在实际验证后才能声称代码可编译。

无法验证时必须说明：

```text
以下代码基于当前接口设计，但尚未在 Unity 2022.3.51f1c1 中实际编译验证。
```

---

### 59.4 一致性检查

AI 输出前必须检查：

- 命名是否一致。
- Namespace 是否一致。
- 接口是否一致。
- 状态枚举是否一致。
- 生命周期是否一致。
- 文档是否一致。
- 测试是否需要更新。

---

### 59.5 代码排版

AI 输出代码必须遵循本项目紧凑排版要求。

避免为了展示而将简单表达式拆成多行。

---

## 60. Code Review 规范

Review 重点：

- 是否符合架构。
- 职责是否清晰。
- 生命周期是否明确。
- 状态是否安全。
- API 是否稳定。
- 错误是否可诊断。
- 异步是否可取消。
- 是否存在泄漏。
- 是否产生不必要 GC。
- 是否可测试。
- 文档是否同步。

---

## 61. Code Review Checklist

### Architecture

- [ ] 代码符合当前 Architecture。
- [ ] 模块边界没有被绕过。
- [ ] 依赖方向正确。
- [ ] 没有新增循环依赖。
- [ ] 公共 API 变化已评审。

### Naming

- [ ] 类型命名正确。
- [ ] 接口使用 `I` 前缀。
- [ ] 私有字段使用 `_camelCase`。
- [ ] 异步方法使用 `Async` 后缀。
- [ ] bool 命名表达真假含义。
- [ ] 集合使用复数名称。

### Lifecycle

- [ ] 初始化顺序明确。
- [ ] 关闭顺序明确。
- [ ] 重复初始化有保护。
- [ ] 重复释放有保护。
- [ ] 状态转换合法。
- [ ] 对象所有权明确。

### Async

- [ ] 支持 CancellationToken。
- [ ] 没有不必要的 `async void`。
- [ ] fire-and-forget 有异常处理。
- [ ] 生命周期取消正确。
- [ ] 超时策略明确。
- [ ] Unity API 仅在主线程调用。

### Unity

- [ ] MonoBehaviour 职责轻量。
- [ ] 没有每帧查找组件。
- [ ] 事件订阅和取消成对。
- [ ] Inspector 字段使用 `[SerializeField] private`。
- [ ] Editor 代码与 Runtime 隔离。
- [ ] 序列化字段变更已考虑兼容。

### Performance

- [ ] 热点路径无明显 GC。
- [ ] 没有无意义 LINQ。
- [ ] 没有高频字符串分配。
- [ ] 没有无意义 Update。
- [ ] 集合容量合理。
- [ ] 优化有数据支持。

### Error Handling

- [ ] 参数校验完整。
- [ ] 异常保留上下文。
- [ ] 没有空 catch。
- [ ] 可预期失败有明确结果。
- [ ] 日志包含模块和关键参数。
- [ ] 没有敏感信息日志。

### Tests

- [ ] 新行为有测试。
- [ ] Bug 有回归测试。
- [ ] 测试命名清晰。
- [ ] 测试相互独立。
- [ ] 关键边界已覆盖。
- [ ] 测试结果已记录。

### Documentation

- [ ] Architecture 已同步。
- [ ] Design 已同步。
- [ ] RFC 已关联。
- [ ] Test 文档已更新。
- [ ] 注释与实现一致。
- [ ] Git Commit 符合规范。

---

## 62. 提交前自检

开发者提交前至少执行：

- [ ] 检查 Git Diff。
- [ ] 移除临时调试代码。
- [ ] 清理未使用 using。
- [ ] 确认代码格式。
- [ ] 编译通过。
- [ ] 运行相关测试。
- [ ] 检查 Console 警告和错误。
- [ ] 检查生命周期。
- [ ] 检查资源释放。
- [ ] 更新相关文档。
- [ ] 编写准确 Commit 信息。

---

## 63. 验收标准

本规范执行后，应达到：

- 项目代码风格统一。
- 模块边界清晰。
- 生命周期可追踪。
- 异步任务可取消。
- 错误能够定位。
- Runtime 热点路径无明显无意义分配。
- Unity 组件职责清晰。
- 第三方库被适配层隔离。
- 代码可以被自动化测试。
- AI 能够基于稳定规则生成和修改代码。
- 新成员能够快速理解 Project Aether 的代码结构。
- 代码、文档和 Git 历史保持一致。

---

## 64. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-08-06 | 创建 Project Aether 编码规范正式初稿 |

---

# End
