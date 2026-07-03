# 35_核心框架实现_ConfigTableFramework

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

建立 Project Aether 配置表框架标准。

统一规范：

* 配置表结构
* 配置行结构
* 配置查询方式
* 导表工具输出格式

为后续：

MonsterConfig

SkillConfig

WeaponConfig

ItemConfig

BuffConfig

提供统一实现标准。

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

Game.Network

---

# 4. 物理路径

Assets/GameScripts/Config

---

# 5. 文件列表

IConfigRow.cs

ConfigTable.cs

MonsterConfig.cs

SkillConfig.cs

---

# 6. 架构设计

统一结构：

```text
ConfigTable<T>

├── MonsterConfig
├── SkillConfig
├── WeaponConfig
├── ItemConfig
└── BuffConfig
```

统一查询：

```csharp
ConfigManager.Get<MonsterConfig>(1001)

ConfigManager.Get<SkillConfig>(2001)
```

统一数据结构：

```csharp
public class MonsterConfig
    : IConfigRow
{
}
```

---

# 7. 生命周期

配置表本身无生命周期。

由：

ConfigManager

统一管理。

---

# 8. 接口设计

## IConfigRow.cs

（上一文档已实现）

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

# 9. 通用配置表

## ConfigTable.cs

```csharp
using System.Collections.Generic;

namespace ProjectAether.Config
{
    public class ConfigTable<T>
        where T : IConfigRow
    {
        private readonly Dictionary<int, T>
            _rows =
                new();

        public void Add(T row)
        {
            _rows[row.Id] = row;
        }

        public T Get(int id)
        {
            _rows.TryGetValue(
                id,
                out T row);

            return row;
        }

        public IReadOnlyDictionary<
            int,
            T> Rows => _rows;
    }
}
```

---

# 10. 示例配置表

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

        public int Attack
        {
            get;
            set;
        }

        public float MoveSpeed
        {
            get;
            set;
        }
    }
}
```

---

## SkillConfig.cs

```csharp
namespace ProjectAether.Config
{
    public class SkillConfig
        : IConfigRow
    {
        public int Id { get; set; }

        public string Name
        {
            get;
            set;
        }

        public int Damage
        {
            get;
            set;
        }

        public float Cooldown
        {
            get;
            set;
        }
    }
}
```

---

# 11. 配置表命名规范

配置表类：

```text
MonsterConfig

SkillConfig

WeaponConfig

ItemConfig

BuffConfig
```

---

禁止：

```text
MonsterData

MonsterInfo

MonsterTableData
```

---

统一后缀：

```text
Config
```

---

# 12. 字段规范

第一列必须：

```csharp
public int Id;
```

---

推荐顺序：

```text
Id

Name

Desc

业务字段
```

示例：

```csharp
Id

Name

Hp

Attack

MoveSpeed
```

---

# 13. 导表规范预留

未来Excel：

```text
Monster.xlsx
```

生成：

```csharp
MonsterConfig.cs
```

以及：

```text
MonsterConfig.bytes
```

---

# 14. Unity测试步骤

创建：

MonsterConfig

SkillConfig

实例对象。

---

使用：

```csharp
ConfigManager.Load()
```

加载。

---

验证：

```csharp
ConfigManager.Get()
```

返回正确数据。

---

# 15. MVP验收标准

支持：

IConfigRow

---

支持：

ConfigTable<T>

---

支持：

MonsterConfig

---

支持：

SkillConfig

---

支持：

统一命名规范

---

# 16. Git提交规范

Commit：

[Feature] Add Config Table Framework

Tag：

v0.1.8

---

# 17. 后续扩展计划

V2：

Excel导表规范

---

V3：

ConfigContainer

---

V4：

JsonLoader

---

V5：

BinaryLoader

---

V6：

Addressables Config Loader

---

# 18. 文档关联

上游：

34_核心框架实现_ConfigManager

---

下游：

36_核心框架实现_Excel导表规范

---

# 19. 当前工程结构

Assets/GameScripts/Config

├── ConfigModule.cs

├── ConfigManager.cs

├── ConfigTable.cs

├── IConfigRow.cs

├── MonsterConfig.cs

├── SkillConfig.cs

└── Game.Config.asmdef

---

# 20. 结论

ConfigTableFramework 建立了 Project Aether 配置系统统一标准。

未来所有配置表均遵循：

IConfigRow

*

ConfigTable<T>

规范。

后续 Excel 导表工具、Json Loader、Binary Loader 都将基于该标准实现。
