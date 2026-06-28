# 32_核心框架实现_Bootstrap启动框架

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

建立 Project Aether 游戏启动入口。

负责：

* 注册模块
* 初始化模块
* 驱动模块更新
* 关闭模块

不负责：

* 模块业务逻辑

---

# 2. 所属程序集

Game.Framework

---

# 3. 程序集依赖

引用：

Game.Core

Game.Framework

---

被引用：

Game.Config

Game.Resource

Game.UI

Game.Network

---

# 4. 物理路径

Assets/GameScripts/Framework/Bootstrap

---

# 5. 文件列表

Bootstrap.cs

BootstrapRunner.cs

---

# 6. 架构设计

启动流程：

```text
Unity Start

↓
BootstrapRunner

↓
Bootstrap.Initialize()

↓
Register Modules

↓
Initialize Modules

↓
Game Ready
```

运行流程：

```text
Update

↓

ModuleManager.UpdateAll()
```

关闭流程：

```text
Application Quit

↓

ModuleManager.ShutdownAll()
```

---

# 7. 生命周期

创建时机：

Unity启动

---

初始化时机：

Bootstrap.Initialize()

---

更新时机：

每帧Update

---

销毁时机：

Application Quit

---

# 8. 代码实现

## Bootstrap.cs

```csharp
using ProjectAether.Core;

namespace ProjectAether.Framework
{
    public static class Bootstrap
    {
        public static void Initialize()
        {
            Log.Info(
                "Bootstrap Initialize");

            EventBus.Clear();

            ServiceLocator.Clear();

            RegisterModules();

            ModuleManager.InitializeAll();
        }

        private static void RegisterModules()
        {
            // 后续注册：

            // ConfigModule

            // ResourceModule

            // UIModule

            // NetworkModule
        }

        public static void Shutdown()
        {
            ModuleManager.ShutdownAll();

            Log.Info(
                "Bootstrap Shutdown");
        }
    }
}
```

---

## BootstrapRunner.cs

```csharp
using UnityEngine;

namespace ProjectAether.Framework
{
    public class BootstrapRunner
        : MonoBehaviour
    {
        private void Start()
        {
            Bootstrap.Initialize();
        }

        private void Update()
        {
            ModuleManager.UpdateAll();
        }

        private void OnApplicationQuit()
        {
            Bootstrap.Shutdown();
        }
    }
}
```

---

# 9. 测试代码

测试直接复用：

TestModule

ModuleFramework

测试代码。

---

# 10. Unity测试步骤

创建：

Bootstrap

GameObject

---

挂载：

BootstrapRunner

---

在RegisterModules()

中注册：

```csharp
ModuleManager.Register(
    new TestModule());
```

---

运行项目

---

# 11. 预期输出

```text
[INFO] Bootstrap Initialize

[INFO] Create Module

[INFO] Initialize Module

[INFO] Shutdown Module

[INFO] Bootstrap Shutdown
```

---

# 12. MVP验收标准

支持：

Bootstrap.Initialize()

Bootstrap.Shutdown()

---

支持：

模块注册

模块初始化

模块更新

模块关闭

---

# 13. Git提交规范

Commit：

[Feature] Add Bootstrap

Tag：

v0.1.5

---

# 14. 后续扩展计划

## V2

启动阶段：

```text
PreInit

Init

PostInit
```

---

## V3

启动进度条

Loading Scene

---

## V4

异步启动

Addressables加载

配置表加载

---

## V5

热更新启动流程

Patch检查

资源版本检测

---

# 15. 文档关联

上游：

31_核心框架实现_ModuleFramework模块框架.md

下游：

33_核心框架实现_ConfigModule.md

---

# 16. 结论

Bootstrap 是 Project Aether 唯一启动入口。

Bootstrap 不直接管理业务系统。

所有业务模块均通过 ModuleFramework 接入。
