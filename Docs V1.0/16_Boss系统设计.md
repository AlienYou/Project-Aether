# 16_Boss系统设计（Boss System Design）

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：正式版

---

# 1. 文档目标

定义 Project Aether Boss系统架构。

负责：

* 世界Boss
* 副本Boss
* 周常Boss
* Raid Boss
* 剧情Boss

构建工业级Boss内容生产体系。

---

# 2. 设计目标

支持：

Single Boss

Multi Boss

Raid Boss

Phase Boss

Mechanic Boss

---

统一通过：

BossSystem

管理。

---

# 3. 系统定位

系统关系：

Boss

↓

Monster

↓

Character

↓

Combat

↓

Skill

↓

Buff

↓

AI

---

Boss本质上属于高级Monster。

---

# 4. 系统架构

BossSystem

├── BossManager

├── BossPhaseSystem

├── BossSkillLoop

├── BossMechanicSystem

├── BossBreakSystem

├── BossRageSystem

└── BossRuntime

---

# 5. BossManager

负责：

创建

销毁

阶段管理

状态管理

Boss事件广播

---

# 6. BossRuntime

运行时数据。

包含：

BossId

CurrentPhase

HP

BreakValue

RageValue

MechanicState

---

# 7. Boss分类

支持：

DungeonBoss

WorldBoss

WeeklyBoss

RaidBoss

StoryBoss

---

# 8. Dungeon Boss

特点：

单队挑战

固定机制

阶段切换

---

# 9. World Boss

特点：

开放世界刷新

多人参与

共享伤害统计

---

# 10. Weekly Boss

特点：

周期奖励

复杂机制

高价值掉落

---

# 11. Raid Boss

特点：

多人团队

多阶段机制

高复杂度设计

---

# 12. Boss阶段系统

BossPhaseSystem

负责：

阶段切换

技能组切换

机制切换

---

# 13. Phase结构

支持：

Phase1

Phase2

Phase3

Phase4

---

# 14. Phase触发条件

支持：

HP百分比

时间

机制完成

剧情事件

---

# 15. 示例

Boss HP

100%

↓

70%

↓

Phase2

↓

40%

↓

Phase3

---

# 16. Phase能力变化

支持：

技能新增

技能强化

Buff强化

机制变化

---

# 17. Boss技能循环

BossSkillLoop

负责：

技能选择

技能顺序

技能冷却

---

# 18. 技能分类

支持：

NormalSkill

SpecialSkill

UltimateSkill

MechanicSkill

---

# 19. 技能循环示例

Attack

↓

Attack

↓

Dash

↓

AOE

↓

Ultimate

↓

Repeat

---

# 20. Rage系统

Boss狂暴机制。

---

# 21. Rage来源

支持：

时间

血量

机制失败

特殊触发

---

# 22. Rage效果

支持：

攻击提升

移速提升

技能强化

冷却缩短

---

# 23. Break系统

削韧系统。

动作游戏核心机制之一。

---

# 24. BreakValue

Boss拥有：

BreakGauge

---

玩家攻击：

降低韧性。

---

# 25. Break状态

BreakValue

<=0

↓

Break

↓

Boss虚弱

---

# 26. Break奖励

支持：

额外伤害

控制窗口

特殊处决

---

# 27. Weakness系统

支持：

火弱点

冰弱点

雷弱点

物理弱点

---

# 28. 弱点命中

触发：

额外伤害

额外Break

特殊Buff

---

# 29. Mechanic系统

Boss机制核心。

---

# 30. Mechanic类型

支持：

区域机制

机关机制

躲避机制

协作机制

解谜机制

---

# 31. 机制事件

支持：

MechanicStart

MechanicSuccess

MechanicFail

MechanicEnd

---

# 32. Boss召唤系统

支持：

小怪召唤

分身召唤

机关召唤

---

# 33. Boss区域控制

支持：

毒圈

火圈

雷区

安全区

---

# 34. Boss动画系统

支持：

阶段动画

机制动画

处决动画

过场动画

---

# 35. Boss过场

支持：

Phase Cutscene

Intro Cutscene

Death Cutscene

---

# 36. 仇恨系统

采用：

ThreatSystem

统一管理。

---

# 37. 目标选择

支持：

最高仇恨

随机目标

指定目标

机制目标

---

# 38. 网络架构

服务器权威。

---

Boss AI

Boss机制

Boss伤害

服务器执行。

---

# 39. 同步内容

同步：

HP

Phase

BreakValue

Skill

Animation

MechanicState

---

# 40. Boss配置

BossConfig.xlsx

字段：

BossId

PhaseGroup

SkillGroup

BreakValue

DropGroup

---

# 41. Phase配置

BossPhaseConfig.xlsx

字段：

BossId

Phase

TriggerHP

SkillGroup

BuffGroup

---

# 42. Mechanic配置

BossMechanicConfig.xlsx

字段：

MechanicId

Type

Duration

Reward

Penalty

---

# 43. 资源规范

BossPrefab

↓

Animator

↓

BossComponent

↓

BossController

---

# 44. asmdef设计

程序集：

Game.Boss

依赖：

Game.Monster

Game.AI

Game.Skill

Game.Buff

Game.Combat

---

# 45. 第一阶段实现

实现：

BossManager

BossPhaseSystem

BossSkillLoop

BreakSystem

---

支持：

单Boss战斗

阶段切换

削韧机制

---

# 46. 第二阶段实现

实现：

MechanicSystem

RaidBoss

Cutscene

高级机制

---

形成完整Boss生态。

---

# 47. 风险分析

风险1：

Boss逻辑复杂

解决：

Phase驱动

---

风险2：

机制不可维护

解决：

配置化设计

---

风险3：

多人同步压力

解决：

服务器权威

AOI同步

---

# 48. 文档关联

上游：

09_AI系统设计.md

10_动画系统设计.md

15_怪物系统设计.md

---

下游：

17_掉落系统设计.md

18_装备系统设计.md

19_结算系统设计.md

---

所有Boss内容统一通过 Boss System 构建。

---

# 49. MVP阶段目标

第一阶段实现：

1个副本Boss

3个阶段

削韧机制

狂暴机制

掉落奖励

---

支持完整Boss战流程。

---

# 50. 长期演进规划

V1：

副本Boss

↓

V2：

世界Boss

↓

V3：

周本Boss

↓

V4：

Raid Boss

---

架构保持兼容。

---

# 51. 结论

Boss System 是 Project Aether PvE体验核心。

通过：

BossPhaseSystem

*

BossSkillLoop

*

BreakSystem

*

RageSystem

*

MechanicSystem

构建工业级Boss战斗框架。

支撑长期运营与高质量动作战斗内容生产。
