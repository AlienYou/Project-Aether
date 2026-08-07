# Project Aether 项目里程碑规范

> **文件名：** `09_ProjectMilestone.md`  
> **文档编号：** PAS-009  
> **版本：** v1.0  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **Unity 版本：** Unity 2022.3.51f1c1  
> **最后更新：** 2026-08-07  

---

## 1. 文档目的

本文档定义 Project Aether 的统一项目里程碑规范，用于规划项目阶段、管理范围、设置质量门禁、评估风险、判断阶段完成状态并支持版本发布。

本规范的目标是：

- 将长期目标拆分为可验证的阶段成果。
- 为每个阶段定义明确的进入条件和退出条件。
- 防止“功能很多，但没有形成可交付版本”的情况。
- 保证架构、实现、测试、文档和发布能力同步推进。
- 让项目进度基于可交付结果，而不是代码数量或主观完成度。
- 提前暴露范围、依赖、质量和技术风险。
- 为团队扩张、跨角色协作和 AI 协作提供稳定计划基线。
- 支持 Project Aether 从原型、技术验证逐步演进到可发布商业项目。

---

## 2. 适用范围

本规范适用于：

- 项目总计划。
- 年度与季度计划。
- 原型阶段。
- Vertical Slice。
- Pre-Alpha。
- Alpha。
- Beta。
- Release Candidate。
- 正式发布。
- 版本迭代。
- 技术专项。
- 核心模块建设。
- 架构升级。
- 网络与服务端建设。
- 内容生产。
- 性能优化。
- 发布准备。
- Hotfix 与版本维护。

---

## 3. 核心原则

### 3.1 Milestone Represents a Verifiable Outcome

里程碑必须代表一个可以被验证的结果。

正确：

```text
完成可联网的单场景战斗闭环，
两名客户端可进入同一房间、移动、攻击、受击和结算。
```

不推荐：

```text
网络系统完成 80%。
```

---

### 3.2 Exit Criteria Must Be Defined First

里程碑开始前必须先定义退出条件。

没有退出条件的里程碑无法客观关闭。

---

### 3.3 Scope Must Be Controlled

里程碑范围必须明确：

- Included。
- Excluded。
- Deferred。
- Optional。

新增范围必须重新评估时间、风险和质量。

---

### 3.4 Quality Is Part of the Milestone

里程碑不能只交付功能。

还必须考虑：

- 编译。
- 测试。
- 性能。
- 内存。
- 稳定性。
- 文档。
- 构建。
- 回滚。
- 已知问题。

---

### 3.5 Progress Is Measured by Completed Deliverables

进度应基于：

- 已完成交付物。
- 已通过验收。
- 已合入主线。
- 已通过测试。
- 已关闭风险。

不以：

- 代码行数。
- 已创建文件数量。
- 已投入时间。
- 主观百分比。

作为主要进度依据。

---

### 3.6 Risks Are First-Class Work

重大风险必须进入计划。

风险验证任务与功能任务具有同等优先级。

---

### 3.7 Architecture Must Precede Scale

项目在扩大功能和团队前，必须建立足够稳定的：

- 目录规范。
- 文档规范。
- Git 规范。
- 编码规范。
- Review 规范。
- 测试规范。
- AI 规范。
- 核心架构。
- 构建基线。

---

### 3.8 Milestones Must Be Replanable

计划不是永久不变的承诺。

当事实变化时，应基于数据调整：

- 范围。
- 顺序。
- 时间。
- 资源。
- 风险。
- 质量目标。

调整必须保留记录。

---

## 4. 里程碑类型

Project Aether 使用以下里程碑类型：

| 类型 | 用途 |
|---|---|
| Project Milestone | 项目整体阶段 |
| Product Milestone | 产品功能和用户体验阶段 |
| Technical Milestone | 架构或基础设施阶段 |
| Content Milestone | 关卡、角色、任务等内容阶段 |
| Quality Milestone | 性能、稳定性、测试阶段 |
| Release Milestone | 版本候选和发布阶段 |
| Research Milestone | 技术验证和方案选择阶段 |
| Operations Milestone | 线上运营和维护阶段 |

---

## 5. 里程碑状态

统一状态：

```text
Planned
Ready
In Progress
At Risk
Blocked
In Validation
Completed
Cancelled
```

---

## 6. 状态定义

### 6.1 Planned

里程碑已经提出，但尚未满足启动条件。

---

### 6.2 Ready

已经满足进入条件，可以正式启动。

---

### 6.3 In Progress

当前正在执行。

---

### 6.4 At Risk

仍在推进，但目标、时间或质量存在明显风险。

必须记录：

- 风险。
- 影响。
- 负责人。
- 缓解措施。
- 决策时间。

---

### 6.5 Blocked

关键依赖未满足，无法继续推进。

---

### 6.6 In Validation

主要实现已完成，正在进行：

- 验收。
- 回归。
- 性能。
- 内存。
- 构建。
- 文档检查。

---

### 6.7 Completed

所有退出条件已满足。

---

### 6.8 Cancelled

里程碑已取消。

必须记录取消原因和遗留影响。

---

