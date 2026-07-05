# ADR-003_Config系统架构修正

版本：v1.0

项目：Project Aether

状态：已采纳

日期：2026-07-06

---

# 1. 背景

在实现 ConfigManager 与 ConfigContainer 过程中发现：

原设计：

IConfigRow

↓

ConfigTable<T>

↓

ConfigManager

↓

Gameplay

---

随着配置系统扩展：

* 多配置表管理
* 配置热更新
* 配置索引
* 配置引用缓存

原架构扩展成本逐渐升高。

---

# 2. 问题分析

原 ConfigManager 内部结构：

```csharp
Dictionary<Type,
    Dictionary<int, IConfigRow>>
```

存在问题：

1. ConfigManager职责过重

2. 配置表逻辑全部堆积在Manager中

3. 不利于热更新

4. 不利于多Key索引

5. 不利于配置表独立扩展

---

同时：

ConfigTable<T>

与

ConfigContainer<T>

职责高度重叠。

---

# 3. 决策

废弃：

```text
ConfigTable<T>
```

统一采用：

```text
ConfigContainer<T>
```

作为运行时配置表容器。

---

# 4. 新架构

配置系统结构：

```text
IConfigRow

↓

ConfigContainer<T>

↓

ConfigManager

↓

IConfigLoader

↓

JsonConfigLoader
BinaryConfigLoader

↓

ConfigModule

↓

Gameplay
```

---

# 5. 各层职责

## IConfigRow

配置行标准接口。

定义：

```csharp
public interface IConfigRow
{
    int Id { get; }
}
```

---

## ConfigContainer<T>

负责：

* 单张配置表管理
* 查询
* 遍历
* 索引扩展

例如：

```text
MonsterConfig表

SkillConfig表

WeaponConfig表
```

---

## ConfigManager

负责：

* 管理所有配置表
* 注册配置表
* 获取配置表
* 提供统一访问入口

不负责：

* 数据存储
* 文件读取

---

## IConfigLoader

负责：

* 文件读取
* 数据反序列化
* ConfigContainer构建

---

## ConfigModule

负责：

* 生命周期管理
* Loader初始化
* ConfigManager初始化

---

# 6. ConfigManager标准结构

推荐实现：

```csharp
Dictionary<Type, object>
```

保存：

```text
ConfigContainer<MonsterConfig>

ConfigContainer<SkillConfig>

ConfigContainer<WeaponConfig>
```

---

# 7. 废弃内容

废弃：

```text
ConfigTable<T>
```

废弃原因：

与 ConfigContainer<T> 职责重复。

---

# 8. 文档修订

受影响文档：

34_核心框架实现_ConfigManager

35_核心框架实现_ConfigTableFramework

---

修订结果：

ConfigTable<T> 不再使用。

统一改为 ConfigContainer<T>。

---

# 9. 后续路线

下一阶段：

38_核心框架实现_ConfigLoader

实现：

```csharp
IConfigLoader

JsonConfigLoader

BinaryConfigLoader
```

完成配置系统闭环。

---

# 10. 结论

Project Aether 配置系统正式采用：

IConfigRow

↓

ConfigContainer<T>

↓

ConfigManager

↓

IConfigLoader

↓

ConfigModule

架构。

该架构支持：

* 大规模配置表
* 配置热更新
* 多Key索引
* 工业级扩展能力

后续所有文档与代码均以本ADR为准。
