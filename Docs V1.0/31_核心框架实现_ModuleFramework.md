# 31_核心框架实现_ModuleFramework模块框架

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

实现 Project Aether 模块管理框架。

用于统一管理：

* ConfigModule
* ResourceModule
* UIModule
* NetworkModule
* SaveModule

等所有全局模块。

---

# 2. 所属程序集

Game.Framework

---

# 3. 程序集依赖

引用：

Game.Core

被引用：

Game.Config

Game.Resource

Game.UI

Game.Network

Game.Save

---

# 4. 物理路径

Assets/GameScripts/Framework/Module

---

# 5. 文件列表

IGameModule.cs

ModuleState.cs

ModuleManager.cs

---

# 6. 架构设计

模块生命周期：

```text
Create
    ↓
Initialize
    ↓
Update
    ↓
Shutdown
```

模块管理结构：

```text
ModuleManager

├── ConfigModule
├── ResourceModule
├── UIModule
├── NetworkModule
└── SaveModule
```

职责划分：

IGameModule

负责：

* 生命周期定义

ModuleManager

负责：

* 注册模块
* 初始化模块
* 更新模块
* 关闭模块

---

# 7. 生命周期

创建时机：

Bootstrap启动阶段

---

初始化时机：

Bootstrap.Initialize()

---

更新时机：

GameLoop.Update()

---

销毁时机：

Application Quit

---

# 8. 代码实现

## IGameModule.cs

```csharp
namespace ProjectAether.Framework
{
    public interface IGameModule
    {
        string ModuleName { get; }

        ModuleState State { get; }

        void Create();

        void Initialize();

        void Update();

        void Shutdown();
    }
}
```

---

## ModuleState.cs

```csharp
namespace ProjectAether.Framework
{
    public enum ModuleState
    {
        None,

        Created,

        Initialized,

        Running,

        Shutdown
    }
}
```

---

## ModuleManager.cs

```csharp
using System.Collections.Generic;

namespace ProjectAether.Framework
{
    public static class ModuleManager
    {
        private static readonly List<IGameModule>
            Modules =
                new();

        public static void Register(
            IGameModule module)
        {
            if (module == null)
            {
                return;
            }

            Modules.Add(module);

            module.Create();
        }

        public static void InitializeAll()
        {
            foreach (IGameModule module
                     in Modules)
            {
                module.Initialize();
            }
        }

        public static void UpdateAll()
        {
            foreach (IGameModule module
                     in Modules)
            {
                module.Update();
            }
        }

        public static void ShutdownAll()
        {
            for (int i =
                 Modules.Count - 1;
                 i >= 0;
                 i--)
            {
                Modules[i].Shutdown();
            }

            Modules.Clear();
        }
    }
}
```

---

# 9. 测试代码

测试目录：

Assets/GameScripts/Framework/Module/Test

---

## TestModule.cs

```csharp
using ProjectAether.Core;

namespace ProjectAether.Framework.Test
{
    public class TestModule
        : IGameModule
    {
        public string ModuleName =>
            "TestModule";

        public ModuleState State
        {
            get;
            private set;
        }

        public void Create()
        {
            State =
                ModuleState.Created;

            Log.Info(
                "Create Module");
        }

        public void Initialize()
        {
            State =
                ModuleState.Initialized;

            Log.Info(
                "Initialize Module");
        }

        public void Update()
        {
        }

        public void Shutdown()
        {
            State =
                ModuleState.Shutdown;

            Log.Info(
                "Shutdown Module");
        }
    }
}
```

---

## ModuleTest.cs

```csharp
using UnityEngine;

namespace ProjectAether.Framework.Test
{
    public class ModuleTest
        : MonoBehaviour
    {
        private void Start()
        {
            ModuleManager.Register(
                new TestModule());

            ModuleManager.InitializeAll();
        }

        private void OnDestroy()
        {
            ModuleManager.ShutdownAll();
        }
    }
}
```

---

# 10. Unity测试步骤

创建空物体：

ModuleTester

---

挂载：

ModuleTest

---

运行项目

---

# 11. 预期输出

```text
[INFO] Create Module

[INFO] Initialize Module

[INFO] Shutdown Module
```

---

# 12. MVP验收标准

支持：

Register()

InitializeAll()

UpdateAll()

ShutdownAll()

---

支持：

多模块注册

---

支持：

生命周期管理

---

# 13. Git提交规范

Commit：

[Feature] Add Module Framework

Tag：

v0.1.4

---

# 14. 后续扩展计划

## V2

增加：

ModulePriority

模块优先级

---

## V3

增加：

依赖关系管理

例如：

ResourceModule

依赖：

ConfigModule

---

## V4

增加：

异步初始化

```csharp
Task InitializeAsync()
```

---

## V5

增加：

热插拔模块

动态加载模块

---

# 15. 文档关联

上游：

30_核心框架实现_FSM状态机框架.md

下游：

32_核心框架实现_Bootstrap启动框架.md

---

# 16. 结论

ModuleFramework 是 Project Aether 运行时框架核心。

后续：

ConfigModule

ResourceModule

UIModule

NetworkModule

均通过 ModuleManager 统一管理生命周期。

Bootstrap 不再直接管理具体系统，而只负责驱动 ModuleFramework。