## 7. 里程碑基本结构

每个里程碑必须包含：

```markdown
# Milestone Name

## Objective

## User / Project Value

## Entry Criteria

## Scope

## Non-Scope

## Deliverables

## Dependencies

## Risks

## Quality Targets

## Exit Criteria

## Validation Plan

## Rollback / Contingency

## Owner

## Target Date

## Status
```

---

## 8. Objective

Objective 必须描述阶段结果，而不是任务列表。

正确：

```text
建立 Project Aether 可持续开发的工程规范和核心框架基线，
使后续模块能够在统一目录、文档、Git、编码、评审和测试规范下开发。
```

不推荐：

```text
写十篇文档，创建几个文件夹。
```

---

## 9. User / Project Value

每个里程碑必须说明价值。

价值可能是：

- 提供可玩的核心体验。
- 降低技术风险。
- 支持多人开发。
- 支持网络同步。
- 支持内容生产。
- 支持版本发布。
- 降低崩溃率。
- 降低资源加载时间。
- 提高开发效率。
- 提高可维护性。

---

## 10. Entry Criteria

里程碑启动前必须满足的条件。

示例：

- 上一里程碑已完成。
- 架构已评审。
- 关键依赖已确认。
- 人员已分配。
- 测试环境可用。
- 目标设备可用。
- 资源和配置格式已确认。
- 服务端接口已准备。
- 高风险 Spike 已完成。

---

## 11. Scope

Scope 必须使用明确可交付项。

示例：

```text
Included:
- ModuleManager 生命周期。
- ResourceManager 基础加载。
- ConfigManager 基础读取。
- 自动化 EditMode 测试。
- 核心架构文档。

Excluded:
- 远程资源热更新。
- 完整战斗系统。
- 大世界流式加载。
```

---

## 12. Deliverables

交付物可以包括：

- 可运行 Build。
- 源码。
- 模块。
- 测试。
- 文档。
- 工具。
- 配置。
- 资源。
- 性能数据。
- 测试报告。
- 发布说明。
- 迁移脚本。

每个交付物必须可检查。

---

## 13. Dependencies

依赖必须记录：

- 依赖名称。
- 提供方。
- 版本。
- 目标时间。
- 验收方式。
- 失败影响。
- 替代方案。

---

## 14. Risks

里程碑风险至少评估：

- 技术。
- 进度。
- 人员。
- 内容。
- 性能。
- 内存。
- 网络。
- 工具。
- 第三方依赖。
- 平台。
- 发布。
- 安全。
- 数据兼容。

---

## 15. Quality Targets

里程碑应根据阶段定义质量目标。

示例：

```text
- Editor 无编译错误。
- 自动化测试全部通过。
- 核心流程无 Blocker。
- 目标场景平均帧率不低于 60 FPS。
- 单帧 GC Alloc 接近 0 B。
- 连续运行 2 小时无明显内存增长。
```

---

## 16. Exit Criteria

退出条件必须：

- 明确。
- 可测量。
- 可验证。
- 与目标一致。
- 不使用模糊词。

不推荐：

```text
系统基本稳定。
```

推荐：

```text
连续执行 100 次资源加载和释放流程，
引用计数无下溢，
Addressables Handle 无残留，
自动化测试全部通过。
```

---

## 17. Validation Plan

必须说明如何验证：

- 功能。
- 测试。
- 性能。
- 内存。
- 网络。
- 构建。
- 平台。
- 文档。
- 发布。

---

## 18. Rollback / Contingency

高风险里程碑必须说明：

- 失败时如何回滚。
- 是否可降级。
- 是否可关闭功能。
- 是否可切换旧实现。
- 是否需要保留旧数据格式。
- 是否需要 Feature Flag。

---

## 19. 里程碑负责人

每个里程碑必须有一个明确 Accountable Owner。

负责人负责：

- 目标清晰。
- 范围控制。
- 依赖跟踪。
- 风险升级。
- 验收组织。
- 状态更新。
- 最终关闭。

负责人可以不执行所有任务，但必须对结果负责。

---

## 20. 里程碑日期

必须记录：

- Planned Start。
- Planned End。
- Actual Start。
- Actual End。
- Review Date。
- Freeze Date。
- Release Date。

日期调整必须记录原因。

---

## 21. 项目阶段总览

Project Aether 推荐使用以下整体阶段：

```text
M0 Project Foundation
M1 Core Framework
M2 Technical Prototype
M3 Vertical Slice
M4 Pre-Alpha
M5 Alpha
M6 Beta
M7 Release Candidate
M8 Launch
M9 Live Operations
```

阶段名称可以根据项目实际调整，但退出标准必须保留。

---

## 22. M0 Project Foundation

### 22.1 目标

建立项目规范、目录、仓库、文档、开发环境和基础技术决策。

### 22.2 范围

- Unity 版本固定。
- Git 仓库。
- 目录规范。
- 文档规范。
- Git 规范。
- 编码规范。
- Review 规范。
- 测试规范。
- AI 协作规范。
- 项目工作流。
- 项目里程碑规范。
- 基础 Package 方案。
- asmdef 基线。
- CI 基础规划。

