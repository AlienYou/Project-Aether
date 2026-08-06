# Project Aether 文档规范

> **文件名：** `02_DocumentStandard.md`  
> **文档编号：** PAS-002  
> **版本：** v1.0  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **最后更新：** 2026-08-06

---

## 1. 文档目的

本文档定义 Project Aether 的统一文档规范，用于约束项目内所有架构、设计、评审、RFC、测试、决策记录和 AI 协作文档。

本规范的目标是：

- 建立统一、清晰、可维护的文档体系。
- 保证项目架构、设计决策和代码实现可追踪。
- 降低多人协作中的信息偏差。
- 避免聊天记录、个人记忆或临时说明成为唯一依据。
- 为 ChatGPT、Codex、Claude Code、Gemini 等 AI 工具提供稳定的项目上下文。
- 让 Project Aether 在人员变动、会话切换和长期维护后仍能准确恢复开发状态。

---

## 2. 适用范围

本文档适用于以下内容：

- 项目规范。
- 系统架构。
- 模块设计。
- 技术评审。
- RFC 提案。
- 测试方案与测试结果。
- 技术决策记录。
- AI 协作说明。
- 文档模板。

适用人员包括：

- 客户端开发。
- 服务端开发。
- 技术美术。
- 工具开发。
- 测试人员。
- 项目负责人。
- 使用 AI 参与开发的成员。

---

## 3. 核心原则

### 3.1 文档是项目知识基线

Project Aether 不以聊天记录、口头说明或个人记忆作为长期知识基线。

项目的重要知识必须沉淀到 `Docs/` 目录中，包括：

- 模块职责。
- 系统边界。
- 生命周期。
- 公开接口。
- 关键数据流。
- 重要技术决策。
- 设计取舍。
- 已知限制。
- 测试结论。
- 架构变更。

### 3.2 架构与代码必须同步

当代码发生以下变化时，必须同步更新相关文档：

- 模块职责或依赖发生变化。
- 初始化、运行或销毁顺序发生变化。
- 生命周期发生变化。
- 公开 API 或数据结构发生变化。
- 配置、资源、网络或存档流程发生变化。
- 核心系统行为发生变化。

禁止长期存在“代码已经修改，但文档仍描述旧实现”的情况。

### 3.3 文档描述当前真实状态

正式文档必须描述当前项目实际采用的方案。禁止将以下内容混入正式架构文档：

- 未确认的设想。
- 已废弃实现。
- 临时试验代码。
- 未通过评审的方案。
- 与当前代码不一致的历史版本。

待讨论内容应进入 RFC 或 Design 文档，不应直接写入已批准的 Architecture 文档。

### 3.4 单一事实来源

同一项规则或设计应有一个明确的权威文档。其他文档可以引用该内容，但不应复制并维护多份不同版本。

示例：

```text
本模块生命周期以：
Docs/01_Architecture/Framework/ModuleLifecycle.md

为唯一依据。
```

### 3.5 文档必须可执行

规范文档不能只描述原则，还应尽量给出：

- 明确规则。
- 适用范围。
- 正确示例。
- 错误示例。
- 检查清单。
- 验收标准。

---

## 4. 文档目录结构

Project Aether 的文档目录统一为：

