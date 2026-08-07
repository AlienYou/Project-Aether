# Project Aether 项目工作流

> **文件名：** `08_ProjectWorkflow.md`  
> **文档编号：** PAS-008  
> **版本：** v1.0  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **Unity 版本：** Unity 2022.3.51f1c1  
> **最后更新：** 2026-08-06  

---

## 1. 文档目的

本文档定义 Project Aether 从需求提出、方案设计、任务拆分、开发实现、评审、测试、合入、构建、发布到复盘的统一项目工作流。

本规范的目标是：

- 让每项工作都有明确入口和完成条件。
- 保证需求、设计、实现、测试和文档能够串联。
- 降低跨角色协作中的信息损耗。
- 避免直接进入编码导致的返工。
- 保证高风险修改经过适当评审和验证。
- 让项目进度可以追踪、暂停、恢复和交接。
- 为 AI 参与开发建立明确工作边界。
- 为长期商业项目建立稳定、可重复执行的研发流程。

---

## 2. 适用范围

本工作流适用于：

- 新功能开发。
- Bug 修复。
- 技术重构。
- 架构调整。
- 性能优化。
- 资源更新。
- 配置更新。
- 工具开发。
- 网络功能。
- 构建系统修改。
- 发布准备。
- 热修复。
- 文档建设。
- AI 协作任务。

---

## 3. 核心原则

### 3.1 No Task Without Context

任何任务开始前都必须明确：

- 为什么做。
- 做什么。
- 不做什么。
- 影响哪些模块。
- 如何验收。
- 有哪些限制。
- 谁负责。
- 何时完成。

---

### 3.2 Design Before Implementation

高风险或跨模块任务必须先完成设计。

不允许直接通过代码试探架构。

---

### 3.3 Small, Reviewable Changes

任务应拆分为可独立理解、验证和回滚的小步修改。

---

### 3.4 Documentation Is Part of Delivery

文档不是可选附件。

以下变化必须同步文档：

- 模块职责。
- 生命周期。
- 公共接口。
- 配置格式。
- 资源策略。
- 网络协议。
- 发布流程。
- 测试规则。

---

### 3.5 Definition of Done Is Explicit

“代码写完”不等于任务完成。

任务完成必须满足既定 Definition of Done。

---

### 3.6 Risk Determines Process

低风险任务可以使用轻量流程。

高风险任务必须使用完整流程。

---

### 3.7 One Current Baseline

所有参与者必须基于同一分支、Commit、文档和资源版本工作。

---

### 3.8 Human Owns Final Decisions

AI 可以辅助分析、实现和检查。

关键架构、风险接受、合入和发布决策必须由人负责。

---

## 4. 工作项类型

Project Aether 使用以下工作项：

| 类型 | 用途 |
|---|---|
| Epic | 大型目标或阶段性能力 |
| Feature | 用户可感知的新功能 |
| Story | 可交付的具体需求 |
| Task | 明确的开发工作 |
| Bug | 缺陷修复 |
| Refactor | 不改变预期行为的重构 |
| RFC | 重大技术变更提案 |
| Spike | 技术验证或调研 |
| Chore | 工程维护 |
| Docs | 文档工作 |
| Test | 测试专项 |
| Hotfix | 已发布版本紧急修复 |

---

## 5. 工作项状态

统一状态：

```text
Backlog
Ready
In Progress
In Review
In Test
Blocked
Done
Cancelled
```

对于发布相关任务，可增加：

```text
Ready for Release
Released
```

---

## 6. 状态定义

### 6.1 Backlog

任务已记录，但尚未准备开发。

可能缺少：

- 优先级。
- 需求细节。
- 设计。
- 依赖。
- 验收标准。

---

### 6.2 Ready

任务已经具备开发条件。

必须满足：

- 目标明确。
- 范围明确。
- 验收明确。
- 依赖明确。
- 风险初步评估。
- 负责人明确。

---

### 6.3 In Progress

任务正在实现。

要求：

- 已创建正确分支。
- 已确认基线。
- 已开始实际修改。
- 进度可追踪。

---

### 6.4 In Review

实现已完成，正在评审。

要求：

- 自检完成。
- 编译通过。
- 测试完成。
- 文档同步。
- PR 已创建。

---

### 6.5 In Test

