# 43_核心框架实现_ResourceManager_LoadAsync

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：实施版

关联文档：

40_核心框架实现_ResourceManager

41_核心框架实现_IResourceProvider

42_核心框架实现_ResourceHandle

ADR-004_程序集与命名空间统一规范

---

# 1. 文档目标

正式建立 Project Aether 第一版资源加载链路。

实现：

```text
Game Logic

↓

ResourceManager

↓

IResourceProvider

↓

EditorProvider

↓

ResourceHandle<T>
```

本阶段目标：

* 增加 LoadAsync<T>()
* Provider支持资源加载
* 返回 ResourceHandle<T>

本阶段不实现：

* InstantiateAsync
* Release管理器
* 引用计数
* AddressablesProvider

---

# 2. 设计原则

统一入口：

```csharp
await ResourceManager.LoadAsync<T>();
```

禁止：

```csharp
Resources.Load()

Addressables.LoadAssetAsync()

AssetBundle.LoadAsset()
```

直接出现在业务代码。

---

# 3. Provider接口扩展

文件：

```text
Resource/Providers/IResourceProvider.cs
```

修改为：

```csharp
using Cysharp.Threading.Tasks;

namespace ProjectAether.Resource
{
    public interface IResourceProvider
    {
        void Initialize();

        void Shutdown();

        UniTask<ResourceHandle<T>>
            LoadAsync<T>(string assetPath)
            where T : UnityEngine.Object;
    }
}
```

---

# 4. EditorProvider实现

文件：

```text
Resource/Providers/EditorProvider.cs
```

增加：

```csharp
using Cysharp.Threading.Tasks;
using UnityEngine;
```

实现：

```csharp
public async UniTask<ResourceHandle<T>>
    LoadAsync<T>(string assetPath)
    where T : Object
{
    await UniTask.Yield();

    T asset =
        Resources.Load<T>(assetPath);

    var handle =
        new ResourceHandle<T>
        {
            Asset = asset
        };

    handle.AssetPath =
        assetPath;

    handle.State =
        ResourceHandleState.Loaded;

    return handle;
}
```

---

# 5. ResourceHandle修正

当前：

```csharp
public class ResourceHandle<T>
{
}
```

增加构造能力：

```csharp
namespace ProjectAether.Resource
{
    public class ResourceHandle<T>
        : ResourceHandle
    {
        public T Asset
        {
            get;
            internal set;
        }

        internal void SetLoaded(
            string path,
            T asset)
        {
            AssetPath = path;

            Asset = asset;

            State =
                ResourceHandleState.Loaded;
        }
    }
}
```

---

# 6. ResourceManager扩展

文件：

```text
Resource/Runtime/ResourceManager.cs
```

增加：

```csharp
using Cysharp.Threading.Tasks;
using UnityEngine;
```

---

新增：

```csharp
public static UniTask<ResourceHandle<T>>
    LoadAsync<T>(
        string assetPath)
    where T : Object
{
    if (!IsInitialized)
    {
        Log.Error(
            "[ResourceManager] Not Initialized");

        return default;
    }

    return _provider.LoadAsync<T>(
        assetPath);
}
```

---

# 7. 加载流程

```text
Game Logic

↓

ResourceManager.LoadAsync

↓

Provider.LoadAsync

↓

创建Handle

↓

返回Handle
```

---

# 8. 调用示例

例如：

```csharp
var handle =
    await ResourceManager
        .LoadAsync<GameObject>(
            "Prefabs/Player");
```

获取资源：

```csharp
GameObject playerPrefab =
    handle.Asset;
```

---

# 9. 为什么先使用Resources

原因：

```text
AddressablesProvider

尚未实现
```

当前目标：

```text
验证架构

验证接口

验证Handle链路
```

不是验证热更新。

---

# 10. Resources目录规范

Unity要求：

```text
Assets

└── Resources
    │
    └── Prefabs
        │
        └── Player.prefab
```

加载：

```csharp
await ResourceManager
    .LoadAsync<GameObject>(
        "Prefabs/Player");
```

---

# 11. Unity验证步骤

创建：

```text
Assets/Resources/Prefabs
```

---

创建：

```text
Player.prefab
```

---

测试：

```csharp
private async void Start()
{
    var handle =
        await ResourceManager
            .LoadAsync<GameObject>(
                "Prefabs/Player");

    Debug.Log(
        handle.Asset.name);
}
```

---

预期：

```text
Player
```

输出成功。

---

# 12. MVP验收标准

支持：

* ResourceManager.LoadAsync
* IResourceProvider.LoadAsync
* EditorProvider.LoadAsync
* ResourceHandle<T>
* Resources加载

不支持：

* Addressables
* 实例化
* 引用计数
* 自动释放

---

# 13. Git提交规范

Commit：

```bash
git commit -m "[Resource][Feature] Add LoadAsync pipeline"
```

Tag：

```text
v0.1.16
```

---

# 14. 下一阶段

44_核心框架实现_InstantiateAsync

实现：

```csharp
InstantiateAsync<T>()
```

统一实例化入口。

---

# 15. 结论

本阶段完成后，Project Aether 将拥有第一条完整资源加载链路：

```text
Game Logic

↓

ResourceManager

↓

IResourceProvider

↓

EditorProvider

↓

ResourceHandle<T>
```

后续 AddressablesProvider 仅需替换 Provider 层即可，无需修改业务代码。
