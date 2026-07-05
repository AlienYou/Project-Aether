# 36_核心框架实现_Excel导表规范

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

建立 Project Aether 配置表标准。

统一规范：

* Excel结构
* 字段命名
* 类型定义
* 导出格式
* 代码生成规则

为后续：

MonsterConfig

SkillConfig

WeaponConfig

ItemConfig

BuffConfig

提供统一数据来源。

---

# 2. 所属程序集

无

本规范属于：

工具链标准文档。

---

# 3. 配置表目录规范

Excel目录：

```text
ConfigExcels/

├── Monster.xlsx
├── Skill.xlsx
├── Weapon.xlsx
├── Item.xlsx
└── Buff.xlsx
```

---

导出目录：

```text
ConfigOutput/

├── Json
├── Binary
└── CSharp
```

---

# 4. Excel命名规范

表名：

```text
Monster.xlsx
Skill.xlsx
Weapon.xlsx
Item.xlsx
Buff.xlsx
```

---

禁止：

```text
monster.xlsx

monster_data.xlsx

怪物配置.xlsx

技能表.xlsx
```

---

规则：

```text
PascalCase

英文命名

与Config类保持一致
```

---

# 5. Excel结构规范

每张表固定四行头。

---

第一行：

字段名

---

第二行：

字段类型

---

第三行：

字段说明

---

第四行开始：

数据内容

---

示例：

```text
----------------------------------------------------

Id      Name      Hp      Attack

int     string    int     int

唯一ID   名称      生命值   攻击力

1001    Goblin    100     20

1002    Orc       300     45

----------------------------------------------------
```

---

# 6. 字段命名规范

字段采用：

PascalCase

---

正确：

```text
Id

Name

Hp

Attack

MoveSpeed

Cooldown
```

---

错误：

```text
id

monster_name

hp

attack_value
```

---

# 7. 类型规范

支持：

```text
int

long

float

double

bool

string
```

---

支持数组：

```text
int[]

string[]
```

---

示例：

```text
RewardIds

int[]
```

数据：

```text
1001|1002|1003
```

---

# 8. 主键规范

每张配置表必须存在：

```text
Id
```

字段。

---

要求：

```text
唯一

不可重复

大于0
```

---

示例：

```text
1001

1002

1003
```

---

# 9. Config类生成规范

Monster.xlsx

自动生成：

```csharp
MonsterConfig.cs
```

---

示例：

```csharp
public class MonsterConfig
    : IConfigRow
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Hp { get; set; }

    public int Attack { get; set; }
}
```

---

# 10. Json导出规范

文件名：

```text
MonsterConfig.json
```

---

结构：

```json
[
  {
    "Id":1001,
    "Name":"Goblin",
    "Hp":100,
    "Attack":20
  }
]
```

---

# 11. Binary导出规范

文件名：

```text
MonsterConfig.bytes
```

---

用途：

```text
正式服

资源包

热更新
```

---

# 12. Id规划规范

Monster：

```text
10000~19999
```

---

Skill：

```text
20000~29999
```

---

Weapon：

```text
30000~39999
```

---

Item：

```text
40000~49999
```

---

Buff：

```text
50000~59999
```

---

Level：

```text
60000~69999
```

---

# 13. 错误检查规范

导表工具必须检查：

---

重复Id

---

空字段

---

类型错误

---

非法引用

---

导出失败必须中断。

---

# 14. MVP验收标准

支持：

统一Excel格式

---

支持：

统一字段规范

---

支持：

统一Id规划

---

支持：

Config类自动生成

---

支持：

Json导出

---

# 15. Git提交规范

Commit：

```bash
[Docs] Add Excel Config Standard
```

---

Tag：

```text
v0.1.9
```

---

# 16. 后续扩展计划

V2：

Excel导表工具

---

V3：

代码自动生成

---

V4：

Json导出器

---

V5：

Binary导出器

---

V6：

配置引用检查器

---

# 17. 文档关联

上游：

35_核心框架实现_ConfigTableFramework

---

下游：

37_核心框架实现_ConfigExportTool

---

# 18. 当前配置系统结构

```text
Game.Config

├── ConfigModule
├── ConfigManager
├── ConfigTable
├── IConfigRow

MonsterConfig
SkillConfig

Excel Standard
```

---

# 19. 结论

Excel导表规范是整个数据驱动体系的基础。

后续所有配置表、导表工具、代码生成器都必须严格遵循本规范。

任何新配置表接入前，必须先满足本规范要求。