代码评审已通过，正在执行测试或验收。

---

### 6.6 Blocked

任务因外部条件无法继续。

必须记录：

- 阻塞原因。
- 阻塞时间。
- 依赖对象。
- 负责人。
- 解除条件。
- 临时方案。

---

### 6.7 Done

任务已满足 Definition of Done。

---

### 6.8 Cancelled

任务已明确取消。

必须记录取消原因。

---

## 7. 标准功能开发流程

```text
Requirement
  ↓
Clarification
  ↓
Risk Assessment
  ↓
Design
  ↓
Task Breakdown
  ↓
Implementation
  ↓
Self Review
  ↓
Code Review
  ↓
Test
  ↓
Merge
  ↓
Post-Merge Verification
  ↓
Done
```

---

## 8. 需求入口

需求必须至少包含：

- 背景。
- 目标。
- 用户价值。
- 期望行为。
- 非目标。
- 验收标准。
- 优先级。
- 目标版本。
- 依赖。
- 风险。

---

## 9. 需求澄清

需求澄清重点：

- 谁使用。
- 在什么场景使用。
- 当前行为是什么。
- 期望行为是什么。
- 异常情况如何处理。
- 是否涉及网络。
- 是否涉及存档。
- 是否涉及热更新。
- 是否涉及性能。
- 是否涉及多平台。
- 是否有美术、策划或服务端依赖。

---

## 10. 非目标

每个中大型任务应明确 Non-Goals。

示例：

```text
本次实现只完成资源句柄生命周期，
不包含远程资源下载策略，
不包含资源加密，
不包含资源版本回滚。
```

---

## 11. 验收标准

验收标准必须：

- 可观察。
- 可验证。
- 不依赖主观判断。
- 覆盖正常路径。
- 覆盖关键异常路径。

示例：

```text
当相同 AssetKey 被同时请求两次时，
底层资源只发起一次加载，
两个调用方都收到 Loaded 状态的 Handle。
```

---

## 12. 风险评估

任务开始前评估：

- 架构风险。
- 数据风险。
- 网络风险。
- 性能风险。
- 内存风险。
- 兼容性风险。
- 发布风险。
- 安全风险。
- 进度风险。
- 第三方依赖风险。

---

## 13. 风险等级

### Low

- 模块内部小修改。
- 无公共接口变化。
- 无数据兼容影响。
- 容易测试和回滚。

### Medium

- 影响多个类。
- 影响模块内部生命周期。
- 需要集成测试。
- 有一定性能或兼容风险。

### High

- 跨模块。
- 公共接口变化。
- 网络协议变化。
- 存档变化。
- 核心资源生命周期变化。
- 构建或发布流程变化。

### Critical

- 生产数据。
- 安全。
- 支付。
- 账号。
- 大规模迁移。
- 无法快速回滚的发布。

---

## 14. 流程选择

### Low Risk

```text
Task
  ↓
Implementation
  ↓
Self Review
  ↓
Lightweight Review
  ↓
Test
  ↓
Merge
```

### Medium Risk

```text
Requirement
  ↓
Design
  ↓
Implementation
  ↓
Standard Review
  ↓
Integration Test
  ↓
Merge
```

### High / Critical Risk

```text
Requirement
  ↓
RFC
  ↓
Architecture Review
  ↓
Design Review
  ↓
Implementation Plan
  ↓
Staged Implementation
  ↓
Critical Review
  ↓
Full Test
  ↓
Release Plan
  ↓
Controlled Merge / Release
```

---

## 15. Design 入口条件

以下情况必须有 Design：

- 新增模块。
- 修改模块职责。
- 新增状态机。
- 修改核心生命周期。
- 新增复杂异步流程。
- 新增资源缓存策略。
- 新增配置流水线。
- 新增网络同步逻辑。
- 修改战斗核心规则。
- 修改存档结构。
- 修改构建流程。

---

## 16. RFC 入口条件

以下情况通常必须创建 RFC：

- 跨模块架构变化。
- 公共 API 破坏性变化。
- 技术栈替换。
- 第三方核心库引入。
- 网络协议变化。
- 存档迁移。
- 热更新策略变化。
- 资源生命周期变化。
- 初始化框架变化。
- 安全策略变化。

---

## 17. Spike 工作流