### 22.3 退出条件

- [ ] Unity 版本固定为 `2022.3.51f1c1`。
- [ ] `Docs/00_ProjectStandard/` 文档完整。
- [ ] 项目目录已建立。
- [ ] Git 仓库可用。
- [ ] `.gitignore` 和 `.gitattributes` 已配置。
- [ ] 核心 Package 版本已记录。
- [ ] 编译基线通过。
- [ ] 文档索引更新。
- [ ] 团队理解并接受规范。

---

## 23. M1 Core Framework

### 23.1 目标

建立 Project Aether 的核心运行框架和基础模块生命周期。

### 23.2 范围

- Bootstrap。
- ModuleManager。
- Service 注册。
- Logging。
- Resource 基础框架。
- Config 基础框架。
- Pool 基础框架。
- 基础错误处理。
- 基础测试框架。
- 基础 Editor 工具。

### 23.3 退出条件

- [ ] Bootstrap 可稳定启动和关闭。
- [ ] 模块生命周期完整。
- [ ] 模块顺序可验证。
- [ ] 资源加载和释放闭环可用。
- [ ] 配置加载和校验可用。
- [ ] 对象池基础流程可用。
- [ ] 核心测试通过。
- [ ] 架构文档完整。
- [ ] 无已知 Blocker。
- [ ] 可生成基础 Runtime Build。

---

## 24. M2 Technical Prototype

### 24.1 目标

验证核心技术链路是否可以支撑目标动作游戏。

### 24.2 范围

- 基础角色控制。
- 基础镜头。
- 基础战斗。
- 基础动画。
- 基础资源加载。
- 基础配置。
- FishNet 连接。
- 基础角色同步。
- 简单场景。
- 基础 UI。

### 24.3 重点

此阶段重点是验证技术可行性，不追求最终内容质量。

### 24.4 退出条件

- [ ] 角色可移动。
- [ ] 镜头可控制。
- [ ] 基础攻击链路可运行。
- [ ] 受击与死亡可运行。
- [ ] 客户端可连接服务端。
- [ ] 多客户端可进入同一场景。
- [ ] 基础状态可同步。
- [ ] 核心技术风险有结论。
- [ ] 原型代码是否保留已明确。
- [ ] 下一阶段架构调整已记录。

---

## 25. M3 Vertical Slice

### 25.1 目标

交付一个接近目标品质的完整可玩切片。

### 25.2 范围

通常包括：

- 一个完整角色。
- 一套完整战斗循环。
- 一个完整关卡。
- 一种或多种敌人。
- 完整 UI 流程。
- 基础音效和特效。
- 网络联机。
- 结算。
- 存档或进度。
- 目标平台 Build。

### 25.3 退出条件

- [ ] 从启动到结算形成完整闭环。
- [ ] 核心体验达到目标方向。
- [ ] 主要技术链路已验证。
- [ ] 内容生产流程已验证。
- [ ] 性能预算初步达标。
- [ ] 网络体验达到阶段目标。
- [ ] 关键工具可支持内容生产。
- [ ] 团队可估算后续规模化成本。
- [ ] 项目主要风险已重新评估。

---

## 26. M4 Pre-Alpha

### 26.1 目标

完成主要系统骨架，并开始规模化内容生产。

### 26.2 范围

- 主要 Gameplay 系统。
- 主要网络系统。
- 主要 UI 框架。
- 主要内容工具。
- 资源流水线。
- 配置流水线。
- 存档。
- 账号基础。
- 构建流水线。
- 自动化测试扩展。

### 26.3 退出条件

- [ ] 主要系统已建立。
- [ ] 核心系统接口趋于稳定。
- [ ] 内容生产流程可重复。
- [ ] 自动化 Build 可用。
- [ ] 关键测试可自动执行。
- [ ] 主要模块有 Architecture 文档。
- [ ] 性能问题已有跟踪机制。
- [ ] 已知技术债已分类。

---

## 27. M5 Alpha

### 27.1 目标

实现 Feature Complete。

### 27.2 Feature Complete

Feature Complete 表示：

- 计划中的主要功能已经实现。
- 可以通过配置或内容继续完善。
- 不再新增大型核心系统。
- 后续重点转向稳定性、性能、内容和体验。

### 27.3 退出条件

- [ ] 主要功能完整。
- [ ] 主要内容流程完整。
- [ ] 核心网络功能完整。
- [ ] 存档和版本兼容流程可用。
- [ ] 主要平台可构建。
- [ ] 所有 P0 用例可执行。
- [ ] 无未接受的架构 Blocker。
- [ ] 进入 Feature Freeze 计划明确。

---

## 28. M6 Beta

### 28.1 目标

达到 Content Complete，并集中解决质量问题。

### 28.2 Content Complete

表示计划发布的主要内容已经进入项目。

### 28.3 阶段重点

- Bug 修复。
- 性能优化。
- 内存优化。
- 稳定性。
- 网络质量。
- 平台兼容。
- 本地化。
- 安全。
- 用户体验。
- 运营准备。

### 28.4 退出条件

