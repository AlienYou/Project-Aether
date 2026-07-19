# 44_核心框架实现_InstantiateAsync

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：实施版

关联文档：

40_核心框架实现_ResourceManager

41_核心框架实现_IResourceProvider

42_核心框架实现_ResourceHandle

43_核心框架实现_ResourceManager_LoadAsync

43A_ResourceHandle失败状态设计修正

---

# 1. 文档目标

建立 Project Aether 统一实例化入口。

实现：

```text
Game Logic

↓

ResourceManager.InstantiateAsync()

↓

IResourceProvider

↓

ResourceHandle<GameObject>

↓

Instantiate

↓

GameObject
```

---

# 2. 为什么需要InstantiateAsync

错误做法：

```csharp
var handle =
    await ResourceManager.LoadAsync<GameObject>(
        "Prefabs/Player");

GameObject obj =
    GameObject.Instantiate(
        handle.Asset);
```

问题：

```text
业务层自行实例化

生命周期不可控

无法统计实例数量

无法统一对象池
```

---

工业级做法：

```csharp
GameObject player =
    await ResourceManager
        .InstantiateAsync(
            "Prefabs/Player");
```

业务层不关心：

```text
资源来源

Addressables

对象池

热更新
```

---

# 3. 当前阶段目标

实现：

```csharp
InstantiateAsync()
```

支持：

```csharp
GameObject
```

暂不支持：

```text
对象池

预加载

异步下载

引用计数回收
```

---

# 4. Provider接口扩展

文件：

```text
Assets/GameScripts/Resource/Providers/IResourceProvider.cs
```

新增：

```csharp
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ProjectAether.Resource
{
    public interface IResourceProvider
    {
        void Initialize();

        void Shutdown();

        UniTask<ResourceHandle<T>>
            LoadAsync<T>(
                string assetPath)
            where T : Object;

        UniTask<GameObject>
            InstantiateAsync(
                string assetPath);
    }
}
```

---

# 5. EditorProvider实现

新增：

```csharp
public async UniTask<GameObject>
    InstantiateAsync(
        string assetPath)
{
    var handle =
        await LoadAsync<GameObject>(
            assetPath);

    if (handle.State ==
        ResourceHandleState.Failed)
    {
        Log.Error(
            handle.Error);

        return null;
    }

    return Object.Instantiate(
        handle.Asset);
}
```

---

# 6. ResourceManager扩展

文件：

```text
Assets/GameScripts/Resource/Runtime/ResourceManager.cs
```

新增：

```csharp
using Cysharp.Threading.Tasks;
using UnityEngine;
```

新增：

```csharp
public static UniTask<GameObject>
    InstantiateAsync(
        string assetPath)
{
    if (!IsInitialized)
    {
        throw new InvalidOperationException(
            "ResourceManager Not Initialized");
    }

    return _provider.InstantiateAsync(
        assetPath);
}
```

---

# 7. 生命周期流程

```text
Game Logic

↓

ResourceManager.InstantiateAsync

↓

Provider.LoadAsync

↓

ResourceHandle

↓

Instantiate

↓

GameObject
```

---

# 8. 调用示例

角色创建：

```csharp
GameObject player =
    await ResourceManager
        .InstantiateAsync(
            "Prefabs/Player");
```

怪物创建：

```csharp
GameObject monster =
    await ResourceManager
        .InstantiateAsync(
            "Prefabs/Monster");
```

UI创建：

```csharp
GameObject ui =
    await ResourceManager
        .InstantiateAsync(
            "UI/UIMain");
```

---

# 9. 当前架构限制

当前：

```text
每次实例化

↓

重新LoadAsync

↓

Instantiate
```

存在重复加载问题。

例如：

```csharp
for (int i = 0; i < 100; i++)
{
    await ResourceManager
        .InstantiateAsync(
            "Prefabs/Bullet");
}
```

会产生大量重复加载。

---

这是当前阶段允许存在的技术债务。

原因：

```text
资源缓存系统

尚未实现
```

---

# 10. 为什么暂不实现缓存

缓存涉及：

```text
引用计数

缓存驱逐

内存预算

资源依赖管理
```

属于后续阶段。

---

# 11. Unity验证步骤

目录：

```text
Assets/Resources/Prefabs
```

创建：

```text
Player.prefab
```

测试：

```csharp
private async void Start()
{
    GameObject player =
        await ResourceManager
            .InstantiateAsync(
                "Prefabs/Player");

    Debug.Log(
        player.name);
}
```

预期：

```text
Player(Clone)
```

---

# 12. MVP验收标准

支持：

* InstantiateAsync
* Provider实例化
* ResourceManager实例化入口
* GameObject生成

不支持：

* 对象池
* Addressables实例化
* 缓存系统
* 引用计数

---

# 13. Git提交规范

Commit：

```bash
git commit -m "[Resource][Feature] Add InstantiateAsync pipeline"
```

Tag：

```text
v0.1.18
```

---

# 14. 下一阶段

45_核心框架实现_ResourceCache

实现：

```text
资源缓存

重复加载检测

资源复用

内存管理基础
```

---

# 15. 结论

InstantiateAsync 成为 Project Aether 唯一实例化入口。

业务层正式禁止直接：

```csharp
Resources.Load()

GameObject.Instantiate()
```

所有运行时资源实例化统一通过：

```csharp
ResourceManager.InstantiateAsync()
```

完成资源系统与业务层的第一次彻底解耦。