当技术方案不确定时，创建 Spike。

Spike 必须明确：

- 要验证的问题。
- 时间盒。
- 验证方式。
- 输出。
- 成功条件。
- 失败条件。

Spike 输出可以是：

- 结论。
- 原型。
- 性能数据。
- 风险列表。
- 推荐方案。
- 放弃原因。

Spike 代码默认不能直接进入正式工程。

---

## 18. 任务拆分

任务拆分应满足：

- 单一目标。
- 可独立理解。
- 可独立验证。
- 可独立回滚。
- 依赖明确。
- 负责人明确。
- 验收明确。

---

## 19. 推荐拆分顺序

大型功能推荐按以下顺序：

```text
Architecture
  ↓
Core Interfaces
  ↓
Data Structures
  ↓
Core Logic
  ↓
Unity Integration
  ↓
Network Integration
  ↓
UI / Presentation
  ↓
Tests
  ↓
Documentation
  ↓
Optimization
```

---

## 20. 任务过大信号

出现以下情况应继续拆分：

- 预计修改多个独立模块。
- 同时修改大量公共接口。
- PR 难以一次理解。
- 同时包含重构和功能。
- 无法明确单一验收标准。
- 无法在合理时间内完成 Review。
- 回滚会影响大量无关内容。

---

## 21. 任务模板

```markdown
# Task Title

## Background

## Goal

## Non-Goals

## Scope

## Constraints

## Dependencies

## Risks

## Acceptance Criteria

## Test Plan

## Related Documents

## Owner

## Target Milestone
```

---

## 22. 开发前检查

开始编码前确认：

- [ ] 任务状态为 Ready。
- [ ] 需求明确。
- [ ] 验收标准明确。
- [ ] 风险等级明确。
- [ ] 相关 Design 或 RFC 已批准。
- [ ] 当前分支和 Commit 已确认。
- [ ] 相关源码已读取。
- [ ] 相关测试已读取。
- [ ] 禁止修改事项明确。
- [ ] 依赖已准备。

---

## 23. 分支创建

按照 `03_GitStandard.md` 创建分支。

示例：

```bash
git switch develop
git pull --ff-only
git switch -c feature/resource-manager
```

---

## 24. 开发节奏

推荐小步循环：

```text
Implement Small Change
  ↓
Compile
  ↓
Run Focused Test
  ↓
Inspect Diff
  ↓
Commit
```

避免长时间不编译、不测试、不提交。

---

## 25. 实现顺序

推荐顺序：

1. 数据结构。
2. 接口。
3. 核心逻辑。
4. 错误处理。
5. 生命周期。
6. Unity 集成。
7. 第三方库适配。
8. 测试。
9. 文档。
10. 性能优化。

---

## 26. 编译检查

每个可独立阶段至少执行一次编译。

必须处理：

- 编译错误。
- 新增警告。
- asmdef 引用错误。
- Editor / Runtime 引用错误。
- 平台条件编译错误。

---

## 27. 测试节奏

开发过程中分层执行：

```text
Focused Unit Test
  ↓
Module Test
  ↓
Integration Test
  ↓
PlayMode Test
```

不应等到功能全部完成才第一次测试。

---

## 28. 自检

作者自检包含：

- 需求对照。
- Diff 检查。
- 架构一致性。
- 生命周期。
- 错误路径。
- 资源释放。
- 异步取消。
- 性能风险。
- 测试。
- 文档。
- Git 状态。

---

## 29. Commit 节奏

推荐在以下节点提交：

- 接口完成。
- 核心逻辑完成。
- 测试完成。
- 文档完成。
- 独立 Bug 修复完成。

Commit 必须可理解。

---

## 30. PR 创建

PR 创建前必须：

- 编译通过。
- 相关测试通过。
- 自检完成。
- 文档同步。
- Commit 整理完成。
- PR 描述完整。
- 风险和回滚明确。

---

## 31. PR 评审流程

遵循 `05_ReviewStandard.md`。

```text
Create PR
  ↓
Assign Reviewers
  ↓
Review
  ↓
Resolve Findings
  ↓
Re-Review
  ↓
Approve
```

---

## 32. 测试阶段

代码评审完成后进入测试。

根据任务风险执行：

