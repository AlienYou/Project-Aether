# 43A_ResourceHandle失败状态设计修正

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：修正版

关联文档：

42_核心框架实现_ResourceHandle

43_核心框架实现_ResourceManager_LoadAsync

---

# 1. 修正目标

完善 ResourceHandle 状态机。

解决：

* 资源不存在
* Provider加载失败
* Addressables异常
* 网络异常

无法表达的问题。

---

# 2. 当前问题

现有状态：

```csharp
public enum ResourceHandleState
{
    None,

    Loading,

    Loaded,

    Released,
}
```

问题：

```text
Loading

↓

加载失败

↓

???
```

缺少失败状态。

---

# 3. 状态机升级

修改：

```csharp
namespace ProjectAether.Resource
{
    public enum ResourceHandleState
    {
        None,

        Loading,

        Loaded,

        Failed,

        Released,
    }
}
```

---

# 4. 状态流转

正常流程：

```text
None

↓

Loading

↓

Loaded

↓

Released
```

异常流程：

```text
None

↓

Loading

↓

Failed
```

---

# 5. ResourceHandle扩展

文件：

```text
Assets/GameScripts/Resource/Handles/ResourceHandle.cs
```

新增：

```csharp
public string Error
{
    get;
    protected set;
}
```

---

# 6. SetFailed接口

文件：

```text
Assets/GameScripts/Resource/Handles/ResourceHandleT.cs
```

新增：

```csharp
internal void SetFailed(
    string assetPath,
    string error)
{
    AssetPath = assetPath;

    Error = error;

    Asset = default;

    State =
        ResourceHandleState.Failed;
}
```

---

# 7. SetLoaded最终版

统一修改为：

```csharp
internal void SetLoaded(
    string assetPath,
    T asset)
{
    AssetPath = assetPath;

    Asset = asset;

    Error = null;

    State =
        ResourceHandleState.Loaded;
}
```

---

# 8. Release修正

修改：

```csharp
public virtual void Release()
{
    State =
        ResourceHandleState.Released;
}
```

为：

```csharp
public virtual void Release()
{
    Error = null;

    State =
        ResourceHandleState.Released;
}
```

---

# 9. Provider失败示例

例如：

```csharp
Resources.Load<GameObject>(
    "NotExist");
```

返回：

```csharp
null
```

处理：

```csharp
if (asset == null)
{
    handle.SetFailed(
        assetPath,
        $"Asset Not Found : {assetPath}");

    return handle;
}
```

---

# 10. 调用示例

```csharp
var handle =
    await ResourceManager
        .LoadAsync<GameObject>(
            "NotExist");
```

检查：

```csharp
if (handle.State ==
    ResourceHandleState.Failed)
{
    Log.Error(
        handle.Error);

    return;
}
```

---

# 11. 为什么不用异常表达失败

不推荐：

```csharp
throw new Exception();
```

原因：

```text
资源加载属于业务失败

不是程序错误
```

例如：

```text
配置缺失
资源缺失
网络失败
```

属于预期风险。

---

工业级项目更倾向：

```text
Handle状态
+
错误信息
```

表达失败。

---

# 12. MVP验收标准

支持：

* Failed状态
* Error信息
* SetFailed()
* SetLoaded()
* 失败链路

不支持：

* Retry
* 自动恢复
* 下载重试

---

# 13. Git提交规范

Commit：

```bash
git commit -m "[Resource][Fix] Add failed state for resource handle"
```

Tag：

```text
v0.1.17
```

---

# 14. 对后续阶段影响

44 InstantiateAsync

45 AddressablesProvider

46 引用计数系统

全部直接复用：

```csharp
ResourceHandleState.Failed
```

无需再次修改接口。

---

# 15. 结论

ResourceHandle 正式具备成功与失败双状态表达能力。

资源系统后续所有加载接口统一通过：

```csharp
State
+
Error
```

表达加载结果。

这是 Project Aether 资源框架进入工业级设计的重要一步。
