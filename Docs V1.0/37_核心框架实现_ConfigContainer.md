# 37_核心框架实现_ConfigContainer

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

建立配置表运行时容器。

负责：

* 存储单张配置表
* 提供配置查询
* 提供配置遍历
* 隔离 ConfigManager 与配置数据

为后续：

MonsterConfig

SkillConfig

WeaponConfig

BuffConfig

提供统一容器实现。

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

Assets/GameScripts/Config/Runtime

---

# 5. 文件列表

ConfigContainer.cs

IConfigRow.cs

---

# 6. 架构设计

整体结构：

```text id="k5p2w9"
ConfigManager

├── ConfigContainer<MonsterConfig>
├── ConfigContainer<SkillConfig>
├── ConfigContainer<WeaponConfig>
└── ConfigContainer<BuffConfig>
```

职责划分：

ConfigManager

负责：

* 管理所有配置表

---

ConfigContainer<T>

负责：

* 管理单张配置表

---

IConfigRow

负责：

* 配置行统一接口

---

# 7. 生命周期

创建：

ConfigLoader加载完成后

---

运行：

游戏全生命周期

---

释放：

ConfigManager.Shutdown()

---

# 8. 类设计

## ConfigContainer<T>

职责：

管理一张配置表。

例如：

```text id="n9z7b1"
MonsterConfig表

SkillConfig表

WeaponConfig表
```

---

内部结构：

```csharp id="t3m8v5"
Dictionary<int, T>
```

---

# 9. 代码实现

## ConfigContainer.cs

```csharp id="x6c4n2"
using System.Collections.Generic;

namespace ProjectAether.Config
{
    public class ConfigContainer<T>
        where T : IConfigRow
    {
        private readonly Dictionary<int, T>
            _configs =
                new();

        public int Count =>
            _configs.Count;

        public void Add(T row)
        {
            _configs[row.Id] = row;
        }

        public T Get(int id)
        {
            _configs.TryGetValue(
                id,
                out T row);

            return row;
        }

        public bool Contains(int id)
        {
            return _configs.ContainsKey(id);
        }

        public IReadOnlyDictionary<int, T>
            GetAll()
        {
            return _configs;
        }

        public void Clear()
        {
            _configs.Clear();
        }
    }
}
```

---

# 10. ConfigManager重构

旧结构：

```csharp id="e2f9m6"
Dictionary<Type,
Dictionary<int, IConfigRow>>
```

---

新结构：

```csharp id="v7n3k8"
Dictionary<Type, object>
```

保存：

```text id="h1w6q5"
ConfigContainer<MonsterConfig>

ConfigContainer<SkillConfig>

ConfigContainer<WeaponConfig>
```

---

优势：

减少耦合

方便扩展

支持热更新

支持增量替换

---

# 11. 测试配置

## MonsterConfig.cs

```csharp id="j8r4t2"
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

# 12. 测试代码

```csharp id="m4k1x7"
using UnityEngine;

namespace ProjectAether.Config
{
    public class ConfigContainerTest
        : MonoBehaviour
    {
        private void Start()
        {
            ConfigContainer<MonsterConfig>
                container =
                    new();

            container.Add(
                new MonsterConfig
                {
                    Id = 1001,
                    Name = "Goblin",
                    Hp = 100
                });

            MonsterConfig monster =
                container.Get(1001);

            Debug.Log(
                monster.Name);
        }
    }
}
```

---

# 13. Unity测试步骤

创建：

ConfigContainerTestObject

---

挂载：

ConfigContainerTest

---

运行项目

---

# 14. 预期输出

```text id="c7b9w4"
Goblin
```

---

# 15. MVP验收标准

支持：

Add()

Get()

Contains()

Clear()

---

支持：

泛型配置表

---

支持：

多配置表并存

---

# 16. Git提交规范

Commit：

[Feature] Add ConfigContainer

Tag：

v0.1.10

---

# 17. 后续扩展计划

V2：

配置索引

---

V3：

多Key查询

---

V4：

配置引用缓存

---

V5：

热更新替换

---

# 18. 文档关联

上游：

34_核心框架实现_ConfigManager

35_核心框架实现_ConfigTableFramework

---

下游：

38_核心框架实现_ConfigLoader

---

# 19. 当前工程结构

Assets/GameScripts/Config

├── Runtime
│   └── ConfigContainer.cs
│
├── ConfigModule.cs
├── ConfigManager.cs
├── ConfigTable.cs
└── IConfigRow.cs

---

# 20. 结论

ConfigContainer 是配置系统运行时数据容器。

每张配置表对应一个 ConfigContainer。

ConfigManager 不再直接管理配置数据，而是统一管理多个 ConfigContainer。

这是后续配置热更新、配置索引、多Key查询的重要基础。
