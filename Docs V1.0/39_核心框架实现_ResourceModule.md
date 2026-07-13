# 39_核心框架实现_ResourceModule

版本：v2.1

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：正式版

关联文档：

31_核心框架实现_ModuleFramework

32_核心框架实现_Bootstrap

ADR-003_Config系统架构修正

ADR-004_程序集与命名空间统一规范

---

# 1. 文档目标

建立 Project Aether 资源系统入口模块。

负责：

* Resource系统生命周期管理
* ResourceManager初始化
* ResourceProvider注册
* Addressables初始化入口
* Resource系统关闭

不负责：

* 资源加载
* 资源实例化
* 资源释放
* 资源引用计数

以上职责由 ResourceManager 负责。

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

# 3. 程序集依赖

引用：

```text
ProjectAether.Core

ProjectAether.Framework

Cysharp.Threading.Tasks
```

后续增加：

```text
Unity.Addressables
```

---

# 4. 工程目录

```text
Assets/GameScripts

└── Resource
    │
    ├── Module
    │   └── ResourceModule.cs
    │
    ├── Runtime
    │
    ├── Providers
    │
    ├── Handles
    │
    └── ProjectAether.Resource.asmdef
```

---

# 5. 架构定位

资源系统整体结构：

```text
Bootstrap

↓

ModuleManager

↓

ResourceModule

↓

ResourceManager

↓

IResourceProvider

├── EditorProvider
├── AddressablesProvider
└── HotUpdateProvider
```

---

# 6. 生命周期设计

状态流转：

```text
None

↓

Create()

↓

Created

↓

Initialize()

↓

Initialized

↓

第一次Update()

↓

Running

↓

Shutdown()

↓

Shutdown
```

---

# 7. ResourceModule职责

负责：

```text
初始化 ResourceManager

注册 ResourceProvider

驱动资源系统生命周期

关闭资源系统
```

---

不负责：

```text
Load

Instantiate

Release

Unload
```

---

# 8. ResourceModule实现

路径：

```text
Assets/GameScripts/Resource/Module/ResourceModule.cs
```

代码：

```csharp
using ProjectAether.Core;
using ProjectAether.Framework;

namespace ProjectAether.Resource
{
    public sealed class ResourceModule
        : IGameModule
    {
        public string ModuleName =>
            "Resource";

        public ModuleState State
        {
            get;
            private set;
        } = ModuleState.None;

        public void Create()
        {
            State = ModuleState.Created;

            Log.Info(
                "[Resource] Create");
        }

        public void Initialize()
        {
            ResourceManager.Initialize();

            State = ModuleState.Initialized;

            Log.Info(
                "[Resource] Initialize");
        }

        public void Update()
        {
            if (State ==
                ModuleState.Initialized)
            {
                State =
                    ModuleState.Running;
            }
        }

        public void Shutdown()
        {
            ResourceManager.Shutdown();

            State =
                ModuleState.Shutdown;

            Log.Info(
                "[Resource] Shutdown");
        }
    }
}
```

---

# 9. Bootstrap集成

Bootstrap.cs

注册：

```csharp
private static void RegisterModules()
{
    ModuleManager.Register(
        new ConfigModule());

    ModuleManager.Register(
        new ResourceModule());
}
```

---

# 10. BootstrapRunner驱动

当前工程：

```csharp
void Update()
{
    ModuleManager.UpdateAll();
}
```

因此：

```text
ResourceModule.Update()
```

每帧自动执行。

---

# 11. Config系统关系

未来结构：

```text
ConfigModule

↓

ConfigLoader

↓

ResourceManager

↓

AddressablesProvider
```

说明：

ConfigLoader 不允许直接访问文件系统。

统一通过：

```csharp
ResourceManager
```

获取配置资源。

---

# 12. Addressables规划

阶段一：

```text
EditorProvider
```

---

阶段二：

```text
AddressablesProvider
```

---

阶段三：

```text
HotUpdateProvider
```

---

业务层代码保持不变。

---

# 13. Unity验证步骤

创建：

```text
ProjectAether.Resource.asmdef
```

---

创建：

```text
ResourceModule.cs
```

---

Bootstrap注册：

```csharp
new ResourceModule()
```

---

运行项目。

---

# 14. 预期日志

启动：

```text
[Config] Create

[Resource] Create

[Config] Initialize

[Resource] Initialize
```

---

退出：

```text
[Resource] Shutdown

[Config] Shutdown
```

---

# 15. MVP验收标准

支持：

* ResourceModule创建
* State状态流转
* Bootstrap集成
* ModuleManager驱动
* ResourceManager初始化入口

---

# 16. Git提交规范

Commit：

```bash
[Feature] Add ResourceModule
```

Tag：

```text
v0.1.12
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

下游文档：

```text
40_核心框架实现_ResourceManager
```

实现：

```csharp
Initialize()

Shutdown()

LoadAsync<T>()

InstantiateAsync()

Release()
```

建立整个 Project Aether 的统一资源访问入口。

---

# 19. 结论

ResourceModule 是 Project Aether 资源系统入口模块。

负责资源系统生命周期管理。

所有业务模块必须通过 ResourceManager 访问资源系统。

禁止直接调用：

* Addressables API
* Resources API
* AssetBundle API