- [ ] 主要内容完整。
- [ ] Feature Freeze 已执行。
- [ ] 核心回归通过。
- [ ] 性能接近发布目标。
- [ ] 内存接近发布目标。
- [ ] 网络稳定性达标。
- [ ] 发布工具链可用。
- [ ] 已知问题有明确处理策略。
- [ ] 无未接受 Critical 缺陷。

---

## 29. M7 Release Candidate

### 29.1 目标

生成可以发布的候选版本。

### 29.2 RC 规则

RC 阶段只允许：

- 发布阻塞修复。
- 必要兼容修复。
- 文档。
- 配置。
- 构建修复。

禁止：

- 新功能。
- 无关重构。
- 高风险依赖升级。
- 大规模资源替换。

### 29.3 退出条件

- [ ] Release Branch 稳定。
- [ ] 回归测试通过。
- [ ] 性能达标。
- [ ] 内存达标。
- [ ] 网络达标。
- [ ] 安全检查完成。
- [ ] 安装和升级通过。
- [ ] 回滚验证通过。
- [ ] 发布文档完整。
- [ ] Release Review 通过。

---

## 30. M8 Launch

### 30.1 目标

正式发布版本，并完成上线监控。

### 30.2 退出条件

- [ ] 正式 Build 已签名。
- [ ] 正式资源已发布。
- [ ] 正式配置已发布。
- [ ] 服务端版本已确认。
- [ ] Tag 已创建。
- [ ] ChangeLog 已发布。
- [ ] 发布监控可用。
- [ ] Incident 联系方式明确。
- [ ] Hotfix 流程准备完成。
- [ ] 上线后关键指标稳定。

---

## 31. M9 Live Operations

### 31.1 目标

持续维护线上版本并稳定迭代。

### 31.2 范围

- 内容更新。
- 活动。
- 平衡调整。
- Bug 修复。
- 性能维护。
- 安全更新。
- 版本兼容。
- 数据分析。
- 用户反馈。
- 线上事故处理。

### 31.3 质量要求

- 更新可回滚。
- 配置可验证。
- 资源可校验。
- 协议兼容。
- 存档兼容。
- 运营风险可控。
- 线上监控可用。

---

## 32. 技术里程碑

除项目阶段外，核心技术模块可以建立独立里程碑。

例如：

```text
T0 Framework Foundation
T1 Resource System
T2 Config System
T3 Networking Foundation
T4 Character Controller
T5 Combat Foundation
T6 Save System
T7 Build Pipeline
T8 Performance Baseline
```

---

## 33. 技术里程碑完成标准

每个技术里程碑至少包含：

- Architecture。
- Design。
- 核心接口。
- 实现。
- 单元测试。
- 集成测试。
- 错误处理。
- 性能评估。
- 文档。
- 示例或验证场景。

---

## 34. 当前建议里程碑顺序

结合 Project Aether 当前阶段，建议顺序：

```text
PA-M0 Project Standard
PA-M1 Project Structure
PA-M2 Core Framework
PA-M3 Resource Foundation
PA-M4 Config Foundation
PA-M5 Pool Foundation
PA-M6 Logging and Diagnostics
PA-M7 Test Infrastructure
PA-M8 Networking Foundation
PA-M9 Character Prototype
PA-M10 Combat Prototype
PA-M11 Vertical Slice
```

---

## 35. PA-M0 Project Standard

### 目标

建立项目统一规范。

### 交付物

- `00_ProjectStandard_Index.md`
- `01_DirectoryStandard.md`
- `02_DocumentStandard.md`
- `03_GitStandard.md`
- `04_CodingStandard.md`
- `05_ReviewStandard.md`
- `06_TestStandard.md`
- `07_AIStandard.md`
- `08_ProjectWorkflow.md`
- `09_ProjectMilestone.md`

### 退出条件

- [ ] 所有文档已生成。
- [ ] 文件名与编号一致。
- [ ] 文档格式一致。
- [ ] 文档已放入正确目录。
- [ ] 索引已更新。
- [ ] 文档通过 Review。
- [ ] 状态从 Draft 更新为 Approved。

---

## 36. PA-M1 Project Structure

### 目标

根据目录规范建立 Unity 项目结构和 asmdef 基线。

### 退出条件

- [ ] Runtime、Editor、Tests 分离。
- [ ] Framework、Gameplay、Presentation 分层。
- [ ] asmdef 依赖方向正确。
- [ ] 无循环依赖。
- [ ] 基础 Namespace 正确。
- [ ] 项目可编译。
- [ ] 目录文档与实际工程一致。

---

## 37. PA-M2 Core Framework

### 目标

完成 Bootstrap、ModuleManager 和核心生命周期。

### 退出条件

- [ ] `BootstrapRunner` 可启动。
- [ ] `Bootstrap.Initialize()` 可执行。
- [ ] `ModuleManager.Register()` 可注册模块。
- [ ] `Create()` 顺序正确。
- [ ] `InitializeAll()` 顺序正确。
- [ ] `UpdateAll()` 可执行。
- [ ] `ShutdownAll()` 顺序正确。
- [ ] 重复初始化有保护。
- [ ] 自动化测试通过。
- [ ] 架构文档完成。

