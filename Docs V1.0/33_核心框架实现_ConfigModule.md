# 33_核心框架实现_ConfigModule

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

建立配置系统入口模块。

负责：

* 配置系统生命周期管理
* 配置系统初始化
* 配置系统关闭

不负责：

* 配置表读取
* 配置表查询
* 配置缓存管理

上述内容将在后续 ConfigManager 中实现。

---

# 2. 所属程序集

Game.Config

---

# 3. 程序集依赖

## 引用

Game.Core

Game.Framework

---

## 被引用

Game.Resource

Game.UI

Game.Character

Game.Combat

Game.Skill

Game.Buff

Game.AI

---

# 4. 物理路径

Assets/GameScripts/Config

---

# 5. 文件列表

ConfigModule.cs

Game.Config.asmdef

---

# 6. asmdef配置

程序集名称：

Game.Config

引用程序集：

Game.Core

Game.Framework

目录：

Assets/GameScripts/Config

---

# 7. 架构设计

模块关系：

Bootstrap

↓

ModuleManager

↓

ConfigModule

↓

ConfigManager（下一阶段实现）

---

职责划分：

ConfigModule：

* 生命周期管理
* 模块注册
* 模块初始化
* 模块关闭

ConfigManager：

* 配置加载
* 配置缓存
* 配置查询
* 配置热重载

---

# 8. 生命周期

Create

职责：

注册配置系统

---

Initialize

职责：

初始化配置环境

---

Update

职责：

预留扩展

当前版本无逻辑

---

Shutdown

职责：

释放配置资源

清理配置缓存

---

# 9. 代码实现

## ConfigModule.cs

```csharp
using ProjectAether.Core;
using ProjectAether.Framework;

namespace ProjectAether.Config
{
    public class ConfigModule : IGameModule
    {
        public string ModuleName =>
            "ConfigModule";

        public ModuleState State
        {
            get;
            private set;
        }

        public void Create()
        {
            State = ModuleState.Created;

            Log.Info(
                "[Config] Create");
        }

        public void Initialize()
        {
            State =
                ModuleState.Initialized;

            Log.Info(
                "[Config] Initialize");
        }

        public void Update()
        {
        }

        public void Shutdown()
        {
            State =
                ModuleState.Shutdown;

            Log.Info(
                "[Config] Shutdown");
        }
    }
}
```

---

# 10. Bootstrap接入

修改：

Bootstrap.RegisterModules()

增加：

```csharp
ModuleManager.Register(
    new ConfigModule());
```

---

# 11. Unity测试步骤

步骤1：

创建空物体：

Bootstrap

---

步骤2：

挂载：

BootstrapRunner

---

步骤3：

在 Bootstrap.RegisterModules()

中增加：

```csharp
ModuleManager.Register(
    new ConfigModule());
```

---

步骤4：

运行项目

---

# 12. 预期输出

```text
[INFO] Bootstrap Initialize

[INFO] [Config] Create

[INFO] [Config] Initialize
```

退出项目：

```text
[INFO] [Config] Shutdown

[INFO] Bootstrap Shutdown
```

---

# 13. MVP验收标准

必须支持：

* ConfigModule注册
* ConfigModule初始化
* ConfigModule关闭

必须完成：

* ModuleFramework接入
* Bootstrap接入

必须通过：

* Unity运行测试

---

# 14. Git提交规范

Commit：

[Feature] Add ConfigModule

Tag：

v0.1.6

---

# 15. 后续扩展计划

V2：

ConfigManager

---

V3：

ConfigTableFramework

---

V4：

Excel导表系统

---

V5：

配置热更新系统

---

# 16. 文档关联

上游文档：

32_核心框架实现_Bootstrap启动框架

---

下游文档：

34_核心框架实现_ConfigManager

---

# 17. 当前工程结构

Assets/GameScripts

├── Core

├── Framework

└── Config
├── ConfigModule.cs
└── Game.Config.asmdef

---

# 18. 结论

ConfigModule 是 Project Aether 配置系统的入口模块。

负责统一管理配置系统生命周期。

后续所有配置表、配置缓存、配置查询逻辑均通过 ConfigManager 实现。

ConfigModule 不承担具体配置业务逻辑，仅负责模块管理职责。