```text
ProjectAether/
└── Docs/
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

各目录职责如下：

| 目录 | 职责 |
|---|---|
| `00_ProjectStandard` | 项目级开发规范、流程和统一标准 |
| `01_Architecture` | 当前已采用的系统架构和模块边界 |
| `02_Design` | 模块详细设计、方案分析和实现设计 |
| `03_Review` | 架构评审、代码评审和阶段评审记录 |
| `04_RFC` | 重大变更提案和评审过程 |
| `05_Test` | 测试方案、测试记录和验收结果 |
| `06_DecisionLog` | 关键技术决策及其原因 |
| `07_AI` | AI 使用说明、上下文、提示词和协作约束 |
| `Templates` | 文档模板 |

不得在 `Docs/` 根目录随意放置未分类文档。

---

## 5. 文档分类

### 5.1 Project Standard

用于定义项目统一规则，例如目录、文档、Git、编码、Review、测试、AI 协作、工作流和里程碑规范。

存放路径：

```text
Docs/00_ProjectStandard/
```

### 5.2 Architecture

用于描述当前正式采用的系统架构，重点回答：

- 系统由哪些模块组成？
- 每个模块负责什么？
- 模块之间如何依赖？
- 系统如何初始化、运行和关闭？
- 核心数据如何流动？
- 哪些接口可以被其他模块使用？
- 哪些约束不能被破坏？

Architecture 文档不应包含大量具体实现代码。必要代码仅用于说明接口或核心流程。

存放路径：

```text
Docs/01_Architecture/
```

### 5.3 Design

用于说明某个模块或功能如何设计，以及为什么采用该方案。

建议包含：

1. 背景。
2. 问题定义。
3. 目标。
4. 非目标。
5. 约束条件。
6. 候选方案。
7. 方案对比。
8. 最终方案。
9. 数据结构。
10. 生命周期。
11. 异常处理。
12. 性能考虑。
13. 扩展方向。
14. 测试计划。
15. 已知风险。

存放路径：

```text
Docs/02_Design/
```

### 5.4 Review

用于记录正式评审过程和结论。

必须包含：

- 评审对象与范围。
- 评审时间与参与人员。
- 发现的问题及问题等级。
- 修改建议与处理结果。
- 最终结论。

存放路径：

```text
Docs/03_Review/
```

### 5.5 RFC

RFC 用于提出可能影响多个模块、公共接口或系统架构的重大变更。

以下情况通常必须创建 RFC：

- 新增核心系统。
- 修改模块边界或公共 API。
- 修改资源生命周期、配置加载流程或网络架构。
- 引入新的核心第三方库。
- 替换现有基础设施。
- 修改存档或协议格式。

推荐流程：

```text
Draft
  ↓
Review
  ↓
Approved / Rejected
  ↓
Update Architecture
  ↓
Implementation
  ↓
Verification
  ↓
Closed
```

存放路径：

```text
Docs/04_RFC/
```

### 5.6 Test

用于描述测试目标、方法、环境、结果和结论。

至少包含：

- 测试目标与范围。
- 测试环境和前置条件。
- 测试步骤。
- 预期结果与实际结果。
- 性能数据与异常情况。
- 最终结论。

存放路径：

```text
Docs/05_Test/
```

### 5.7 Decision Log

用于记录重要技术决策，回答：

- 当时遇到了什么问题？
- 有哪些可选方案？
- 最终选择了什么？
- 为什么这样选择？
- 对项目有什么影响？
- 是否可逆？
- 何时需要重新评估？

存放路径：

```text
Docs/06_DecisionLog/
```

### 5.8 AI

用于保存 AI 参与开发所需的稳定上下文和约束，例如项目概要、技术栈、代码生成规则、模块边界、当前实现基线和 AI 交接说明。

存放路径：

```text
Docs/07_AI/
```

### 5.9 Templates

用于保存统一文档模板。模板只定义格式，不承载实际设计结论。

存放路径：

```text
Docs/Templates/
```

---

## 6. 文件命名规范

### 6.1 基本格式

项目标准文档采用：

```text
编号_名称.md
```

示例：

```text
00_ProjectStandard_Index.md
01_DirectoryStandard.md
02_DocumentStandard.md
03_GitStandard.md
04_CodingStandard.md
```

### 6.2 模块文档命名

模块文档推荐使用：

```text
编号_模块名_文档类型.md
```

示例：

```text
01_Framework_Architecture.md
02_Resource_Design.md
03_Config_Review.md
```

也可以按模块建立子目录，但同一目录中必须保持一致，禁止混用多种命名方式。

### 6.3 命名要求

文件名必须：

- 使用英文。
- 使用项目统一的命名方式。
- 使用下划线分隔编号和名称。
- 能够直接反映文档内容。
- 避免模糊名称。

禁止：

```text
NewDocument.md
临时.md
最终版.md
最终版2.md
test.md
说明.md
```

---

## 7. 文档头部信息

正式文档必须在标题下方提供元信息。

推荐格式：

```markdown
> **文件名：** `02_DocumentStandard.md`  
> **文档编号：** PAS-002  
> **版本：** v1.0  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **最后更新：** 2026-08-06
```

必要时可增加：

```markdown
> **负责人：**  
> **评审人：**  
> **关联 RFC：**  
> **关联模块：**  
> **替代文档：**
```

---

## 8. 文档状态

统一使用以下状态：

| 状态 | 含义 |
|---|---|
| `Draft` | 正在编写，内容可能变化 |
| `Review` | 已提交评审，等待结论 |
| `Approved` | 已通过评审，可作为正式依据 |
| `Frozen` | 已冻结，修改必须经过正式流程 |
| `Deprecated` | 已废弃，不再作为当前依据 |
| `Archived` | 仅保留历史记录 |

推荐状态流转：

```text
Draft
  ↓