---

## 38. PA-M3 Resource Foundation

### 目标

完成基础资源加载、Handle 生命周期、缓存和释放闭环。

### 退出条件

- [ ] `ResourceManager` 可加载资源。
- [ ] `ResourceHandle<T>` 状态完整。
- [ ] `SetLoaded` 和 `SetFailed` 行为正确。
- [ ] Retain / Release 行为明确。
- [ ] 重复释放有保护。
- [ ] Cache 行为可验证。
- [ ] Pending Release 行为可验证。
- [ ] Addressables 适配边界明确。
- [ ] 自动化测试通过。
- [ ] 内存释放验证通过。
- [ ] 文档与实现一致。

---

## 39. PA-M4 Config Foundation

### 目标

完成配置导入、类型安全访问和基础校验。

### 退出条件

- [ ] 配置可导入。
- [ ] `IConfigRow` 规则明确。
- [ ] `ConfigManager` 可加载。
- [ ] 重复 Key 被阻止。
- [ ] 引用错误可定位。
- [ ] Runtime 配置只读。
- [ ] Resource Module 接入明确。
- [ ] 自动化测试通过。
- [ ] 导表文档完成。

---

## 40. PA-M5 Pool Foundation

### 目标

完成对象池的创建、获取、归还、清理和容量管理。

### 退出条件

- [ ] Get 可用。
- [ ] Release 可用。
- [ ] 重复归还有保护。
- [ ] Reset 策略明确。
- [ ] Clear 可用。
- [ ] Shutdown 可用。
- [ ] 容量策略明确。
- [ ] 自动化测试通过。
- [ ] 无明显泄漏。

---

## 41. PA-M6 Logging and Diagnostics

### 目标

建立统一日志、错误上下文和运行时诊断能力。

### 退出条件

- [ ] 日志级别统一。
- [ ] 模块前缀统一。
- [ ] Release 日志可过滤。
- [ ] 异常保留上下文。
- [ ] 核心模块有诊断信息。
- [ ] 敏感信息不会输出。
- [ ] Debug Overlay 或统计入口可用。

---

## 42. PA-M7 Test Infrastructure

### 目标

建立 EditMode、PlayMode、集成和性能测试基础设施。

### 退出条件

- [ ] 测试 asmdef 完成。
- [ ] CI 可执行基础测试。
- [ ] 核心模块测试可运行。
- [ ] 测试报告可保存。
- [ ] 性能基线可记录。
- [ ] Flaky Test 管理规则可执行。

---

## 43. PA-M8 Networking Foundation

### 目标

建立 FishNet 连接、房间、实体生成和基础状态同步。

### 退出条件

- [ ] Server 可启动。
- [ ] Client 可连接。
- [ ] Host 模式可运行。
- [ ] 多客户端可进入。
- [ ] Ownership 明确。
- [ ] 权威规则明确。
- [ ] 基础 RPC 可验证。
- [ ] 断线行为明确。
- [ ] 网络测试完成。
- [ ] 网络架构文档完成。

---

## 44. PA-M9 Character Prototype

### 目标

完成可联网的基础角色控制和表现。

### 退出条件

- [ ] 角色可生成。
- [ ] 本地输入可用。
- [ ] 移动可用。
- [ ] 镜头可用。
- [ ] 动画基础状态可用。
- [ ] 网络同步可用。
- [ ] Ownership 正确。
- [ ] 销毁和重连流程可用。

---

## 45. PA-M10 Combat Prototype

### 目标

完成基础攻击、受击、伤害和死亡闭环。

### 退出条件

- [ ] 攻击输入可用。
- [ ] 命中检测可用。
- [ ] 伤害计算可用。
- [ ] 受击可用。
- [ ] 死亡可用。
- [ ] 服务端权威明确。
- [ ] 状态同步可用。
- [ ] 回归测试可用。
- [ ] 性能满足原型目标。

---

## 46. PA-M11 Vertical Slice

### 目标

形成 Project Aether 第一个完整可玩切片。

### 退出条件

- [ ] 登录或进入流程完整。
- [ ] 房间或场景流程完整。
- [ ] 角色流程完整。
- [ ] 战斗流程完整。
- [ ] 敌人流程完整。
- [ ] UI 流程完整。
- [ ] 结算流程完整。
- [ ] 网络流程完整。
- [ ] Build 可安装。
- [ ] 性能达到阶段目标。
- [ ] 主要风险已重新评估。

---

## 47. 里程碑范围管理

里程碑开始后，新增需求必须分类：

### Must Have

没有该项，里程碑无法实现目标。

### Should Have

重要，但可以在必要时延后。

### Could Have

有价值，但不影响阶段目标。

### Won't Have

本里程碑明确不做。

---

## 48. Scope Change 流程

```text
Change Request
  ↓
Impact Analysis
  ↓
Risk Analysis
  ↓
Schedule Analysis
  ↓
Decision
  ↓
Update Milestone
```

必须更新：

- Scope。
- Exit Criteria。
- 风险。
- 时间。
- 依赖。
- 负责人。

