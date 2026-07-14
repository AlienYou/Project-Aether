# 40_核心框架实现_ResourceManager

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：实施版

关联文档：

39_核心框架实现_ResourceModule

ADR-004_程序集与命名空间统一规范

---

# 1. 文档目标

建立 Project Aether 资源系统统一入口。

当前阶段仅完成：

* ResourceManager创建
* Initialize()
* Shutdown()
* 初始化状态管理
* 重复初始化保护

不实现：

* LoadAsync<T>()
* InstantiateAsync()
* Release()
* Addressables接入
* Provider架构

上述内容属于后续文档实现范围。

---

# 2. 所属程序集

程序集：

```text
ProjectAether.Resource
```

命名空间：

```csharp
namespace ProjectAether.Resource;
```

---

# 3. 工程目录

```text
Assets/GameScripts

└── Resource
    │
    ├── Module
    │   └── ResourceModule.cs
    │
    ├── Runtime
    │   └── ResourceManager.cs
    │
    ├── Providers
    │
    ├── Handles
    │
    └── ProjectAether.Resource.asmdef
```

---

# 4. ResourceManager职责

负责：

* 资源系统初始化
* 资源系统关闭
* 资源系统状态管理

不负责：

* 资源加载
* 资源实例化
* 资源释放

---

# 5. ResourceManager设计

采用静态管理器模式：

```csharp
public static class ResourceManager
{
}
```

原因：

当前阶段资源系统全局唯一。

无需实例化。

---

# 6. ResourceManager实现

路径：

```text
Assets/GameScripts/Resource/Runtime/ResourceManager.cs
```

代码：

```csharp
using ProjectAether.Core;

namespace ProjectAether.Resource
{
    public static class ResourceManager
    {
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

            IsInitialized = false;

            Log.Info(
                "[ResourceManager] Shutdown");
        }
    }
}
```

---

# 7. 初始化流程

调用：

```csharp
ResourceManager.Initialize();
```

执行：

```text
检查是否已初始化

↓

设置 IsInitialized = true

↓

输出日志
```

---

# 8. 重复初始化保护

允许：

```csharp
ResourceManager.Initialize();
```

---

禁止重复初始化：

```csharp
ResourceManager.Initialize();

ResourceManager.Initialize();
```

日志：

```text
[ResourceManager] Already Initialized
```

---

# 9. Shutdown流程

调用：

```csharp
ResourceManager.Shutdown();
```

执行：

```text
检查初始化状态

↓

设置 IsInitialized = false

↓

输出日志
```

---

# 10. 重复Shutdown保护

允许：

```csharp
ResourceManager.Shutdown();

ResourceManager.Shutdown();
```

不会抛出异常。

---

# 11. ResourceModule接入

完成 ResourceManager 后，

修改：

```csharp
public void Initialize()
{
    State = ModuleState.Initialized;

    Log.Info(
        "[Resource] Initialize");
}
```

为：

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

修改：

```csharp
public void Shutdown()
{
    State = ModuleState.Shutdown;

    Log.Info(
        "[Resource] Shutdown");
}
```

为：

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

# 12. 启动链路

```text
BootstrapRunner

↓

Bootstrap.Initialize()

↓

RegisterModules()

↓

ResourceModule.Create()

↓

ModuleManager.InitializeAll()

↓

ResourceModule.Initialize()

↓

ResourceManager.Initialize()
```

---

# 13. 关闭链路

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
```

---

# 14. Unity验证步骤

创建：

```text
ResourceManager.cs
```

---

编译通过。

---

运行项目。

---

观察日志：

```text
[ResourceManager] Initialize
```

---

退出：

```text
[ResourceManager] Shutdown
```

---

# 15. MVP验收标准

支持：

* ResourceManager创建
* Initialize()
* Shutdown()
* IsInitialized状态管理
* 重复初始化保护
* 重复关闭保护

---

# 16. Git提交规范

Commit：

```bash
git commit -m "[Resource][Feature] Add ResourceManager"
```

---

Tag：

```text
v0.1.13
```

---

# 17. 当前工程结构

```text
Assets/GameScripts

├── Core
├── Framework
├── Config
├── Resource
│
│   ├── Module
│   │   └── ResourceModule.cs
│   │
│   ├── Runtime
│   │   └── ResourceManager.cs
│   │
│   ├── Providers
│   │
│   ├── Handles
│   │
│   └── ProjectAether.Resource.asmdef
│
└── Entry
```

---

# 18. 下一阶段

41_核心框架实现_IResourceProvider

实现：

```csharp
LoadAsync<T>()

InstantiateAsync()

Release()
```

建立资源访问抽象层。

---

# 19. 结论

ResourceManager 正式成为 Project Aether 资源系统统一入口。

当前阶段负责资源系统生命周期管理。

资源加载能力将在后续 Provider 架构中实现。
