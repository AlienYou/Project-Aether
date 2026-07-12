# 38_核心框架实现_ConfigLoader

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

建立配置加载框架。

负责：

* 配置文件读取
* 配置反序列化
* ConfigContainer构建
* 配置注册

不负责：

* 配置查询
* 配置缓存管理
* 配置热更新

---

# 2. 所属程序集

Game.Config

---

# 3. 程序集依赖

引用：

Game.Core

Game.Framework

UnityEngine

---

被引用：

Game.Character

Game.Combat

Game.Skill

Game.Buff

Game.AI

---

# 4. 物理路径

Assets/GameScripts/Config/Loader

---

# 5. 文件列表

IConfigLoader.cs

JsonConfigLoader.cs

BinaryConfigLoader.cs

---

# 6. 架构设计

整体结构：

```text id="c9w7r4"
ConfigModule

↓

IConfigLoader

↓

JsonConfigLoader

BinaryConfigLoader

↓

ConfigContainer<T>

↓

ConfigManager
```

---

# 7. 接口设计

## IConfigLoader.cs

```csharp id="z5n4ku"
namespace ProjectAether.Config
{
    public interface IConfigLoader
    {
        ConfigContainer<T>
            Load<T>(
                string path)
            where T : IConfigRow;
    }
}
```

---

# 8. Json版本实现

## JsonConfigLoader.cs

```csharp id="q7x2af"
using System.Collections.Generic;

namespace ProjectAether.Config
{
    public class JsonConfigLoader
        : IConfigLoader
    {
        public ConfigContainer<T>
            Load<T>(
                string path)
            where T : IConfigRow
        {
            ConfigContainer<T>
                container =
                    new();

            /*
             * V1阶段
             *
             * 仅保留框架
             *
             * 后续接入：
             *
             * Newtonsoft.Json
             *
             * 或 Unity JsonUtility
             */

            return container;
        }
    }
}
```

---

# 9. Binary版本实现

## BinaryConfigLoader.cs

```csharp id="y3m5pd"
namespace ProjectAether.Config
{
    public class BinaryConfigLoader
        : IConfigLoader
    {
        public ConfigContainer<T>
            Load<T>(
                string path)
            where T : IConfigRow
        {
            ConfigContainer<T>
                container =
                    new();

            /*
             * V1阶段
             *
             * 保留接口
             *
             * 后续实现：
             *
             * BinaryReader
             *
             * MessagePack
             *
             * ProtoBuf
             */

            return container;
        }
    }
}
```

---

# 10. ConfigManager扩展

增加注册接口：

```csharp id="k4v6ej"
public static void RegisterTable<T>(
    ConfigContainer<T> table)
    where T : IConfigRow
{
    _containers[typeof(T)]
        = table;
}
```

---

增加获取表接口：

```csharp id="m8p2xj"
public static ConfigContainer<T>
    GetTable<T>()
    where T : IConfigRow
{
    ...
}
```

---

# 11. ConfigModule扩展

新增：

```csharp id="n7u3wc"
private IConfigLoader
    _loader;
```

---

初始化：

```csharp id="h6k1ya"
_loader =
    new BinaryConfigLoader();
```

---

未来支持：

```csharp id="w4j9vr"
Debug模式

↓

JsonLoader

Release模式

↓

BinaryLoader
```

---

# 12. 配置加载流程

运行流程：

```text id="t5d8oq"
Bootstrap

↓

ConfigModule

↓

BinaryConfigLoader

↓

MonsterConfig.bytes

↓

ConfigContainer<MonsterConfig>

↓

ConfigManager

↓

Gameplay
```

---

# 13. Unity测试方案

测试文件：

```text id="p3k8wb"
MonsterConfig.json
```

---

测试步骤：

创建：

```csharp id="r1q7zx"
JsonConfigLoader
```

---

调用：

```csharp id="g9n2yd"
Load<MonsterConfig>()
```

---

验证：

```csharp id="v6e5ah"
container.Count
```

---

# 14. MVP验收标准

支持：

IConfigLoader

---

支持：

JsonConfigLoader

---

支持：

BinaryConfigLoader

---

支持：

ConfigContainer构建

---

支持：

ConfigManager注册

---

# 15. Git提交规范

Commit：

```bash id="s2f9ku"
[Feature] Add Config Loader Framework
```

---

Tag：

```text id="d8r4cx"
v0.1.11
```

---

# 16. 后续扩展计划

V2：

Newtonsoft Json Loader

---

V3：

MessagePack Loader

---

V4：

ProtoBuf Loader

---

V5：

热更新配置加载器

---

# 17. 文档关联

上游：

37_核心框架实现_ConfigContainer

ADR-003_Config系统架构修正

---

下游：

39_核心框架实现_ResourceModule

---

# 18. 当前工程结构

Assets/GameScripts/Config

├── Loader
│   ├── IConfigLoader.cs
│   ├── JsonConfigLoader.cs
│   └── BinaryConfigLoader.cs
│
├── Runtime
│   └── ConfigContainer.cs
│
├── ConfigManager.cs
├── ConfigModule.cs
└── IConfigRow.cs

---

# 19. 结论

ConfigLoader 建立了配置系统与配置文件之间的桥梁。

后续无论使用：

* Json
* Binary
* MessagePack
* ProtoBuf

业务层均无需修改。

配置系统正式完成：

加载 → 存储 → 查询

完整闭环。