- Unit Test。
- Integration Test。
- PlayMode Test。
- Manual Test。
- Performance Test。
- Memory Test。
- Network Test。
- Build Test。

---

## 33. 测试失败

测试失败时：

1. 停止合入。
2. 保存日志。
3. 确认版本。
4. 判断根因。
5. 修复。
6. 增加回归测试。
7. 重新评审。
8. 重新测试。

---

## 34. 合入条件

必须满足：

- 所有 Blocker 已解决。
- Major 已解决或明确接受风险。
- 必要 Review 已批准。
- 测试通过。
- 文档同步。
- 无冲突。
- Commit 合规。
- 合并策略正确。

---

## 35. 合并策略

根据 `03_GitStandard.md` 选择：

- Squash Merge。
- Merge Commit。
- Rebase 后合并。

合并策略必须保留清晰历史。

---

## 36. 合入后验证

合入 `develop` 后至少执行：

- Compile。
- 自动化测试。
- 冒烟测试。
- 关键场景验证。
- 资源加载验证。
- 配置加载验证。
- 网络连接验证。

---

## 37. Definition of Ready

任务进入 Ready 前必须满足：

- [ ] 背景明确。
- [ ] 目标明确。
- [ ] Non-Goals 明确。
- [ ] 范围明确。
- [ ] 验收标准明确。
- [ ] 风险已评估。
- [ ] 依赖已识别。
- [ ] 负责人明确。
- [ ] 目标版本明确。
- [ ] 相关文档已准备。
- [ ] 高风险设计已评审。

---

## 38. Definition of Done

任务完成必须满足：

- [ ] 需求已实现。
- [ ] 代码符合 Coding Standard。
- [ ] 编译通过。
- [ ] 测试通过。
- [ ] Code Review 通过。
- [ ] 文档同步。
- [ ] Git Commit 合规。
- [ ] 无未处理 Blocker。
- [ ] 性能影响已确认。
- [ ] 内存影响已确认。
- [ ] 网络影响已确认。
- [ ] 合入后验证通过。
- [ ] 验收标准全部满足。

---

## 39. Bug 工作流

```text
Report
  ↓
Triage
  ↓
Reproduce
  ↓
Root Cause
  ↓
Fix
  ↓
Regression Test
  ↓
Review
  ↓
Verify
  ↓
Close
```

---

## 40. Bug 报告

必须包含：

- 环境。
- 版本。
- 复现步骤。
- 期望结果。
- 实际结果。
- 频率。
- 日志。
- 截图或视频。
- 严重等级。
- 影响范围。

---

## 41. Bug Triage

Triage 确认：

- 是否可复现。
- 是否为重复问题。
- 严重等级。
- 优先级。
- 影响版本。
- 负责人。
- 是否阻止发布。
- 是否需要 Hotfix。

---

## 42. Root Cause

Bug 修复必须尽量找到根因。

禁止只通过：

- 增加延迟。
- 捕获所有异常。
- 重试无限次。
- 忽略状态。
- 放宽测试。
- 屏蔽日志。

来掩盖问题。

---

## 43. Bug 完成条件

- 根因明确。
- 修复最小。
- 回归测试已添加。
- 相关风险已检查。
- 代码评审通过。
- 测试验证通过。
- 文档必要时已更新。

---

## 44. Refactor 工作流

```text
Problem
  ↓
Behavior Baseline
  ↓
Tests
  ↓
Refactor Plan
  ↓
Small Changes
  ↓
Continuous Verification
  ↓
Review
```

---

## 45. Refactor 要求

重构前必须：

- 明确问题。
- 明确不改变的行为。
- 建立测试基线。
- 评估调用方。
- 评估性能。
- 评估序列化。
- 评估网络和存档。

---

## 46. 重构与功能分离

除非无法避免，重构和功能修改应拆分为不同 Commit 或 PR。

这样可以：

- 降低 Review 难度。
- 降低回滚风险。
- 保持行为变化清晰。
- 更容易定位回归。

---

## 47. 性能优化工作流

```text
Identify Symptom
  ↓
Measure Baseline
  ↓
Locate Bottleneck
  ↓
Set Target
  ↓
Implement
  ↓
Measure Again
  ↓
Regression Test
  ↓
Review
```

---

## 48. 性能优化完成条件

