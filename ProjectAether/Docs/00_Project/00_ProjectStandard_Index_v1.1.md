# Project Aether Project Standard Index

> **文件名：** `00_ProjectStandard_Index.md`  
> **文档编号：** PAS-000  
> **版本：** v1.1  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **Unity 版本：** Unity 2022.3.51f1c1  
> **最后更新：** 2026-08-08  

---

## 1. 文档目的

本文档是 Project Aether `00_ProjectStandard` 规范体系的统一入口。

Project Standard 用于定义整个项目共同遵守的：

- 工程目录。
- 文档管理。
- Git 工作流。
- 编码规范。
- 评审规范。
- 测试规范。
- AI 协作规范。
- 项目研发流程。
- 项目里程碑规则。

所有 Runtime、Editor、Tools、Tests、Build Pipeline 以及 AI 辅助开发工作均必须遵守本规范体系。

---

## 2. Project Standard 文档清单

| 编号 | 文档 | 文档 ID | 状态 | 职责 |
|---|---|---|---|---|
| 00 | [Project Standard Index](./00_ProjectStandard_Index.md) | PAS-000 | Draft | 规范体系入口与状态 |
| 01 | [Directory Standard](./01_DirectoryStandard.md) | PAS-001 | Draft | 工程目录、分层与文件职责 |
| 02 | [Document Standard](./02_DocumentStandard.md) | PAS-002 | Draft | 文档分类、格式、状态和维护规则 |
| 03 | [Git Standard](./03_GitStandard.md) | PAS-003 | Draft | Branch、Commit、PR、Merge、Release |
| 04 | [Coding Standard](./04_CodingStandard.md) | PAS-004 | Draft | C#、Unity、框架、第三方库与 AI 代码规范 |
| 05 | [Review Standard](./05_ReviewStandard.md) | PAS-005 | Draft | Architecture、Design、Code、Release Review |
| 06 | [Test Standard](./06_TestStandard.md) | PAS-006 | Draft | Unit、Integration、PlayMode、Performance、Release Test |
| 07 | [AI Standard](./07_AIStandard.md) | PAS-007 | Draft | ChatGPT、Codex、Claude Code、Gemini 等 AI 协作规则 |
| 08 | [Project Workflow](./08_ProjectWorkflow.md) | PAS-008 | Draft | Requirement → Design → Implementation → Review → Test → Merge |
| 09 | [Project Milestone](./09_ProjectMilestone.md) | PAS-009 | Draft | 阶段目标、Exit Criteria、版本与质量门禁 |

---

## 3. 文档阅读顺序

新成员或新 AI 会话推荐按以下顺序读取：

```text
00_ProjectStandard_Index
  ↓
01_DirectoryStandard
  ↓
02_DocumentStandard
  ↓
03_GitStandard
  ↓
04_CodingStandard
  ↓
05_ReviewStandard
  ↓
06_TestStandard
  ↓
07_AIStandard
  ↓
08_ProjectWorkflow
  ↓
09_ProjectMilestone
```

执行具体模块任务时，再补充：

```text
Current Module Architecture
  ↓
Current Module Design / RFC
  ↓
Current Source Code
  ↓
Current Tests
```

---

## 4. Project Documentation Structure

Project Aether 正式文档体系：

```text
Docs/
├── 00_ProjectStandard/
├── 01_Architecture/
├── 02_Design/
├── 03_Review/
├── 04_RFC/
├── 05_Test/
├── 06_DecisionLog/
├── 07_AI/
└── Templates/
```

目录职责以 `01_DirectoryStandard.md` 和 `02_DocumentStandard.md` 为准。

---

## 5. 文档类别

### 5.1 Project Standard

```text
Docs/00_ProjectStandard/
```

定义整个项目共同遵守的规则。

### 5.2 Architecture

```text
Docs/01_Architecture/
```

记录当前正式采用的系统职责、模块边界、依赖、生命周期、公开接口和数据流。

### 5.3 Design

```text
Docs/02_Design/
```

记录详细方案、候选设计、取舍、实现计划和风险。

### 5.4 Review

```text
Docs/03_Review/
```

记录 Architecture、Design、Code、Test、Performance 和 Release Review 结果。

### 5.5 RFC

