# 32_核心框架实现_Bootstrap启动框架（修正版）

版本：v1.1

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版（ADR-002修正版）

---

# 1. 文档目标

建立 Project Aether 唯一启动入口。

负责：

* 注册模块
* 初始化模块
* 驱动模块更新
* 关闭模块

不负责：

* 业务逻辑
* 配置管理
* 资源管理

Bootstrap 仅负责框架启动。

---

# 2. 所属程序集

Game.Entry

---

# 3. 程序集依赖

## 引用

Game.Core

Game.Framework

Game.Config

Game.Resource

Game.UI

Game.Network

---

## 被引用

无

Game.Entry 为最顶层程序集。

---

# 4. 物理路径

Assets/GameScripts/Entry/Bootstrap

---

# 5. 文件列表

Bootstrap.cs

BootstrapRunner.cs

Game.Entry.asmdef

---

# 6. asmdef配置

程序集名称：

Game.Entry

---

引用程序集：

Game.Core

Game.Framework

Game.Config

Game.Resource

Game.UI

Game.Network

---

目录：

Assets/GameScripts/Entry

---

# 7. 架构设计

程序集结构：

Game.Entry

↓

Game.Config

Game.Resource

Game.UI

Game.Network

↓

Game.Framework

↓

Game.Core

---

启动流程：

Unity

↓

BootstrapRunner

↓

Bootstrap.Initialize()

↓

RegisterModules()

↓

ModuleManager.InitializeAll()

↓

Game Ready

---

# 8. 生命周期

Create

Unity启动

---

Initialize

Bootstrap.Initialize()

---

Update

ModuleManager.UpdateAll()

---

Shutdown

Application Quit

---

# 9. 代码实现

## Bootstrap.cs

```csharp
using ProjectAether.Framework;
using ProjectAether.Config;

namespace ProjectAether.Entry
{
    public static class Bootstrap
    {
        public static void Initialize()
        {
            RegisterModules();

            ModuleManager.InitializeAll();
        }

        private static void RegisterModules()
        {
            ModuleManager.Register(
                new ConfigModule());

            // 后续：

            // ResourceModule

            // UIModule

            // NetworkModule
        }

        public static void Shutdown()
        {
            ModuleManager.ShutdownAll();
        }
    }
}
```

---

## BootstrapRunner.cs

```csharp
using UnityEngine;
using ProjectAether.Framework;

namespace ProjectAether.Entry
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

# 10. Unity测试步骤

创建：

Bootstrap

GameObject

---

挂载：

BootstrapRunner

---

运行项目

---

# 11. 预期输出

```text
[INFO] [Config] Create

[INFO] [Config] Initialize
```

退出：

```text
[INFO] [Config] Shutdown
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

模块关闭

---

支持：

Game.Entry 启动流程

---

# 13. Git提交规范

Commit：

[Refactor] Move Bootstrap To Entry

Tag：

v0.1.5a

---

# 14. 后续扩展计划

V2：

启动阶段拆分

PreInit

Init

PostInit

---

V3：

Loading流程

---

V4：

异步模块初始化

---

V5：

热更新启动器

---

# 15. 文档关联

上游：

31_核心框架实现_ModuleFramework模块框架

ADR-002_程序集依赖架构修正

---

下游：

33_核心框架实现_ConfigModule

34_核心框架实现_ConfigManager

---

# 16. 当前工程结构

Assets/GameScripts

├── Core

├── Framework

├── Config

└── Entry
└── Bootstrap

---

# 17. 结论

Bootstrap 已迁移至 Game.Entry。

Game.Framework 不再引用任何业务程序集。

程序集依赖保持单向结构，彻底避免循环引用问题。