---

## 49. 里程碑计划拆分

里程碑应拆分为：

```text
Workstream
  ↓
Epic
  ↓
Feature / Story
  ↓
Task
```

---

## 50. Workstream

常见 Workstream：

- Framework。
- Resource。
- Config。
- Networking。
- Gameplay。
- Combat。
- Character。
- UI。
- Tools。
- Build。
- QA。
- Content。
- Documentation。

---

## 51. 里程碑任务要求

每个任务必须关联：

- Milestone。
- Workstream。
- Owner。
- Priority。
- Dependency。
- Acceptance Criteria。
- Status。
- Target Date。

---

## 52. 关键路径

必须识别阻止里程碑完成的关键路径。

示例：

```text
Framework
  ↓
Resource
  ↓
Config
  ↓
Character
  ↓
Combat
  ↓
Vertical Slice
```

关键路径任务的风险必须优先处理。

---

## 53. 并行工作

可并行任务应满足：

- 接口稳定。
- 文件冲突可控。
- 依赖明确。
- 集成顺序明确。
- 验收独立。

---

## 54. 依赖冻结

里程碑进入后期时，应冻结：

- 公共 API。
- 配置 Schema。
- 网络协议。
- 存档格式。
- Package 版本。
- Build 参数。

必要修改必须经过变更评审。

---

## 55. 里程碑追踪

推荐每周更新：

- 状态。
- 已完成交付物。
- 剩余交付物。
- 风险。
- 阻塞。
- 范围变化。
- 质量状态。
- 预测完成日期。

---

## 56. 里程碑状态报告模板

```markdown
# Milestone Status

## Status

On Track / At Risk / Blocked

## Completed

## In Progress

## Next

## Risks

## Blockers

## Scope Changes

## Quality

## Forecast
```

---

## 57. 健康状态

### On Track

范围、时间和质量可达到目标。

### At Risk

存在风险，但有明确缓解方案。

### Blocked

关键依赖阻止推进。

### Off Track

当前计划无法达到目标，需要重新规划。

---

## 58. 里程碑进度

推荐以已完成退出条件或交付物衡量。

例如：

```text
8 / 10 Deliverables Completed
6 / 8 Exit Criteria Verified
```

不推荐只写：

```text
Progress: 80%
```

---

## 59. 质量门禁

每个里程碑至少经过：

```text
Scope Review
  ↓
Architecture / Design Review
  ↓
Implementation Review
  ↓
Test Review
  ↓
Milestone Exit Review
```

---

## 60. Milestone Exit Review

完成里程碑前必须召开或执行正式退出评审。

评审内容：

- Objective 是否实现。
- Scope 是否交付。
- Exit Criteria 是否满足。
- 测试是否完成。
- 性能是否达标。
- 内存是否达标。
- 文档是否同步。
- 风险是否关闭。
- 技术债是否记录。
- 下一里程碑是否 Ready。

---

## 61. Exit Review 结论

统一使用：

- Approved。
- Approved with Follow-up。
- Changes Required。
- Deferred。
- Cancelled。

---

## 62. Approved with Follow-up

允许存在非阻塞遗留项，但必须记录：

- 问题。
- Owner。
- Milestone。
- 截止时间。
- 风险。
- 验收方式。

---

## 63. 不允许关闭的情况

以下情况不得关闭里程碑：

- 核心目标未实现。
- 关键退出条件未验证。
- 存在未接受 Blocker。
- 代码无法编译。
- 关键测试未执行。
- 关键文档缺失。
- 重大风险未记录。
- 下一阶段依赖当前结果但当前结果不稳定。

---

## 64. 里程碑关闭

关闭后必须：

- 更新状态。
- 记录实际结束时间。
- 保存评审结论。
- 记录遗留问题。
- 更新索引。
- 更新下一里程碑入口条件。
- 创建必要后续任务。
- 归档测试和性能数据。

---

## 65. 里程碑复盘

每个重要里程碑关闭后应复盘：

- 什么完成得好。
- 什么未完成。
- 范围是否合理。
- 估算是否准确。
- 风险是否提前发现。
- 哪些依赖造成阻塞。
- 测试是否足够。
- 文档是否有效。
- AI 使用是否有效。
- 下一阶段如何改进。

---

## 66. 延期处理

预计延期时不得等到最后一天才说明。

必须尽早提供：

- 原计划。
- 当前状态。
- 延期原因。
- 剩余工作。
- 风险。
- 新预测。
- 可削减范围。
- 需要的支持。

---

## 67. 范围削减

延期时优先评估：

- 移除 Could Have。
- 延后 Should Have。
- 保留 Must Have。
- 降低非核心内容数量。
- 使用临时但安全方案。
- 分阶段交付。

禁止通过跳过测试、Review 或文档掩盖延期。

---

## 68. 缓冲

高风险里程碑应包含合理缓冲，用于：

- 集成。
- Bug 修复。
- 性能。
- 构建。
- 平台问题。
- 外部依赖延迟。

缓冲不是可自由增加功能的时间。

---

## 69. 估算

估算应考虑：

