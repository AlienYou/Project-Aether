# 41_核心框架实现_IResourceProvider

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：实施版

关联文档：

40_核心框架实现_ResourceManager

ADR-004_程序集与命名空间统一规范

---

# 1. 文档目标

建立 Project Aether 资源系统抽象层。

本阶段目标：

* 建立 IResourceProvider
* 建立 EditorProvider
* ResourceManager 接入 Provider
* 完成资源系统抽象

本阶段不实现：

* LoadAsync<T>()
* InstantiateAsync()
* Release()
* AddressablesProvider
* HotUpdateProvider

---

# 2. 设计背景

禁止业务层直接调用：

```csharp
Addressables.LoadAssetAsync()

Resources.Load()

AssetBundle.LoadAsset()
```

统一通过：

```csharp
ResourceManager
```

访问资源系统。

---

# 3. 架构设计

整体结构：

```text
Game Logic

↓

ResourceManager

↓

IResourceProvider

↓

EditorProvider

(未来)

↓

AddressablesProvider

↓

HotUpdateProvider
```

---

# 4. 所属程序集

程序集：

```text
ProjectAether.Resource
```

命名空间：

```csharp
namespace ProjectAether.Resource;
```

---

# 5. 工程目录

```text
Assets/GameScripts

└── Resource
    │
    ├── Runtime
    │   └── ResourceManager.cs
    │
    ├── Providers
    │   ├── IResourceProvider.cs
    │   └── EditorProvider.cs
    │
    ├── Handles
    │
    └── ProjectAether.Resource.asmdef
```

---

# 6. IResourceProvider职责

Provider 是资源系统实际实现层。

负责：

* 资源系统初始化
* 资源系统关闭

未来负责：

* 资源加载
* 实例化
* 资源释放

---

# 7. IResourceProvider接口

文件：

```text
Assets/GameScripts/Resource/Providers/IResourceProvider.cs
```

代码：

```csharp
namespace ProjectAether.Resource
{
    public interface IResourceProvider
    {
        void Initialize();

        void Shutdown();
    }
}
```

---

# 8. EditorProvider实现

文件：

```text
Assets/GameScripts/Resource/Providers/EditorProvider.cs
```

代码：

```csharp
using ProjectAether.Core;

namespace ProjectAether.Resource
{
    public sealed class EditorProvider
        : IResourceProvider
    {
        public void Initialize()
        {
            Log.Info(
                "[EditorProvider] Initialize");
        }

        public void Shutdown()
        {
            Log.Info(
                "[EditorProvider] Shutdown");
        }
    }
}
```

---

# 9. ResourceManager改造

当前：

```csharp
public static class ResourceManager
{
}
```

升级为：

```csharp
using ProjectAether.Core;

namespace ProjectAether.Resource
{
    public static class ResourceManager
    {
        private static IResourceProvider _provider;

        public static bool IsInitialized
        {
            get;
            private set;
        }

        public static void Initialize()
        {
            if (IsInitialized)
            {
                Log.Warning(
                    "[ResourceManager] Already Initialized");

                return;
            }

            _provider =
                new EditorProvider();

            _provider.Initialize();

            IsInitialized = true;

            Log.Info(
                "[ResourceManager] Initialize");
        }

        public static void Shutdown()
        {
            if (!IsInitialized)
            {
                return;
            }

            _provider?.Shutdown();

            _provider = null;

            IsInitialized = false;

            Log.Info(
                "[ResourceManager] Shutdown");
        }
    }
}
```

---

# 10. ResourceModule接入

确认 ResourceModule 已修改：

```csharp
public void Initialize()
{
    ResourceManager.Initialize();

    State = ModuleState.Initialized;

    Log.Info(
        "[Resource] Initialize");
}
```

---

关闭：

```csharp
public void Shutdown()
{
    ResourceManager.Shutdown();

    State = ModuleState.Shutdown;

    Log.Info(
        "[Resource] Shutdown");
}
```

---

# 11. 生命周期流程

启动：

```text
BootstrapRunner

↓

Bootstrap.Initialize()

↓

ModuleManager.InitializeAll()

↓

ResourceModule.Initialize()

↓

ResourceManager.Initialize()

↓

EditorProvider.Initialize()
```

---

关闭：

```text
OnApplicationQuit()

↓

Bootstrap.Shutdown()

↓

ModuleManager.ShutdownAll()

↓

ResourceModule.Shutdown()

↓

ResourceManager.Shutdown()

↓

EditorProvider.Shutdown()
```

---

# 12. Unity验证步骤

运行项目。

预期日志：

```text
[EditorProvider] Initialize

[ResourceManager] Initialize

[Resource] Initialize
```

退出：

```text
[EditorProvider] Shutdown

[ResourceManager] Shutdown

[Resource] Shutdown
```

---

# 13. MVP验收标准

支持：

* IResourceProvider
* EditorProvider
* ResourceManager持有Provider
* 生命周期驱动
* 编译通过

不支持：

* LoadAsync
* InstantiateAsync
* Release
* Addressables

---

# 14. Git提交规范

Commit：

```bash
git commit -m "[Resource][Feature] Add resource provider abstraction"
```

Tag：

```text
v0.1.14
```

---

# 15. 下一阶段

42_核心框架实现_ResourceHandle

实现：

```csharp
ResourceHandle

AssetHandle

PrefabHandle
```

建立资源生命周期管理体系。

---

# 16. 结论

IResourceProvider 正式成为 Project Aether 资源系统抽象层。

ResourceManager 不再直接依赖具体资源实现。

后续可无缝切换：

* EditorProvider
* AddressablesProvider
* HotUpdateProvider

而无需修改业务层代码。