Review
  ↓
Approved
  ↓
Frozen
```

被替代后：

```text
Approved / Frozen
  ↓
Deprecated
  ↓
Archived
```

禁止删除仍有历史价值的技术文档。

---

## 9. 文档版本

### 9.1 版本格式

使用：

```text
vMajor.Minor
```

示例：

```text
v1.0
v1.1
v2.0
```

### 9.2 版本升级规则

升级 Minor：

- 补充说明。
- 修正文案。
- 增加示例。
- 不改变核心规则。

升级 Major：

- 修改核心架构。
- 修改关键流程。
- 修改公共约束。
- 与旧版本不兼容。

### 9.3 Git 是完整历史来源

文档正文不需要保存所有修改内容，完整变更历史由 Git 提供。重大变化可在文档末尾保留简要 Change Log。

---

## 10. Markdown 排版规范

### 10.1 标题

每个文件只能有一个一级标题。章节从二级标题开始，并按层级递进，禁止无规则跳级。

### 10.2 段落

段落之间保留一个空行。复杂内容应拆分为小节、表格、流程图、示例或检查清单。

### 10.3 列表

无顺序内容使用项目符号，有执行顺序的内容使用编号列表。

### 10.4 代码块

代码块必须标记语言：

```csharp
public sealed class ResourceManager
{
}
```

目录和流程使用 `text`：

```text
Create
  ↓
Initialize
  ↓
Running
  ↓
Shutdown
```

### 10.5 表格

表格用于结构化信息，不应用于承载大段文字。

### 10.6 强调

重要术语使用粗体；文件名、类名、接口名和命令使用行内代码。禁止过度使用强调样式造成视觉噪声。

### 10.7 引用

引用其他文档时必须提供相对路径。

示例：

```markdown
- [目录规范](./01_DirectoryStandard.md)
- [Framework 架构](../01_Architecture/Framework/Framework_Architecture.md)
```

禁止只写“参考前面的文档”。

---

## 11. 图表规范

### 11.1 优先使用文本图

简单结构优先使用文本或 Mermaid。

```text
Bootstrap
  ↓
ModuleManager
  ↓
Game Modules
```

### 11.2 Mermaid

复杂流程可以使用 Mermaid，但必须保证节点清晰、方向明确、图表与正文一致，且不能让图表成为唯一的信息来源。

### 11.3 图片文件

图片统一存放在文档相邻的 `Images/` 目录，并使用相对路径引用。

```text
Docs/
└── 01_Architecture/
    └── Resource/
        ├── Resource_Architecture.md
        └── Images/
            └── ResourceLifecycle.png
```

---

## 12. 代码示例规范

文档中的代码示例必须：

- 与项目当前技术栈一致。
- 与当前接口定义一致。
- 命名符合 Coding Standard。
- 不省略影响理解的关键代码。
- 明确说明是完整代码、伪代码还是示意代码。

禁止将无法编译的示意代码描述为正式实现。

---

## 13. Architecture 文档推荐结构

```markdown
# Module Architecture

## 1. Overview
## 2. Responsibility
## 3. Boundaries
## 4. Dependencies
## 5. Lifecycle
## 6. Public Interfaces
## 7. Data Flow
## 8. Error Handling
## 9. Performance Constraints
## 10. Extension Points
## 11. Known Limitations
## 12. Acceptance Criteria
```

Architecture 文档重点描述稳定规则，而不是逐行解释代码。

---

## 14. Design 文档推荐结构

```markdown
# Feature Design

## 1. Background
## 2. Problem
## 3. Goals
## 4. Non-Goals
## 5. Constraints
## 6. Candidate Solutions
## 7. Comparison
## 8. Final Design
## 9. Data Structures
## 10. Lifecycle
## 11. Error Handling
## 12. Performance
## 13. Test Plan
## 14. Risks
## 15. Open Questions
```

所有未决问题必须明确列出，不能隐藏在正文中。

---

## 15. Review 文档推荐结构

```markdown
# Review Record