- 设计。
- 实现。
- 测试。
- Review。
- 文档。
- 集成。
- 修复。
- 发布。
- 沟通。
- 风险。

禁止只估算编码时间。

---

## 70. 估算更新

获得新信息后应更新估算。

估算变化不是失败，隐瞒变化才会增加项目风险。

---

## 71. 里程碑与版本

里程碑不一定等于发布版本。

一个版本可以包含多个里程碑。

一个大型里程碑也可以跨多个内部版本。

---

## 72. 版本编号

正式版本遵循：

```text
vMajor.Minor.Patch
```

内部里程碑可以使用：

```text
PA-M0
PA-M1
PA-M2
```

Release Candidate：

```text
v1.0.0-rc.1
v1.0.0-rc.2
```

---

## 73. Feature Freeze

进入 Beta 或 RC 前必须定义 Feature Freeze。

Feature Freeze 后禁止新增核心功能。

---

## 74. Code Freeze

正式发布前可以进入 Code Freeze。

Code Freeze 后只允许：

- Blocker 修复。
- Critical 修复。
- 必要构建修复。
- 经批准的发布配置修改。

---

## 75. Content Freeze

内容发布前应冻结：

- 关卡。
- 角色。
- 配置。
- 本地化。
- 资源。
- 商店内容。

冻结后修改必须重新验证。

---

## 76. 发布里程碑依赖

正式发布至少依赖：

- Feature Complete。
- Content Complete。
- QA Sign-off。
- Performance Sign-off。
- Build Sign-off。
- Security Sign-off。
- Operations Readiness。
- Rollback Readiness。

---

## 77. 运营准备

Launch 前必须准备：

- 监控。
- 告警。
- 客服流程。
- Incident 流程。
- Hotfix 流程。
- 回滚。
- 版本兼容。
- 数据备份。
- 发布沟通。

---

## 78. 技术债门禁

每个里程碑结束时必须检查技术债。

技术债必须分类：

- 可接受。
- 下一里程碑处理。
- 发布前必须处理。
- 长期跟踪。
- 立即阻塞。

---

## 79. 文档门禁

里程碑关闭前确认：

- Architecture 更新。
- Design 更新。
- RFC 关闭。
- Decision Log 更新。
- Test 记录保存。
- Review 记录保存。
- AI Handoff 更新。
- 索引更新。

---

## 80. AI 在里程碑中的作用

AI 可以辅助：

- 拆分工作。
- 识别风险。
- 草拟计划。
- 生成检查清单。
- 分析进度。
- 编写状态报告。
- 整理复盘。
- 检查文档一致性。

AI 不得独立：

- 宣布里程碑完成。
- 接受风险。
- 修改范围。
- 修改目标日期。
- 批准发布。

---

## 81. AI 里程碑上下文

向 AI 提供：

- Milestone Objective。
- Scope。
- Exit Criteria。
- Current Status。
- Completed Deliverables。
- Risks。
- Blockers。
- Dependencies。
- Current Commit。
- Related Documents。

---

## 82. 里程碑自动化

可自动化：

- 测试结果。
- Build 状态。
- PR 状态。
- 缺陷数量。
- 性能基线。
- 文档链接。
- Exit Criteria 检查。
- 发布产物校验。

自动化不能替代最终退出评审。

---

## 83. 里程碑指标

可跟踪：

- Deliverable 完成率。
- Exit Criteria 通过率。
- Blocker 数量。
- Bug 数量。
- 测试通过率。
- 构建成功率。
- 性能变化。
- 内存变化。
- Scope Change 数量。
- Reopen 数量。
- 预测偏差。

---

## 84. 指标使用原则

指标用于改进项目，不用于简单评价个人。

必须结合上下文解释。

---

## 85. 里程碑仪表板

推荐展示：

```text
Status
Objective
Target Date
Deliverables
Exit Criteria
Risks
Blockers
Quality
Forecast
```

---

## 86. Milestone 文档命名

推荐：

```text
PA-M0_ProjectStandard.md
PA-M1_ProjectStructure.md
PA-M2_CoreFramework.md
```

存放路径可以为：

```text
Docs/02_Design/ProjectPlan/
```

或单独建立项目计划目录，但必须同步更新目录规范和索引。

---

## 87. Milestone 模板

```markdown
# PA-MX Milestone Name

> **Milestone ID:** PA-MX  
> **Status:** Planned  
> **Owner:**  
> **Planned Start:**  
> **Planned End:**  
> **Actual Start:**  
> **Actual End:**  

## 1. Objective

## 2. Value

## 3. Entry Criteria

## 4. Scope

### Included

### Excluded

## 5. Deliverables

| ID | Deliverable | Owner | Status |
|---|---|---|---|

## 6. Dependencies

## 7. Risks

| Risk | Probability | Impact | Mitigation | Owner |
|---|---|---|---|---|

## 8. Quality Targets

## 9. Exit Criteria

- [ ]

## 10. Validation Plan

## 11. Rollback / Contingency

## 12. Status Updates

## 13. Exit Review

## 14. Follow-up
```

---

## 88. 风险记录模板