- 有基线数据。
- 有目标。
- 有修改后数据。
- 无功能回归。
- 无内存回退。
- 测试环境一致。
- 文档已记录。
- Review 已通过。

---

## 49. 文档工作流

```text
Identify Need
  ↓
Select Document Type
  ↓
Create Draft
  ↓
Review
  ↓
Approved
  ↓
Update Index
  ↓
Commit
```

---

## 50. 文档修改入口

以下情况应创建或更新文档：

- 新模块。
- 新架构。
- 新工作流。
- 新公共 API。
- 新测试策略。
- 新第三方库。
- 重大 Bug 根因。
- 发布流程变化。
- AI 协作规则变化。

---

## 51. AI 协作工作流

```text
Prepare Context
  ↓
Define Task
  ↓
Define Constraints
  ↓
AI Analysis / Generation
  ↓
Human Review
  ↓
Compile
  ↓
Test
  ↓
Code Review
  ↓
Commit
```

---

## 52. AI 任务入口

AI 任务必须明确：

- 当前分支。
- 当前 Commit。
- 当前模块。
- 当前源码。
- 相关文档。
- 允许修改范围。
- 禁止修改事项。
- 输出格式。
- 验证要求。

---

## 53. AI 输出处理

AI 输出不得直接视为完成。

必须：

- 阅读。
- 对比 Diff。
- 检查删除内容。
- 检查接口变化。
- 编译。
- 测试。
- 更新文档。
- 人工 Review。

---

## 54. 多人协作

多人协作前必须明确：

- 工作拆分。
- 文件所有权。
- 接口基线。
- 合并顺序。
- 依赖。
- 集成负责人。
- 测试负责人。

---

## 55. 同文件协作

尽量避免多人同时修改：

- 核心接口。
- 大型 Scene。
- 大型 Prefab。
- 配置 Schema。
- Package 配置。
- ProjectSettings。

无法避免时必须提前约定修改边界。

---

## 56. 跨团队依赖

跨客户端、服务端、策划、美术、测试任务必须记录：

- 输入。
- 输出。
- 格式。
- 版本。
- 时间。
- 负责人。
- 验收方式。
- 变更通知方式。

---

## 57. 每日同步

每日同步重点：

- 昨天完成。
- 今天计划。
- 阻塞。
- 风险。
- 需要决策。
- 依赖变化。

不应变成长篇技术汇报。

---

## 58. 周期评审

每个迭代周期至少检查：

- 目标完成情况。
- 未完成原因。
- 风险。
- Bug。
- 性能。
- 技术债。
- 文档状态。
- 下一周期依赖。

---

## 59. 里程碑工作流

```text
Define Milestone
  ↓
Define Scope
  ↓
Define Exit Criteria
  ↓
Plan Work
  ↓
Execute
  ↓
Track Risk
  ↓
Stabilize
  ↓
Review
  ↓
Close
```

具体里程碑规范见 `09_ProjectMilestone.md`。

---

## 60. Release 工作流

```text
Feature Freeze
  ↓
Create Release Branch
  ↓
Version Update
  ↓
Regression Test
  ↓
Performance / Memory Validation
  ↓
Fix Release Blockers
  ↓
Release Review
  ↓
Build
  ↓
Publish
  ↓
Post-Release Monitoring
```

---

## 61. Feature Freeze

Feature Freeze 后：

允许：

- Bug 修复。
- 发布阻塞修复。
- 测试。
- 文档。
- 构建修复。

禁止：

- 新大型功能。
- 无关重构。
- 高风险依赖升级。
- 非必要架构变化。

---

## 62. Release Branch

根据 `03_GitStandard.md` 创建：

```text
release/<version>
```

---

## 63. Release Candidate

每个 RC 必须记录：

- 版本。
- Commit。
- Build。
- 资源版本。
- 配置版本。
- 服务端版本。
- 测试结果。
- 已知问题。

---

## 64. Release Review

必须根据 `05_ReviewStandard.md` 执行。

---

## 65. 发布后监控

发布后关注：

- 崩溃率。
- 登录成功率。
- 网络错误。
- 资源下载失败。
- 配置错误。
- 性能。
- 内存。
- 用户反馈。
- 支付和存档。
- 服务端异常。

---

## 66. Hotfix 工作流

