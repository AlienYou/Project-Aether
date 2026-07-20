# 47_核心框架实现_ResourceGC

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：实施版

关联文档：

40_核心框架实现_ResourceManager

42_核心框架实现_ResourceHandle

45_核心框架实现_ResourceReferenceCount

46_核心框架实现_ResourceCache

---

# 1. 文档目标

建立 Project Aether 第一版资源回收系统（ResourceGC）。

解决：

```text
资源已无人引用

但仍长期驻留缓存

导致内存不断增长
```

问题。

---

# 2. 当前问题

目前资源生命周期：

```text
LoadAsync()

↓

Cache

↓

RefCount = 1
```

---

使用：

```text
Retain()

↓

RefCount++
```

---

释放：

```text
Release()

↓

RefCount--
```

---

最终：

```text
RefCount == 0

↓

CanRelease == true
```

---

但是：

```text
不会发生任何事情
```

资源仍保留在：

```text
ResourceCache
```

中。

---

# 3. 设计目标

当：

```text
ReferenceCount == 0
```

时：

```text
加入待回收列表
```

而不是：

```text
立即卸载
```

---

最终：

```text
PendingRelease

↓

统一扫描

↓

统一释放
```

---

# 4. 架构设计

新增：

```text
ResourceManager

↓

ResourceCache

↓

ResourceGC

↓

Provider
```

---

职责：

ResourceCache

```text
资源缓存
```

---

ResourceGC

```text
回收管理

待释放资源管理

资源释放入口
```

---

Provider

```text
真正资源卸载
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
    │   ├── ResourceCache.cs
    │   └── ResourceGC.cs
```

---

# 6. Provider扩展

IResourceProvider新增：

```csharp
void Release(
    ResourceHandle handle);
```

完整接口：

```csharp
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ProjectAether.Resource
{
    public interface IResourceProvider
    {
        UniTask<ResourceHandle<T>>
            LoadAsync<T>(
                string assetPath)
            where T : Object;

        void Release(
            ResourceHandle handle);
    }
}
```

---

# 7. EditorProvider实现

第一阶段：

```csharp
public void Release(
    ResourceHandle handle)
{
}
```

---

原因：

```text
Resources.Load

无需主动释放单个资源
```

---

后续：

```text
AddressablesProvider
```

会真正实现：

```csharp
Addressables.Release(...)
```

---

# 8. ResourceGC实现

文件：

```text
ResourceGC.cs
```

代码：

```csharp
using System.Collections.Generic;

namespace ProjectAether.Resource
{
    internal static class ResourceGC
    {
        private static readonly List<ResourceHandle>
            PendingRelease =
                new();

        public static void MarkForRelease(
            ResourceHandle handle)
        {
            if (handle == null)
            {
                return;
            }

            if (!handle.CanRelease)
            {
                return;
            }

            if (PendingRelease.Contains(handle))
            {
                return;
            }

            PendingRelease.Add(handle);
        }
    }
}
```

---

# 9. ResourceHandle修改

Release：

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

        ResourceGC.MarkForRelease(
            this);
    }
}
```

---

形成：

```text
Release

↓

RefCount=0

↓

PendingRelease
```

---

# 10. ResourceGC.Update实现

新增：

```csharp
public static void Update()
{
    for (int i =
        PendingRelease.Count - 1;
        i >= 0;
        i--)
    {
        var handle =
            PendingRelease[i];

        if (!handle.CanRelease)
        {
            PendingRelease.RemoveAt(i);
            continue;
        }

        ReleaseInternal(handle);

        PendingRelease.RemoveAt(i);
    }
}
```

---

# 11. 真正释放

新增：

```csharp
private static void ReleaseInternal(
    ResourceHandle handle)
{
    ResourceCache.Remove(handle);

    ResourceManager.Provider
        .Release(handle);
}
```

---

# 12. ResourceCache扩展

新增：

```csharp
public static void Remove(
    ResourceHandle handle)
{
    ResourceKey foundKey =
        default;

    bool found = false;

    foreach (var pair in Cache)
    {
        if (ReferenceEquals(
                pair.Value,
                handle))
        {
            foundKey =
                pair.Key;

            found = true;

            break;
        }
    }

    if (found)
    {
        Cache.Remove(foundKey);
    }
}
```

---

# 13. ResourceManager扩展

新增：

```csharp
internal static IResourceProvider
    Provider
{
    get
    {
        return _provider;
    }
}
```

---

# 14. ResourceModule扩展

Update：

```csharp
public void Update()
{
    ResourceGC.Update();
}
```

---

这样：

```text
BootstrapRunner

↓

ModuleManager.UpdateAll()

↓

ResourceModule.Update()

↓

ResourceGC.Update()
```

---

形成完整回收链路。

---

# 15. 生命周期

加载：

```text
LoadAsync

↓

Cache

↓

RefCount=1
```

---

使用：

```text
Retain

↓

RefCount=2
```

---

释放：

```text
Release

↓

RefCount=1
```

---

再次释放：

```text
Release

↓

RefCount=0
```

---

进入：

```text
PendingRelease
```

---

下一帧：

```text
ResourceGC.Update
```

---

执行：

```text
Cache.Remove

↓

Provider.Release
```

---

彻底释放。

---

# 16. Unity验证

测试：

```csharp
var handle =
    await ResourceManager
        .LoadAsync<GameObject>(
            "Prefabs/Player");

handle.Release();
```

---

验证：

```text
PendingRelease Count = 1
```

---

下一帧：

```text
ResourceGC.Update()
```

---

验证：

```text
PendingRelease Count = 0

Cache Count = 0
```

---

# 17. MVP验收标准

支持：

* PendingRelease
* ResourceGC
* Cache移除
* Provider释放入口
* 生命周期闭环

不支持：

* 延迟时间
* LRU
* 内存预算
* 自动压缩

---

# 18. Git提交规范

Commit：

```bash
git commit -m "[Resource][Feature] Add resource gc system"
```

Tag：

```text
v0.1.21
```

---

# 19. 下一阶段

48_核心框架实现_AddressablesProvider

实现：

```text
Addressables资源加载

Addressables资源释放

EditorProvider切换

运行时Provider切换
```

---

# 20. 结论

ResourceGC 正式建立。

Project Aether 资源系统首次形成完整生命周期：

```text
Load

↓

Cache

↓

Retain

↓

Release

↓

PendingRelease

↓

ResourceGC

↓

Provider.Release
```

实现资源的完整闭环管理。