## 1. Review Target
## 2. Scope
## 3. Participants
## 4. Findings
## 5. Required Changes
## 6. Optional Improvements
## 7. Resolution
## 8. Final Decision
```

问题等级：

| Level | Meaning |
|---|---|
| Blocker | 不修复不能合入 |
| Major | 高风险问题，必须处理 |
| Minor | 不影响合入，但建议处理 |
| Suggestion | 可选改进 |

---

## 16. RFC 文档推荐结构

```markdown
# RFC Title

## 1. Summary
## 2. Background
## 3. Problem
## 4. Proposal
## 5. Alternatives
## 6. Compatibility
## 7. Migration Plan
## 8. Risks
## 9. Test Plan
## 10. Decision
```

RFC 必须说明兼容性、迁移方式、接口影响、存档或协议影响以及回滚能力。

---

## 17. Decision Log 推荐结构

```markdown
# Decision: Use UniTask for Runtime Async

> **Decision ID:** ADR-001  
> **Status:** Approved  
> **Date:** 2026-08-06

## Context
## Options
## Decision
## Reason
## Impact
## Revisit Conditions
```

---

## 18. 文档与代码同步流程

```text
Identify Change
  ↓
Locate Related Documents
  ↓
Update Design or RFC
  ↓
Review
  ↓
Update Architecture
  ↓
Modify Code
  ↓
Run Tests
  ↓
Update Test Record
  ↓
Commit Code and Documents
```

对于小型修复，可以先修改代码，但必须在同一个 Pull Request 中同步更新文档。

---

## 19. 文档评审要求

以下文档必须评审：

- Project Standard。
- Architecture。
- 重大 Design。
- RFC。
- 核心系统测试结论。
- 关键 Decision Log。

评审至少确认：

- 内容是否与当前代码一致。
- 模块职责和依赖是否清晰。
- 生命周期和接口是否完整。
- 是否存在未说明的风险。
- 是否与其他文档冲突。
- 是否需要更新索引。

---

## 20. 文档索引

`00_ProjectStandard_Index.md` 是项目规范目录入口。新增、重命名或废弃 Project Standard 文档时，必须同步更新索引。

大型目录也应建立自己的索引，例如：

```text
Docs/01_Architecture/00_Architecture_Index.md
Docs/02_Design/00_Design_Index.md
```

---

## 21. AI 上下文规则

向 AI 提供 Project Aether 上下文时，应优先提供：

1. 当前任务相关的 Project Standard。
2. 当前模块的 Architecture。
3. 当前模块的 Design。
4. 最新 Review 或 RFC。
5. 当前真实源码。

禁止仅根据聊天摘要要求 AI 修改核心框架。

当文档与代码冲突时：

- 不得自行猜测。
- 必须明确指出冲突。
- 由项目负责人确认当前基线。
- 确认后同步修正文档和代码。

---

## 22. 禁止事项

禁止：

- 用“最终版2”“新版本”“临时”等方式命名正式文档。
- 将聊天记录直接作为正式架构文档。
- 在多个文档中复制同一套规则并分别维护。
- 修改核心代码后不更新文档。
- 删除重要历史决策。
- 将未评审方案标记为 Approved。
- 将伪代码描述为已编译实现。
- 省略重要限制和风险。
- 使用“大概如此”“以后再处理”等模糊描述代替正式结论。
- 让文档路径与索引长期不一致。

---

## 23. 文档完成检查清单

提交文档前必须确认：

- [ ] 文件存放在正确目录。
- [ ] 文件名符合规范。
- [ ] 文档头部信息完整。
- [ ] 状态和版本正确。
- [ ] 标题层级正确。
- [ ] 文档术语与项目一致。
- [ ] 不存在与当前架构冲突的内容。
- [ ] 代码示例已标明性质。
- [ ] 相对链接有效。
- [ ] 重要流程已说明。
- [ ] 风险和限制已记录。
- [ ] 相关索引已更新。
- [ ] 相关代码修改已同步。
- [ ] Git Commit 符合项目规范。

---

## 24. 验收标准

执行本规范后，应达到：

- Project Aether 拥有统一的文档结构。
- 文档可以作为项目长期知识库。
- 新成员可以通过文档理解系统。
- 新会话可以通过文档恢复上下文。
- 多种 AI 工具可以基于同一套资料协作。
- 架构、设计、实现和测试可以完整追踪。
- 重要技术决策不会只存在于聊天记录中。

---

## 25. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-08-06 | 创建 Project Aether 文档规范正式初稿 |

---

# End