```text
Incident
  ↓
Assess Severity
  ↓
Create Hotfix Branch
  ↓
Minimal Fix
  ↓
Focused Test
  ↓
Review
  ↓
Release
  ↓
Merge Back
  ↓
Postmortem
```

---

## 67. Hotfix 原则

- 修复范围最小。
- 不夹带新功能。
- 不做大规模重构。
- 必须有回归测试。
- 必须有回滚方案。
- 必须同步 `main` 和 `develop`。

---

## 68. Incident 工作流

严重线上问题：

```text
Detect
  ↓
Contain
  ↓
Communicate
  ↓
Diagnose
  ↓
Mitigate
  ↓
Recover
  ↓
Postmortem
```

---

## 69. Incident 记录

必须包含：

- 发生时间。
- 发现方式。
- 影响范围。
- 当前状态。
- 临时措施。
- 根因。
- 永久修复。
- 负责人。
- 后续行动项。

---

## 70. Postmortem

复盘目标是改进系统，不是追责个人。

复盘内容：

- 发生了什么。
- 为什么发生。
- 为什么没有提前发现。
- 哪些措施有效。
- 哪些措施无效。
- 如何防止再次发生。
- 行动项和负责人。

---

## 71. 技术债工作流

技术债必须记录：

- 当前问题。
- 影响。
- 风险。
- 临时方案。
- 推荐方案。
- 优先级。
- 触发处理条件。
- 负责人。

---

## 72. 技术债分类

- Architecture Debt。
- Code Debt。
- Test Debt。
- Documentation Debt。
- Performance Debt。
- Build Debt。
- Tooling Debt。
- Dependency Debt。

---

## 73. 技术债处理

技术债不能只写“以后优化”。

必须进入 Backlog，并有：

- 明确范围。
- 风险。
- 优先级。
- 处理条件。
- 验收标准。

---

## 74. 变更控制

变更进入开发后，需求变化必须评估：

- 是否影响范围。
- 是否影响时间。
- 是否影响设计。
- 是否影响测试。
- 是否影响依赖。
- 是否需要重新评审。
- 是否需要拆分后续任务。

---

## 75. Scope Creep

发现范围不断扩大时：

1. 停止继续扩展。
2. 列出原范围。
3. 列出新增范围。
4. 评估影响。
5. 由负责人决定：
   - 接受。
   - 拆分。
   - 延后。
   - 取消。

---

## 76. 决策记录

以下决定必须记录：

- 技术栈。
- 模块边界。
- 数据格式。
- 网络协议。
- 存档策略。
- 资源策略。
- 发布策略。
- 风险接受。
- 放弃的重要方案。

---

## 77. Blocked 管理

任务进入 Blocked 后必须：

- 立即记录。
- 通知相关人员。
- 明确解除条件。
- 明确下一次检查时间。
- 评估是否可并行其他工作。

---

## 78. 依赖管理

每项依赖必须明确：

- 提供方。
- 接收方。
- 内容。
- 版本。
- 截止时间。
- 验收方式。
- 风险。
- 替代方案。

---

## 79. 进度报告

进度报告应基于：

- 已完成交付物。
- 已通过测试。
- 已合入内容。
- 剩余风险。
- 阻塞。

不以“已写多少代码”作为主要进度。

---

## 80. 任务关闭

关闭任务前检查：

- 验收标准。
- 代码。
- 测试。
- 文档。
- Review。
- 合入。
- 构建。
- 发布或交付。
- 后续任务。

---

## 81. 任务重开

出现以下情况可重开：

- 验收失败。
- 回归缺陷。
- 文档遗漏。
- 测试遗漏。
- 合入后行为与预期不一致。
- 关键风险未处理。

---

## 82. 工作流工具

项目可使用：

- GitHub Issues。
- GitLab Issues。
- Jira。
- Linear。
- Notion。
- 自建任务系统。

无论工具如何变化，状态和规则应保持一致。

---

## 83. 自动化

可自动化：

- Branch 命名检查。
- Commit 格式检查。
- 编译。
- 测试。
- 文档链接检查。
- PR 模板检查。
- Package 锁定检查。
- 敏感信息扫描。
- Build。
- Release Notes 草稿。

自动化不能替代风险判断和架构评审。

---

## 84. 流程例外

确实需要跳过某个流程时，必须记录：

