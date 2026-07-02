# 34_核心框架实现_ConfigManager

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

实现配置系统核心管理器。

负责：

* 配置加载
* 配置缓存
* 配置查询
* 配置卸载

为后续：

MonsterConfig

SkillConfig

WeaponConfig

ItemConfig

提供统一管理入口。

---

# 2. 所属程序集

Game.Config

---

# 3. 程序集依赖

引用：

Game.Core

Game.Framework

---

被引用：

Game.Character

Game.Combat

Game.Skill

Game.Buff

Game.AI

Game.UI

---

# 4. 物理路径

Assets/GameScripts/Config

---

# 5. 文件列表

IConfigRow.cs

ConfigManager.cs

---

# 6. 架构设计

配置结构：

```text
ConfigManager

├── MonsterConfig
├── SkillConfig
├── WeaponConfig
├── ItemConfig
└── BuffConfig
```

统一查询：

```csharp
ConfigManager.Get<T>(id)
```

统一加载：

```csharp
ConfigManager.Load<T>()
```

---

# 7. 生命周期

Create

ConfigModule创建

---

Initialize

初始化缓存容器

---

Shutdown

释放缓存

---

# 8. 接口设计

## IConfigRow.cs

```csharp
namespace ProjectAether.Config
{
    public interface IConfigRow
    {
        int Id { get; }
    }
}
```

---

# 9. 代码实现

## ConfigManager.cs

```csharp
using System;
using System.Collections.Generic;

namespace ProjectAether.Config
{
    public static class ConfigManager
    {
        private static readonly Dictionary<
            Type,
            Dictionary<int, IConfigRow>>
            ConfigTables =
                new();

        public static void Initialize()
        {
            ConfigTables.Clear();
        }

        public static void Shutdown()
        {
            ConfigTables.Clear();
        }

        public static void Load<T>(
            List<T> configs)
            where T : class, IConfigRow
        {
            Dictionary<int, IConfigRow>
                table =
                    new();

            foreach (T row in configs)
            {
                table[row.Id] = row;
            }

            ConfigTables[typeof(T)]
                = table;
        }

        public static T Get<T>(
            int id)
            where T : class, IConfigRow
        {
            if (!ConfigTables.TryGetValue(
                    typeof(T),
                    out Dictionary<int,
                        IConfigRow> table))
            {
                return null;
            }

            if (!table.TryGetValue(
                    id,
                    out IConfigRow row))
            {
                return null;
            }

            return row as T;
        }
    }
}
```

---

# 10. 测试配置表

## MonsterConfig.cs

```csharp
namespace ProjectAether.Config
{
    public class MonsterConfig
        : IConfigRow
    {
        public int Id { get; set; }

        public string Name
        {
            get;
            set;
        }

        public int Hp
        {
            get;
            set;
        }
    }
}
```

---

# 11. 测试代码

```csharp
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public class ConfigTest
        : MonoBehaviour
    {
        private void Start()
        {
            ConfigManager.Initialize();

            List<MonsterConfig>
                monsters =
                    new()
                    {
                        new MonsterConfig
                        {
                            Id = 1001,
                            Name = "Goblin",
                            Hp = 100
                        }
                    };

            ConfigManager.Load(
                monsters);

            MonsterConfig config =
                ConfigManager.Get<
                    MonsterConfig>(
                    1001);

            Debug.Log(
                config.Name);
        }
    }
}
```

---

# 12. Unity测试步骤

创建：

ConfigTestObject

---

挂载：

ConfigTest

---

运行项目

---

# 13. 预期输出

```text
Goblin
```

---

# 14. MVP验收标准

支持：

Load<T>()

Get<T>()

Shutdown()

---

支持：

多配置表管理

---

支持：

泛型配置查询

---

# 15. Git提交规范

Commit：

[Feature] Add ConfigManager

Tag：

v0.1.7

---

# 16. 后续扩展计划

V2：

ConfigContainer

---

V3：

JsonLoader

---

V4：

BinaryLoader

---

V5：

Excel导表系统

---

V6：

热更新配置系统

---

# 17. 文档关联

上游：

33_核心框架实现_ConfigModule

下游：

35_核心框架实现_ConfigTableFramework

---

# 18. 当前工程结构

Assets/GameScripts/Config

├── ConfigModule.cs

├── ConfigManager.cs

├── IConfigRow.cs

└── Game.Config.asmdef

---

# 19. 结论

ConfigManager 是整个配置系统核心。

未来所有配置表均通过：

ConfigManager.Load<T>()

ConfigManager.Get<T>()

进行统一管理。

它将成为 Character、Combat、Skill、Buff、AI 等系统的数据来源。
