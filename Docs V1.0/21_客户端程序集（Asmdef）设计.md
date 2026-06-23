# 21_客户端程序集（Asmdef）设计

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：正式版

---

# 1. 文档目标

定义 Project Aether 客户端程序集（Assembly Definition）架构。

负责：

* 模块隔离
* 编译加速
* 依赖控制
* 团队协作边界
* 工程模块划分

构建工业级 Unity 程序集体系。

---

# 2. 设计原则

遵循：

高内聚

低耦合

单向依赖

禁止循环引用

模块职责明确

---

# 3. Asmdef设计目标

解决：

大型工程编译缓慢

模块依赖混乱

多人协作冲突

代码边界模糊

---

通过程序集隔离实现：

模块化开发

增量编译

稳定架构演进

---

# 4. 总体程序集结构

Game

├── Core

├── Framework

├── Config

├── Resource

├── Network

├── UI

├── Character

├── Attribute

├── Combat

├── Skill

├── Buff

├── AI

├── Animation

├── Monster

├── Boss

├── Drop

├── Equipment

├── Inventory

├── Settlement

└── Account

---

# 5. Core层

程序集：

Game.Core

职责：

日志系统

工具类

数学库

时间系统

扩展方法

公共接口

基础类型定义

---

特点：

最底层模块

---

禁止依赖：

任何业务模块

---

# 6. Framework层

程序集：

Game.Framework

职责：

IOC容器

EventBus

FSM

StateMachine

Command

ServiceLocator

基础框架服务

---

依赖：

Game.Core

---

# 7. Config层

程序集：

Game.Config

职责：

配置读取

配置缓存

配置热更新

配置查询

---

依赖：

Game.Core

---

# 8. Resource层

程序集：

Game.Resource

职责：

Addressables

对象池

资源加载

资源释放

资源缓存

---

依赖：

Game.Core

Game.Framework

---

# 9. Network层

程序集：

Game.Network

职责：

TCP/KCP通信

协议管理

消息分发

同步框架

RPC调用

---

依赖：

Game.Core

Game.Framework

---

# 10. UI层

程序集：

Game.UI

职责：

UIManager

WindowSystem

MVVM

HUD

Popup

UI状态机

---

依赖：

Game.Resource

Game.Config

Game.Framework

---

# 11. Character层

程序集：

Game.Character

职责：

角色实体

角色控制器

角色状态机

角色行为

---

依赖：

Game.Core

Game.Framework

---

# 12. Attribute层

程序集：

Game.Attribute

职责：

属性系统

属性计算

属性同步

属性修正器

---

依赖：

Game.Character

---

# 13. Combat层

程序集：

Game.Combat

职责：

战斗计算

命中检测

伤害计算

战斗事件

---

依赖：

Game.Attribute

Game.Character

---

# 14. Skill层

程序集：

Game.Skill

职责：

技能释放

技能执行

技能逻辑

技能配置解析

---

依赖：

Game.Combat

Game.Attribute

---

# 15. Buff层

程序集：

Game.Buff

职责：

Buff管理

Buff执行

Buff生命周期

Buff同步

---

依赖：

Game.Attribute

Game.Skill

---

# 16. AI层

程序集：

Game.AI

职责：

行为树

决策系统

怪物AI

BossAI

---

依赖：

Game.Character

Game.Skill

---

# 17. Animation层

程序集：

Game.Animation

职责：

Animator管理

PlayableGraph

Timeline

RootMotion

动画事件

---

依赖：

Game.Character

---

# 18. Monster层

程序集：

Game.Monster

职责：

怪物工厂

怪物生成

怪物运行时管理

---

依赖：

Game.Character

Game.AI

Game.Skill

Game.Buff

---

# 19. Boss层

程序集：

Game.Boss

职责：

Boss阶段系统

Boss机制系统

Boss削韧系统

Boss狂暴系统

---

依赖：

Game.Monster

---

# 20. Drop层

程序集：

Game.Drop

职责：

奖励生成

掉落规则

掉落发放