```text
Docs/04_RFC/
```

处理跨模块或高风险技术变更。

### 5.6 Test

```text
Docs/05_Test/
```

保存测试计划、测试结果、性能数据和发布验收记录。

### 5.7 Decision Log

```text
Docs/06_DecisionLog/
```

记录重要技术决策、选择原因、影响和重新评估条件。

### 5.8 AI

```text
Docs/07_AI/
```

保存 AI 项目上下文、Handoff、Prompt 和协作约束。

### 5.9 Templates

```text
Docs/Templates/
```

保存统一文档模板。

---

## 6. 项目事实与冲突处理

Project Aether 不依赖单一聊天记录作为项目事实来源。

遇到冲突时应检查：

1. 当前已批准的 Project Standard。
2. 当前已批准的 Architecture。
3. 当前已批准的 RFC / Decision Log。
4. 当前模块 Design。
5. 当前真实源码。
6. 当前测试。
7. 历史聊天或 AI 输出。

如果正式文档和当前源码冲突：

- 不得静默选择任意一方。
- 必须明确记录冲突。
- 由负责人确认当前基线。
- 同步修正文档和代码。

---

## 7. 核心工程原则

### Architecture First

高风险和跨模块能力先设计、评审，再实现。

### Single Source of Truth

同一项规则只维护一个正式权威来源。

### Incremental Change

基于当前实现做可评审、可验证、可回滚的小步修改。

### Module Isolation

保持明确依赖方向，禁止通过共享全局状态绕过模块边界。

### Testable by Design

核心逻辑必须可测试，外部依赖应尽量可替换。

### Documentation Is Delivery

架构、接口、生命周期和流程变化必须同步文档。

### Human Approval for High-Risk Changes

AI 可以辅助，但不能独立批准架构、风险接受、合入和发布。

---

## 8. Git Commit 基线

Project Aether 统一使用：

```text
[模块][类型] 描述
```

示例：

```text
[Framework][Fix] Prevent duplicate module registration
[Resource][Feature] Add asynchronous resource loading
[Docs][Update] Update project standard index
```

详细规则见：

[03_GitStandard.md](./03_GitStandard.md)

---

## 9. 当前技术基线

当前 Project Aether 基线：

```text
Unity 2022.3.51f1c1
UniTask
VContainer
Addressables
FishNet
```

第三方库的实际 Package 版本必须记录在工程依赖清单中。

---

## 10. 当前规范包状态

当前 `00～09` 已全部生成，但整套规范仍处于：

```text
Draft
```

第一次一致性评审发现 `01_DirectoryStandard.md` 与后续规范存在目录结构差异。

当前批准流程：

```text
Generate 00～09
  ↓
Consistency Review
  ↓
Fix Directory Standard
  ↓
Cross-Document Validation
  ↓
Second Review
  ↓
Approved
```

---

## 11. 当前 Review 结论

第一次一致性评审：

```text
REV-PS-001
Status: Changes Requested
```

主要待处理：

- `01_DirectoryStandard.md` 的旧目录结构。
- Tests 路径统一。
- Framework / Gameplay / Presentation 分层统一。
- BuildScripts 与构建输出职责统一。
- 文档头部格式统一。

---

## 12. Project Milestone 入口

Project Standard 对应：

```text
PA-M0 Project Standard
```

当前 PA-M0 仍未完成。

完成条件：

- [x] `00～09` 文档已生成。
- [x] 第一次一致性 Review 已执行。
- [ ] `01_DirectoryStandard.md` 已修订。
- [ ] 全部路径引用已复查。
- [ ] 第二次一致性 Review 已通过。
- [ ] `00～09` 状态统一为 Approved。
- [ ] 文档已提交 Git。

---

## 13. 下一步

下一步进入：

```text
01_DirectoryStandard.md v1.1 修订
```

目标是使目录结构与：

- `04_CodingStandard.md`
- `06_TestStandard.md`
- `08_ProjectWorkflow.md`
- `09_ProjectMilestone.md`

完全一致。

---

## 14. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-07-28 | 创建 Project Standard 初始索引 |
| v1.1 | 2026-08-08 | 完成 00～09 文档登记，并加入第一次一致性评审状态 |

---

# End