```markdown
| ID | Risk | Probability | Impact | Level | Mitigation | Owner | Status |
|---|---|---|---|---|---|---|---|
```

---

## 89. 依赖记录模板

```markdown
| ID | Dependency | Provider | Needed By | Acceptance | Fallback | Status |
|---|---|---|---|---|---|---|
```

---

## 90. 交付物记录模板

```markdown
| ID | Deliverable | Definition | Owner | Due Date | Status | Evidence |
|---|---|---|---|---|---|---|
```

---

## 91. Exit Criteria 记录模板

```markdown
| ID | Criterion | Validation | Owner | Status | Evidence |
|---|---|---|---|---|---|
```

---

## 92. 里程碑 Review Checklist

### Planning

- [ ] Objective 明确。
- [ ] Value 明确。
- [ ] Entry Criteria 明确。
- [ ] Scope 明确。
- [ ] Non-Scope 明确。
- [ ] Deliverables 明确。
- [ ] Owner 明确。
- [ ] 日期明确。

### Dependencies

- [ ] 依赖已识别。
- [ ] 提供方明确。
- [ ] 验收方式明确。
- [ ] 替代方案明确。
- [ ] 关键路径明确。

### Risks

- [ ] 技术风险已评估。
- [ ] 进度风险已评估。
- [ ] 性能风险已评估。
- [ ] 内存风险已评估。
- [ ] 网络风险已评估。
- [ ] 发布风险已评估。
- [ ] 缓解措施明确。

### Quality

- [ ] 测试目标明确。
- [ ] 性能目标明确。
- [ ] 内存目标明确。
- [ ] 稳定性目标明确。
- [ ] 构建目标明确。
- [ ] 文档目标明确。

### Exit

- [ ] Exit Criteria 可验证。
- [ ] Validation Plan 完整。
- [ ] 回滚方案明确。
- [ ] Exit Review 已安排。
- [ ] Follow-up 规则明确。

---

## 93. Milestone Exit Checklist

- [ ] Objective 已实现。
- [ ] Scope 已交付。
- [ ] Deliverables 完整。
- [ ] Exit Criteria 全部验证。
- [ ] Blocker 已关闭。
- [ ] Major 已处理。
- [ ] 测试通过。
- [ ] 性能达标。
- [ ] 内存达标。
- [ ] 网络达标。
- [ ] Build 通过。
- [ ] 文档同步。
- [ ] 风险已更新。
- [ ] 技术债已记录。
- [ ] 下一里程碑 Ready。
- [ ] Exit Review 已通过。

---

## 94. 常见错误

### 94.1 用百分比代替退出标准

导致无法判断真实完成状态。

### 94.2 范围持续扩大

导致时间和质量失控。

### 94.3 只计划功能

遗漏测试、文档、集成和发布。

### 94.4 高风险任务放到最后

导致阶段末期无法解决。

### 94.5 里程碑未完成就进入下一阶段

导致技术债和不稳定性叠加。

### 94.6 将原型当正式架构

导致后续维护困难。

### 94.7 只关闭任务，不验证目标

任务完成不代表阶段目标实现。

### 94.8 为赶时间跳过质量门禁

导致风险转移到后续版本或线上。

---

## 95. 当前 Project Standard 完成后的下一步

`PA-M0 Project Standard` 完成后，推荐执行：

```text
Review all Project Standard documents
  ↓
Resolve inconsistencies
  ↓
Update 00_ProjectStandard_Index.md
  ↓
Change status from Draft to Approved
  ↓
Commit documents
  ↓
Start PA-M1 Project Structure
```

---

## 96. Project Standard 批准条件

当前规范文档包批准前必须确认：

- [ ] 目录结构一致。
- [ ] 文档编号一致。
- [ ] 文件名一致。
- [ ] Git Commit 格式一致。
- [ ] Coding Standard 与实际技术栈一致。
- [ ] Review 和 Test 门禁一致。
- [ ] AI 规则可执行。
- [ ] Workflow 和 Milestone 没有冲突。
- [ ] 索引完整。
- [ ] 所有链接有效。
- [ ] 状态统一。
- [ ] Change Log 完整。

---

## 97. 建议 Git Commit

完成本文件后可使用：

```text
[Docs][Add] Add project milestone standard
```

完成整个 Project Standard 文档包评审后可使用：

```text
[Docs][Update] Finalize Project Aether project standards
```

---

## 98. 验收标准

本规范执行后，应达到：

- Project Aether 每个阶段拥有明确目标。
- 每个阶段拥有进入和退出条件。
- 进度基于可验证交付物。
- 范围变化可以被控制和追踪。
- 风险和依赖进入正式计划。
- 架构、实现、测试、文档和发布同步推进。
- 原型、Vertical Slice、Alpha、Beta 和 Release 的定义清晰。
- 核心技术模块可以独立建立里程碑。
- 里程碑延期和范围削减有明确流程。
- AI 可以辅助计划，但不能替代人工批准。
- 项目能够从当前规范建设阶段平稳进入核心框架开发阶段。

---

## 99. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-08-07 | 创建 Project Aether 项目里程碑规范正式初稿 |

---

# End
