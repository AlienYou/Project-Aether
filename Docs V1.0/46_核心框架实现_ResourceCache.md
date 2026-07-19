# 46_核心框架实现_ResourceCache

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：实施版

关联文档：

40_核心框架实现_ResourceManager

42_核心框架实现_ResourceHandle

43_核心框架实现_ResourceManager_LoadAsync

45_核心框架实现_ResourceReferenceCount

---

# 1. 文档目标

建立 Project Aether 第一版资源缓存系统。

解决：

```text
同一资源重复加载

重复创建Handle

重复访问Provider
```

问题。

---

# 2. 当前问题

现有实现：

```csharp
var h1 =
    await ResourceManager.LoadAsync<GameObject>(
        "Prefabs/Player");

var h2 =
    await ResourceManager.LoadAsync<GameObject>(
        "Prefabs/Player");
```

执行：

```text
LoadAsync

↓

Provider.Load

↓

创建Handle

↓

返回
```

第二次：

```text
LoadAsync

↓

Provider.Load

↓

创建Handle

↓

返回
```

---

结果：

```text
相同资源

重复加载
```

---

# 3. 设计目标

第一次：

```text
LoadAsync

↓

Provider.Load

↓

Create Handle

↓

加入缓存
```

---

第二次：

```text
LoadAsync

↓

Cache Hit

↓

Retain()

↓

返回Handle
```

---

# 4. 架构设计

结构：

```text
Game Logic

↓

ResourceManager

↓

ResourceCache

↓

Provider

↓

Handle
```

---

职责：

ResourceManager

```text
资源系统入口
```

---

ResourceCache

```text
缓存管理

重复加载检测

引用计数管理
```

---

Provider

```text
真正资源加载
```

---

# 5. 工程目录

新增：

```text
Assets/GameScripts

└── Resource
    │
    ├── Runtime
    │   ├── ResourceManager.cs
    │   └── ResourceCache.cs
    │
    ├── Providers
    │
    ├── Handles
    │
    └── ProjectAether.Resource.asmdef
```

---

# 6. ResourceKey设计

新增：

```csharp
using System;

namespace ProjectAether.Resource
{
    internal readonly struct ResourceKey
    {
        public readonly string AssetPath;

        public readonly Type AssetType;

        public ResourceKey(
            string assetPath,
            Type assetType)
        {
            AssetPath = assetPath;
            AssetType = assetType;
        }
    }
}
```

---

# 7. 为什么不用字符串Key

错误：

```csharp
Dictionary<string, ResourceHandle>
```

例如：

```text
Player
```

可能同时存在：

```csharp
GameObject

AudioClip

TextAsset
```

产生冲突。

---

正确：

```csharp
ResourceKey
```

唯一标识：

```text
路径

+

类型
```

---

# 8. ResourceCache实现

文件：

```text
Assets/GameScripts/Resource/Runtime/ResourceCache.cs
```

代码：

```csharp
using System;
using System.Collections.Generic;

namespace ProjectAether.Resource
{
    internal static class ResourceCache
    {
        private static readonly Dictionary<
            ResourceKey,
            ResourceHandle> Cache
                = new();

        public static bool TryGet(
            ResourceKey key,
            out ResourceHandle handle)
        {
            return Cache.TryGetValue(
                key,
                out handle);
        }

        public static void Add(
            ResourceKey key,
            ResourceHandle handle)
        {
            Cache[key] = handle;
        }

        public static void Clear()
        {
            Cache.Clear();
        }
    }
}
```

---

# 9. ResourceManager改造

修改：

```csharp
LoadAsync<T>()
```

流程。

---

新增：

```csharp
var key =
    new ResourceKey(
        assetPath,
        typeof(T));
```

---

检查缓存：

```csharp
if (ResourceCache.TryGet(
        key,
        out var cacheHandle))
{
    cacheHandle.Retain();

    return
        UniTask.FromResult(
            (ResourceHandle<T>)cacheHandle);
}
```

---

缓存未命中：

```csharp
var handle =
    await _provider.LoadAsync<T>(
        assetPath);
```

---

成功后：

```csharp
if (handle.State ==
    ResourceHandleState.Loaded)
{
    ResourceCache.Add(
        key,
        handle);
}
```

---

返回：

```csharp
return handle;
```

---

# 10. 生命周期

第一次：

```text
LoadAsync

↓

Provider.Load

↓

RefCount = 1

↓

Add Cache
```

---

第二次：

```text
LoadAsync

↓

Cache Hit

↓

Retain()

↓

RefCount = 2
```

---

释放：

```text
Release

↓

RefCount = 1
```

---

再次释放：

```text
Release

↓

RefCount = 0

↓

CanRelease = true
```

---

注意：

```text
本阶段

不移除缓存
```

---

# 11. 为什么暂不自动移除缓存

例如：

```text
打开UI

↓

关闭UI

↓

再次打开UI
```

---

如果：

```text
RefCount=0

立即卸载
```

会导致：

```text
频繁加载

频繁GC

性能下降
```

---

因此：

```text
缓存保留
```

---

真正回收：

```text
下一阶段

ResourceGC
```

负责。

---

# 12. ResourceModule修改

初始化：

```csharp
public void Initialize()
{
    ResourceManager.Initialize();

    State =
        ModuleState.Initialized;
}
```

---

关闭：

```csharp
public void Shutdown()
{
    ResourceCache.Clear();

    ResourceManager.Shutdown();

    State =
        ModuleState.Shutdown;
}
```

---

# 13. Unity验证

测试：

```csharp
var h1 =
    await ResourceManager
        .LoadAsync<GameObject>(
            "Prefabs/Player");

var h2 =
    await ResourceManager
        .LoadAsync<GameObject>(
            "Prefabs/Player");
```

---

验证：

```csharp
Debug.Log(
    ReferenceEquals(
        h1,
        h2));
```

预期：

```text
True
```

---

验证：

```csharp
Debug.Log(
    h1.ReferenceCount);
```

预期：

```text
2
```

---

释放：

```csharp
h1.Release();

h2.Release();
```

预期：

```text
ReferenceCount = 0

CanRelease = true
```

---

# 14. MVP验收标准

支持：

* ResourceCache
* ResourceKey
* 缓存命中
* Retain自动增加
* 重复加载复用

不支持：

* 自动回收
* Addressables释放
* LRU缓存
* 内存预算

---

# 15. Git提交规范

Commit：

```bash
git commit -m "[Resource][Feature] Add resource cache system"
```

Tag：

```text
v0.1.20
```

---

# 16. 下一阶段

47_核心框架实现_ResourceGC

实现：

```text
待释放资源管理

缓存清理

资源回收

内存释放入口
```

---

# 17. 结论

ResourceCache 正式成为 Project Aether 资源系统第二核心组件。

架构升级为：

```text
Game Logic

↓

ResourceManager

↓

ResourceCache

↓

Provider

↓

Handle
```

实现资源复用，避免重复加载，为后续 AddressablesProvider 与 ResourceGC 打下基础。