掉落统计

---

依赖：

Game.Config

---

# 21. Equipment层

程序集：

Game.Equipment

职责：

装备系统

词条系统

强化系统

套装系统

---

依赖：

Game.Attribute

Game.Drop

---

# 22. Inventory层

程序集：

Game.Inventory

职责：

背包系统

物品管理

仓库系统

装备存储

---

依赖：

Game.Equipment

---

# 23. Settlement层

程序集：

Game.Settlement

职责：

副本结算

统计系统

评分系统

奖励展示

---

依赖：

Game.Drop

Game.Combat

---

# 24. Account层

程序集：

Game.Account

职责：

登录流程

角色选择

账号管理

云存档同步

---

依赖：

Game.Network

Game.UI

---

# 25. 推荐工程目录结构

Assets

├── GameScripts

│   ├── Core

│   ├── Framework

│   ├── Config

│   ├── Resource

│   ├── Network

│   ├── UI

│   ├── Gameplay

│   │   ├── Character

│   │   ├── Attribute

│   │   ├── Combat

│   │   ├── Skill

│   │   ├── Buff

│   │   ├── AI

│   │   ├── Animation

│   │   ├── Monster

│   │   ├── Boss

│   │   ├── Drop

│   │   ├── Equipment

│   │   ├── Inventory

│   │   └── Settlement

│   └── Account

---

# 26. 依赖关系总图

Core

↓

Framework

↓

Config

Resource

Network

↓

Character

↓

Attribute

↓

Combat

↓

Skill

↓

Buff

↓

AI

Animation

↓

Monster

↓

Boss

↓

Drop

↓

Equipment

↓

Inventory

↓

Settlement

↓

UI

↓

Account

---

# 27. 强制依赖规范

禁止：

UI → Combat

UI → Skill

UI → Monster

UI → Boss

---

必须通过：

Application Service

进行业务调用。

---

禁止：

Combat ↔ Skill

Buff ↔ Skill

Monster ↔ Boss

双向引用。

---

必须保持：

单向依赖结构。

---

# 28. Editor程序集设计

额外建立：

Game.Editor

职责：

配置导表

资源检查

代码生成

自动化工具

---

仅Editor模式编译。

---

# 29. ThirdParty程序集

第三方库独立管理：

ThirdParty

├── Odin

├── DOTween

├── UniTask

├── NewtonsoftJson

└── HybridCLR

---

禁止直接修改第三方源码。

---

# 30. MVP阶段程序集

第一阶段创建：

Game.Core

Game.Framework

Game.Config

Game.Resource

Game.Network

Game.UI

Game.Character

Game.Attribute

Game.Combat

Game.Skill

Game.Buff

Game.AI

Game.Animation

Game.Monster

Game.Boss

Game.Drop

Game.Equipment

Game.Inventory

Game.Settlement

Game.Account

---

# 31. 编译优化策略

采用：

增量编译

程序集隔离

Editor独立编译

ThirdParty独立编译

---

目标：

大型工程编译时间控制在30秒以内。

---

# 32. 风险分析

风险1：

循环依赖

解决：

严格审核Asmdef引用

---

风险2：

模块职责混乱

解决：

单一职责原则

---

风险3：

业务跨层访问

解决：

Application Service隔离

---

# 33. 文档关联

上游：

01_技术架构设计.md

02_框架设计.md

03_工程目录规范.md

---

下游：

22_服务端微服务架构设计.md

25_Unity工程初始化实施手册.md

26_核心框架代码规范.md

---

# 34. 实施顺序

步骤1：

创建目录结构

↓

步骤2：

创建Asmdef

↓

步骤3：

配置依赖关系

↓

步骤4：

验证编译

↓

步骤5：

开始框架开发

---

# 35. 结论

客户端程序集架构是 Project Aether 的工程骨架。

通过：

Game.Core

*

Game.Framework

*

业务模块程序集

*

严格单向依赖

构建工业级 Unity 项目结构。

为后续数十万行代码开发提供稳定基础。
