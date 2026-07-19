# 45_核心框架实现_ResourceReferenceCount

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：实施版

关联文档：

42_核心框架实现_ResourceHandle

43_核心框架实现_ResourceManager_LoadAsync

43A_ResourceHandle失败状态设计修正

44_核心框架实现_InstantiateAsync

---

# 1. 文档目标

建立 Project Aether 资源引用计数系统。

本阶段目标：

* ResourceHandle支持引用计数
* 支持Retain()
* 支持Release()
* 支持资源可回收判断

本阶段不实现：

* ResourceCache
* Addressables释放
* 自动回收
* 对象池集成

---

# 2. 为什么需要引用计数

场景：

```csharp
var playerHandle =
    await ResourceManager.LoadAsync<GameObject>(
        "Character/Player");

var uiHandle =
    await ResourceManager.LoadAsync<GameObject>(
        "UI/UIMain");
```

此时：

```text
Player资源正在被使用

UIMain资源正在被使用
```

不能直接卸载。

---

必须知道：

```text
当前有多少对象正在持有资源
```

因此需要：

```text
ReferenceCount
```

---

# 3. 生命周期设计

加载：

```text
Load

↓

RefCount = 1
```

---

增加引用：

```text
Retain()

↓

RefCount +1
```

---

释放引用：

```text
Release()

↓

RefCount -1
```

---

最终：

```text
RefCount == 0

↓

可回收
```

---

# 4. ResourceHandle扩展

文件：

```text
Assets/GameScripts/Resource/Handles/ResourceHandle.cs
```

新增：

```csharp
public int ReferenceCount
{
    get;
    protected set;
}
```

---

新增：

```csharp
public bool CanRelease
{
    get
    {
        return ReferenceCount <= 0;
    }
}
```

---

# 5. Retain实现

新增：

```csharp
public virtual void Retain()
{
    ReferenceCount++;
}
```

---

# 6. Release实现

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
    if (ReferenceCount > 0)
    {
        ReferenceCount--;
    }

    if (ReferenceCount == 0)
    {
        State =
            ResourceHandleState.Released;
    }
}
```

---

# 7. SetLoaded修正

文件：

```text
ResourceHandleT.cs
```

修改：

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

为：

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

    ReferenceCount = 1;
}
```

---

# 8. SetFailed修正

修改：

```csharp
internal void SetFailed(
    string assetPath,
    string error)
{
    ...
}
```

增加：

```csharp
ReferenceCount = 0;
```

---

# 9. 使用示例

加载：

```csharp
var handle =
    await ResourceManager
        .LoadAsync<GameObject>(
            "Prefabs/Player");
```

状态：

```text
ReferenceCount = 1
```

---

增加引用：

```csharp
handle.Retain();
```

状态：

```text
ReferenceCount = 2
```

---

释放：

```csharp
handle.Release();
```

状态：

```text
ReferenceCount = 1
```

---

再次释放：

```csharp
handle.Release();
```

状态：

```text
ReferenceCount = 0

State = Released
```

---

# 10. 防御性设计

禁止：

```csharp
handle.Release();

handle.Release();

handle.Release();
```

导致：

```text
ReferenceCount = -1
```

---

因此：

```csharp
if (ReferenceCount > 0)
{
    ReferenceCount--;
}
```

必须保留。

---

# 11. Unity验证步骤

测试：

```csharp
var handle =
    await ResourceManager
        .LoadAsync<GameObject>(
            "Prefabs/Player");

Debug.Log(handle.ReferenceCount);
```

预期：

```text
1
```

---

调用：

```csharp
handle.Retain();
```

预期：

```text
2
```

---

调用：

```csharp
handle.Release();
```

预期：

```text
1
```

---

再次：

```csharp
handle.Release();
```

预期：

```text
0
```

---

状态：

```text
Released
```

---

# 12. MVP验收标准

支持：

* ReferenceCount
* Retain()
* Release()
* CanRelease
* 生命周期管理

不支持：

* 自动回收
* Addressables释放
* 资源缓存

---

# 13. Git提交规范

Commit：

```bash
git commit -m "[Resource][Feature] Add reference count system"
```

Tag：

```text
v0.1.19
```

---

# 14. 下一阶段

46_核心框架实现_ResourceCache

实现：

```text
资源缓存

重复加载检测

缓存复用

缓存释放
```

---

# 15. 结论

ResourceHandle 正式具备引用计数能力。

后续：

```text
ResourceCache

AddressablesProvider

对象池系统
```

都将基于引用计数系统工作。

这是 Project Aether 资源生命周期管理体系的核心基础设施。