- 跳过内容。
- 原因。
- 风险。
- 批准人。
- 临时措施。
- 补充时间。

例外不能长期成为默认流程。

---

## 85. 常见错误

### 85.1 未澄清需求就编码

导致返工。

### 85.2 大任务一次实现

导致 Review 和回滚困难。

### 85.3 代码完成后才补测试

导致设计不可测试。

### 85.4 只更新代码不更新文档

导致上下文漂移。

### 85.5 多窗口同时修改同一模块

导致接口和命名不一致。

### 85.6 发布前继续加功能

导致版本不稳定。

### 85.7 Hotfix 夹带重构

提高线上风险。

### 85.8 Blocked 不记录

导致计划失真。

---

## 86. 工作流检查清单

### Requirement

- [ ] 背景明确。
- [ ] 目标明确。
- [ ] Non-Goals 明确。
- [ ] 验收标准明确。
- [ ] 风险明确。
- [ ] 依赖明确。

### Design

- [ ] 需要 Design 时已创建。
- [ ] 需要 RFC 时已创建。
- [ ] 架构一致。
- [ ] 方案已评审。
- [ ] 测试策略明确。
- [ ] 回滚明确。

### Implementation

- [ ] 分支正确。
- [ ] 基线正确。
- [ ] 小步实现。
- [ ] 持续编译。
- [ ] 持续测试。
- [ ] Commit 清晰。
- [ ] 文档同步。

### Review

- [ ] 自检完成。
- [ ] PR 描述完整。
- [ ] 必要评审人已参与。
- [ ] Blocker 已解决。
- [ ] Major 已处理。
- [ ] 复审完成。

### Test

- [ ] Unit Test 通过。
- [ ] Integration Test 通过。
- [ ] PlayMode Test 通过。
- [ ] 回归测试通过。
- [ ] 性能影响确认。
- [ ] 内存影响确认。
- [ ] 网络影响确认。

### Merge

- [ ] 合入条件满足。
- [ ] 冲突已解决。
- [ ] 合并策略正确。
- [ ] 合入后验证通过。

### Delivery

- [ ] 验收标准全部满足。
- [ ] 文档和索引已更新。
- [ ] 已知问题已记录。
- [ ] 任务状态正确。
- [ ] 后续工作已创建。

---

## 87. 角色职责矩阵

| 活动 | Product / Design | Developer | Tech Lead | QA | Build / Release |
|---|---|---|---|---|---|
| Requirement | Responsible | Consulted | Consulted | Consulted | Informed |
| Architecture | Consulted | Consulted | Accountable | Consulted | Informed |
| Implementation | Informed | Responsible | Accountable | Consulted | Informed |
| Code Review | Informed | Responsible | Accountable | Consulted | Informed |
| Test | Consulted | Responsible | Consulted | Accountable | Informed |
| Release | Informed | Consulted | Consulted | Consulted | Accountable |

团队规模较小时，一个人可以承担多个角色，但职责仍应区分。

---

## 88. AI 角色矩阵

| 活动 | AI 可参与 | AI 不可独立决定 |
|---|---|---|
| Requirement Analysis | 是 | 需求优先级 |
| Architecture Draft | 是 | 最终架构批准 |
| Code Generation | 是 | 最终代码批准 |
| Test Generation | 是 | 测试完成结论 |
| Review Assistance | 是 | 最终合入批准 |
| Release Notes | 是 | 发布决策 |
| Incident Analysis | 是 | 生产环境处置 |

---

## 89. 验收标准

本规范执行后，应达到：

- 每个任务都有明确入口和完成条件。
- 需求、设计、实现、测试和文档形成闭环。
- 高风险修改经过 Design、RFC 和评审。
- 开发工作能够小步验证和回滚。
- Bug 修复包含根因和回归测试。
- 性能优化基于数据。
- 发布拥有明确门禁。
- Hotfix 范围最小且可追踪。
- 阻塞、依赖和风险能够被及时发现。
- AI 能够参与流程，但不能绕过人工决策。
- 新成员可以通过文档理解 Project Aether 的研发方式。
- 项目可以在跨会话、跨工具和跨人员情况下稳定延续。

---

## 90. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-08-06 | 创建 Project Aether 项目工作流正式初稿 |

---

# End
