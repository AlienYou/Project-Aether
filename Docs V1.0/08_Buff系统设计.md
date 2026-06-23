# 08_Buff系统设计（Buff System Design）

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：正式版

---

# 1. 文档目标

定义统一Buff系统。

负责：

* 增益效果
* 减益效果
* 控制效果
* 持续伤害
* 持续治疗
* 光环效果
* 状态叠层
* 属性修改

---

# 2. 设计目标

支持：

Buff

Debuff

Control

DOT

HOT

Aura

Passive Effect

Boss机制

---

# 3. 系统架构

BuffSystem

├── BuffComponent

├── BuffRuntime

├── BuffExecutor

├── BuffTrigger

├── BuffModifier

├── BuffStack

└── BuffConfig

---

# 4. Buff定义

Buff本质：

规则修改器。

例如：

攻击力+20%

↓

修改属性规则

---

燃烧

↓

周期触发伤害

---

冰冻

↓

修改角色状态

---

# 5. Buff生命周期

Add

↓

Activate

↓

Tick

↓

Expire

↓

Remove

---

# 6. BuffRuntime

运行时实例。

包含：

BuffId

Caster

Target

Duration

Stack

RemainingTime

---

# 7. BuffComponent

角色Buff容器。

负责：

添加

移除

刷新

查询

更新

---

# 8. Buff分类

支持：

AttributeBuff

ControlBuff

DotBuff

HotBuff

AuraBuff

PassiveBuff

---

# 9. AttributeBuff

属性修改。

例如：

Attack +20%

HP +10%

MoveSpeed +30%

---

# 10. ControlBuff

控制效果。

支持：

Stun

Freeze

Silence

Root

Fear

KnockDown

---

# 11. DOT系统

Damage Over Time

支持：

Burn

Poison

Bleed

Shock

---

周期：

Tick

↓

DamagePipeline

---

# 12. HOT系统

Heal Over Time

支持：

治疗持续恢复

生命回复

再生效果

---

# 13. Aura系统

光环效果。

作用范围：

Circle

Box

Sector

---

例如：

队友攻击+10%

---

# 14. PassiveBuff

被动效果。

支持：

常驻规则。

例如：

暴击率+5%

火伤+20%

---

# 15. BuffTag

标签系统。

支持：

Fire

Ice

Lightning

Poison

Control

Boss

---

用于：

规则判断。

---

# 16. Trigger系统

BuffTrigger

支持：

OnAttack

OnHit

OnKill

OnCrit

OnDamaged

OnDeath

---

# 17. Trigger示例

暴击后：

↓

攻击力+10%

↓

持续5秒

---

# 18. BuffModifier

支持：

AttributeModifier

DamageModifier

SkillModifier

CooldownModifier

---

# 19. Stack系统

叠层机制。

支持：

Independent

Shared

Replace

Refresh

---

# 20. Stack规则

示例：

燃烧

最大：

5层

---

每层：

10伤害

---

总计：

50伤害

---

# 21. Duration规则

支持：

FixedDuration

Infinite

TriggerDuration

---

# 22. 刷新规则

支持：

RefreshTime

RefreshStack

Replace

Ignore

---

# 23. 优先级系统

Priority

Low

Medium

High

Boss

---

用于：

状态覆盖

---

# 24. 控制抗性

ControlResistance

支持：

StunResist

FreezeResist

SilenceResist

---

# 25. 免疫系统

支持：

ControlImmune

FireImmune

PoisonImmune

DamageImmune

---

# 26. 驱散系统

Dispel

支持：

Cleanse

Purge

RemoveTag

---

# 27. Buff事件

BuffAddedEvent

BuffRemovedEvent

BuffTickEvent

BuffExpiredEvent

---

# 28. 与属性系统关系

Buff

↓

AttributeModifier

↓

AttributeSystem

↓

FinalAttribute

---

# 29. 与战斗系统关系

DOT

↓

DamagePipeline

↓

CombatSystem

---

# 30. 与技能系统关系

Skill

↓

ApplyBuff

↓

BuffSystem

---

统一入口。

---

# 31. Boss机制支持

支持：

阶段Buff

狂暴Buff

护盾Buff

机制Buff

---

# 32. 网络同步

同步：

BuffId

Stack

Duration

---

客户端：

本地表现。

---

# 33. Buff配置

BuffConfig.xlsx

字段：

BuffId

Name

Type

Duration

StackLimit

Trigger

---

# 34. BuffManager

负责：

Buff注册

Buff创建

Buff销毁

Buff查询

---

# 35. asmdef设计

程序集：

Game.Buff

依赖：

Game.Character

Game.Attribute

Game.Combat

Game.Skill

Game.Framework.Event

---

# 36. 第一阶段实现

实现：

BuffComponent

BuffRuntime

AttributeBuff

DotBuff

ControlBuff

---

支持：

增益

减益

DOT

控制

---

# 37. 第二阶段实现

实现：

Aura

PassiveBuff

TriggerSystem

DispelSystem

---

形成完整Buff生态。

---

# 38. 风险分析

风险1：

Buff数量爆炸

解决：

配置驱动

---

风险2：

性能问题

解决：

Tick优化

事件驱动

---

风险3：

状态冲突

解决：

Tag系统

优先级系统

---

# 39. 文档关联

上游：

05_属性系统设计.md

06_战斗系统设计.md

07_技能系统设计.md

---

下游：

09_AI系统设计.md

10_动画系统设计.md

18_装备系统设计.md

所有状态效果统一通过Buff System实现。

---

# 40. 结论

Buff System 是 Project Aether 的规则扩展中心。

负责统一：

增益

减益

控制

DOT

HOT

光环

被动

通过 Trigger + Modifier 架构实现工业级可扩展Buff系统。
