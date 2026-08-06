# Project Aether AI 协作规范

> **文件名：** `07_AIStandard.md`  
> **文档编号：** PAS-007  
> **版本：** v1.0  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **Unity 版本：** Unity 2022.3.51f1c1  
> **最后更新：** 2026-08-06  

---

## 1. 文档目的

本文档定义 Project Aether 中使用 AI 工具进行需求分析、架构设计、代码生成、代码修改、测试、文档编写、评审辅助和问题排查时的统一规范。

适用工具包括但不限于：

- ChatGPT。
- Codex。
- Claude Code。
- Gemini。
- GitHub Copilot。
- Cursor。
- 其他具备代码生成、代码分析或 Agent 能力的 AI 工具。

本规范的目标是：

- 让 AI 基于真实项目上下文工作。
- 防止 AI 重新发明接口或破坏现有架构。
- 防止不同 AI 给出互相冲突的实现。
- 保证 AI 输出可以审查、验证、回滚和追踪。
- 防止未验证代码直接进入正式工程。
- 将 AI 作为工程协作工具，而不是项目事实来源。
- 建立可跨会话、跨模型、跨成员复用的 AI 工作流。

---

## 2. 适用范围

本规范适用于 AI 参与的以下活动：

- 需求澄清。
- 技术方案分析。
- 架构设计。
- Design 文档编写。
- RFC 草拟。
- 代码生成。
- 代码修改。
- 重构建议。
- Bug 分析。
- 日志分析。
- 测试生成。
- Code Review 辅助。
- 性能分析辅助。
- 文档生成。
- Git Commit 和 PR 文案。
- 项目交接。
- 开发流程自动化。

---

## 3. 核心原则

### 3.1 AI Is an Assistant, Not the Source of Truth

AI 不是 Project Aether 的事实来源。

项目事实来源优先级：

```text
Approved Architecture
  ↓
Approved Design / RFC / Decision Log
  ↓
Current Source Code
  ↓
Tests
  ↓
Project Standard
  ↓
AI Conversation
```

当不同来源冲突时，必须人工确认当前基线。

AI 不得根据自身记忆覆盖项目真实实现。

---

### 3.2 Context Before Generation

AI 在生成或修改代码前，必须先获得足够上下文。

最低上下文：

- 当前任务。
- 当前模块职责。
- 当前真实源码。
- 相关接口。
- 生命周期。
- 命名空间。
- asmdef 依赖。
- 相关测试。
- 相关文档。

没有足够上下文时，AI 应明确说明假设或限制。

---

### 3.3 Incremental Change

AI 应基于现有实现做增量修改。

禁止在没有明确批准的情况下：

- 重写整个模块。
- 替换已确定架构。
- 修改公共接口。
- 删除已有能力。
- 改变生命周期。
- 替换第三方库。
- 引入新全局单例。
- 合并原本独立模块。
- 拆分原本稳定模块。
- 修改网络协议。
- 修改存档格式。

---

### 3.4 Verify Before Trust

AI 输出必须经过：

- 人工理解。
- 编译验证。
- 测试验证。
- Diff 检查。
- 文档一致性检查。
- 必要的性能验证。

禁止因为输出“看起来合理”就直接合入。

---

### 3.5 One Baseline

同一个任务必须使用一个明确基线。

推荐记录：

```text
Project Aether
Unity: 2022.3.51f1c1
Branch: feature/resource-manager
Commit: <commit hash>
Related Documents:
- Docs/00_ProjectStandard/04_CodingStandard.md
- Docs/01_Architecture/Resource/Resource_Architecture.md
```

不同 AI 工具必须基于同一份基线。

---

### 3.6 No Silent Assumptions

AI 不能静默假设：

- 某接口存在。
- 某字段存在。
- 某库版本可用。
- 某代码已编译。
- 某测试已通过。
- 某系统使用特定生命周期。
- 某目录结构已经建立。

必须明确区分：

- 已知事实。
- 文档规定。
- 源码事实。
- 推断。
- 建议。
- 未验证内容。

---

### 3.7 Human Owns the Decision

AI 可以建议，但不能替代项目负责人作出以下决定：

- 核心架构变更。
- 模块边界调整。
- 技术栈替换。
- 安全策略。
- 发布决策。
- 风险接受。
- 数据迁移。
- 协议兼容。
- 生产环境操作。

---

## 4. AI 使用角色

Project Aether 中 AI 可以承担以下角色：

| 角色 | 允许内容 |
|---|---|
| Analyst | 分析需求、日志、代码和风险 |
| Designer | 草拟 Design、RFC、接口方案 |
| Implementer | 按既有设计生成或修改代码 |
| Tester | 生成测试、测试数据和测试计划 |
| Reviewer | 辅助发现问题和检查规范 |
| Documenter | 生成和维护文档 |
| Git Assistant | 生成 Commit、PR、ChangeLog 草稿 |
| Debug Assistant | 分析 Bug、调用链和状态 |
| Migration Assistant | 辅助生成迁移步骤和兼容检查 |

AI 角色必须与当前任务一致。

禁止在实现任务中未经授权自行切换为架构决策者。

---

## 5. AI 不允许承担的角色

AI 不得独立承担：

- 最终架构批准。
- 最终代码批准。
- 最终安全批准。
- 最终发布批准。
- 生产密钥管理。
- 无人监督的破坏性 Git 操作。
- 无人监督的服务器操作。
- 无人监督的数据迁移。
- 无人监督的付费或采购操作。

---

## 6. 项目上下文结构

推荐 AI 上下文目录：

```text
Docs/
└── 07_AI/
    ├── 00_AIContext_Index.md
    ├── 01_ProjectOverview.md
    ├── 02_TechnologyStack.md
    ├── 03_CurrentArchitecture.md
    ├── 04_CurrentModules.md
    ├── 05_CurrentConstraints.md
    ├── 06_KnownIssues.md
    ├── 07_AIHandoff.md
    └── Prompts/
```

---

## 7. AI 上下文优先级

向 AI 提供资料时，推荐顺序：

1. 当前任务描述。
2. 相关 Project Standard。
3. 当前模块 Architecture。
4. 当前模块 Design。
5. 相关 RFC 或 Decision Log。
6. 当前源码。
7. 当前测试。
8. 当前日志和错误。
9. 历史讨论摘要。

历史聊天只能作为补充。

---

## 8. 新会话恢复上下文

新会话开始时，推荐提供：

```markdown
# Project Context

Project: Project Aether  
Unity: 2022.3.51f1c1  
Current Branch:  
Current Task:  
Current Module:  

## Required Documents

- 00_ProjectStandard_Index.md
- 04_CodingStandard.md
- 05_ReviewStandard.md
- 06_TestStandard.md
- Current module Architecture
- Current module Design

## Current Source Files

列出当前真实源码。

## Current Progress

说明已经完成、正在进行和下一步。

## Constraints

说明禁止修改的接口、命名和生命周期。
```

---

## 9. AI Handoff 文档

当任务跨会话、跨模型或跨成员时，应建立 AI Handoff。

推荐结构：

```markdown
# AI Handoff

## 1. Task

## 2. Current Status

## 3. Completed Work

## 4. Current Source Baseline

## 5. Confirmed Decisions

## 6. Open Issues

## 7. Do Not Change

## 8. Next Step

## 9. Verification
```

---

## 10. 任务描述规范

给 AI 的任务必须尽量包含：

- 目标。
- 当前行为。
- 期望行为。
- 影响范围。
- 相关文件。
- 限制。
- 验收标准。
- 是否允许修改接口。
- 是否允许新增文件。
- 是否需要测试。
- 是否需要文档。

不推荐：

```text
帮我优化一下。
```

推荐：

```text
基于当前 ResourceManager 和 ResourceHandle<T> 实现，
修复重复 Release 导致引用计数下溢的问题。

限制：
- 不修改 IResourceManager 公开接口。
- 不修改 ResourceState 枚举。
- 保持现有 Namespace。
- 增加回归测试。
- 输出修改文件和验证步骤。
```

---

## 11. 代码输入规范

提供源码时应确保：

- 文件完整。
- 版本正确。
- 没有截断关键成员。
- 包含相关接口。
- 包含相关枚举。
- 包含调用方。
- 包含测试。
- 标明文件路径。

禁止只提供单个方法，却要求 AI 修改整个系统生命周期。

---

## 12. 代码输出规范

AI 输出代码时必须说明：

- 文件路径。
- 新增或修改。
- 主要变化。
- 是否完整文件。
- 是否为局部补丁。
- 是否已验证。
- 需要执行的测试。

完整文件输出必须与当前基线一致。

局部修改应给出明确定位。

---

## 13. 禁止伪造验证结果

AI 不得声称以下内容，除非确实执行过：

- 已编译通过。
- 已在 Unity 中运行。
- 已通过测试。
- 已验证 IL2CPP。
- 已验证 Android。
- 已验证服务器。
- 已验证性能。
- 已验证内存。
- 已验证 Addressables Build。

无法执行时必须写：

```text
以下实现尚未在 Unity 2022.3.51f1c1 中实际编译验证。
```

---

## 14. 接口保护

AI 修改前必须识别：

- Public API。
- Internal API。
- 序列化字段。
- 网络协议。
- 存档结构。
- Editor 入口。
- 第三方回调。
- 生命周期入口。

未经批准不得修改。

---

## 15. 生命周期保护

AI 修改拥有生命周期的系统时，必须检查：

```text
Create
Initialize
Running
Update
Shutdown
Dispose
```

必须确认：

- 重复初始化。
- 重复关闭。
- 部分失败。
- 中途取消。
- 销毁顺序。
- 资源释放。
- 事件取消。
- 异步任务结束。

---

## 16. 命名保护

AI 必须使用项目现有命名。

例如当前已确定：

```csharp
IGameModule
ModuleManager
ResourceManager
ResourceHandle<T>
ConfigManager
PoolManager
BootstrapRunner
```

不得在无理由情况下替换为：

```csharp
IModuleService
GameModuleRegistry
AssetService
HandleReference<T>
```

---

## 17. Namespace 保护

AI 必须读取当前 Namespace。

不得擅自：

- 移动 Namespace。
- 合并 Namespace。
- 使用不存在的根 Namespace。
- 破坏 asmdef 依赖方向。

---

## 18. 继承关系保护

AI 不得擅自：

- 将普通类改为 MonoBehaviour。
- 将 MonoBehaviour 改为普通类。
- 将 `virtual` 改为 `abstract`。
- 将接口改为抽象类。
- 将组合改为继承。
- 删除 `sealed`。
- 新增继承层级。

继承关系变化属于设计变更。

---

## 19. 状态保护

AI 修改状态机前必须列出：

- 当前状态。
- 当前转换。
- 新状态。
- 新转换。
- 非法转换。
- 兼容影响。

不得擅自删除已有状态。

---

## 20. 第三方库保护

Project Aether 当前已使用或计划使用：

- UniTask。
- VContainer。
- Addressables。
- FishNet。

AI 不得未经批准：

- 替换库。
- 同时引入同类库。
- 使用不兼容版本 API。
- 绕过项目适配层。
- 将第三方类型扩散到所有业务模块。

---

## 21. UniTask 使用规则

AI 生成异步代码必须：

- 使用 `Async` 后缀。
- 支持 `CancellationToken`。
- 避免 `async void`。
- 处理 fire-and-forget 异常。
- 绑定正确生命周期。
- 不在后台线程访问 Unity API。
- 不无限等待。

---

## 22. VContainer 使用规则

AI 必须：

- 优先构造函数注入。
- 在 Composition Root 注册。
- 明确 Lifetime。
- 避免业务代码主动解析容器。
- 避免所有服务都注册 Singleton。
- 保持 Runtime 与 Scene Scope 边界。

---

## 23. Addressables 使用规则

AI 必须通过项目 Resource Module 使用 Addressables。

不得在业务模块中直接散布：

```csharp
Addressables.LoadAssetAsync<T>()
```

必须明确：

- Handle 所有者。
- Release 位置。
- LoadAsset 与 Instantiate 区别。
- 失败处理。
- 重复释放保护。
- Scene 切换行为。

---

## 24. FishNet 使用规则

AI 生成网络代码必须明确：

- Authority。
- Ownership。
- RPC 方向。
- 服务端验证。
- Prediction。
- Reconciliation。
- 断线重连。
- 协议兼容。
- 带宽影响。

不得只实现客户端表现而忽略服务端权威。

---

## 25. AI 生成代码排版

AI 必须遵循 `04_CodingStandard.md`。

特别要求：

- 正常紧凑排版。
- 简单调用保持一行。
- 简单条件保持一行。
- 不为展示频繁拆行。
- 使用 Allman 大括号。
- 单行条件也使用大括号。
- 私有字段使用 `_camelCase`。
- 异步方法使用 `Async`。

---

## 26. AI 生成注释

AI 注释应解释：

- 为什么这样设计。
- 特殊约束。
- 兼容原因。
- 不明显的风险。

禁止生成大量重复代码含义的注释。

---

## 27. AI 生成测试

AI 修改行为时必须考虑测试。

至少说明：

- 正常路径。
- 失败路径。
- 边界条件。
- 重复调用。
- 生命周期。
- 回归场景。

禁止只生成 Happy Path 测试。

---

## 28. AI 修改测试的限制

AI 不得为了让测试通过而：

- 删除失败测试。
- 放宽断言。
- 增大超时掩盖竞态。
- 跳过测试。
- 修改测试目标。
- Mock 掉真实问题。

如果测试与新需求冲突，应先确认行为变化。

---

## 29. Bug 分析流程

AI 分析 Bug 时推荐流程：

```text
Reproduce
  ↓
Collect Logs
  ↓
Locate State
  ↓
Trace Call Path
  ↓
Identify Root Cause
  ↓
Propose Minimal Fix
  ↓
Add Regression Test
  ↓
Verify
```

AI 应优先寻找根因，不应只修改表面现象。

---

## 30. 日志分析要求

提供日志时应包含：

- 完整错误。
- Stack Trace。
- 触发步骤。
- Unity 版本。
- 平台。
- Commit。
- 相关代码。
- 当前状态。

AI 不得只根据单行错误断言根因。

---

## 31. 重构规则

AI 提议重构时必须说明：

- 当前问题。
- 重构目标。
- 外部行为是否变化。
- 公共 API 是否变化。
- 文件变化。
- 风险。
- 测试。
- 迁移步骤。
- 回滚方式。

重构与功能修改应尽量分开。

---

## 32. 架构设计规则

AI 可以草拟架构，但必须输出：

- Goals。
- Non-Goals。
- Constraints。
- Candidate Solutions。
- Tradeoffs。
- Dependencies。
- Lifecycle。
- Data Flow。
- Error Handling。
- Performance。
- Test Strategy。
- Risks。

AI 不得把单一方案描述为唯一正确答案。

---

## 33. RFC 生成规则

AI 草拟 RFC 时必须明确：

- 背景。
- 问题。
- 提案。
- 替代方案。
- 兼容性。
- 迁移。
- 风险。
- 测试。
- 回滚。
- 未决问题。

RFC 必须经过人工评审。

---

## 34. 文档生成规则

AI 生成 Project Aether 文档必须：

- 使用 Markdown。
- 使用正确文件名。
- 使用正确文档编号。
- 使用统一头部元信息。
- 使用当前目录规范。
- 与真实代码一致。
- 标记未验证内容。
- 更新索引。
- 不虚构不存在的模块。

---

## 35. 文档更新规则

AI 修改文档时必须：

- 保留原有有效内容。
- 明确修改范围。
- 不无理由重写整篇。
- 不删除历史决策。
- 不改变状态而不说明。
- 不复制多个冲突版本。
- 保持相对链接。

---

## 36. Git 辅助规则

AI 可以生成：

- Branch 名称。
- Commit 信息。
- PR 标题。
- PR 描述。
- ChangeLog 草稿。
- Release Note 草稿。

必须使用 Project Aether 格式：

```text
[模块][类型] 描述
```

禁止擅自切换为 Conventional Commits。

---

## 37. Git 高风险操作

AI 不得未经人工确认执行：

```bash
git reset --hard
git clean -fd
git push --force
git filter-repo
git rebase --onto
```

AI 提供命令时必须说明风险和影响。

---

## 38. Shell 命令规则

AI 生成 Shell 命令时必须：

- 说明执行目录。
- 说明平台。
- 说明预期结果。
- 避免破坏性默认行为。
- 避免泄露密钥。
- 先提供只读检查命令。
- 必要时提供备份步骤。

---

## 39. 自动执行边界

AI Agent 可以自动执行低风险任务：

- 读取文件。
- 搜索代码。
- 运行测试。
- 运行编译。
- 生成报告。
- 格式化代码。
- 检查 Git Diff。

需要人工确认：

- 删除文件。
- 批量重命名。
- 修改公共 API。
- 修改构建配置。
- 修改 Package。
- 修改网络协议。
- 修改存档格式。
- 强制推送。
- 发布版本。
- 上传生产环境。

---

## 40. 敏感信息

不得向 AI 提供：

- 私钥。
- API Key。
- Token。
- 账号密码。
- 生产数据库凭据。
- 签名证书密码。
- 用户隐私数据。
- 未脱敏日志。

必须使用：

- 环境变量。
- Secret Manager。
- 脱敏示例。
- 测试凭据。

---

## 41. 隐私和数据

向外部 AI 服务上传内容前必须确认：

- 公司政策。
- 项目保密要求。
- 代码许可。
- 用户数据政策。
- 第三方协议。
- 数据保留策略。

未经允许不得上传：

- 未发布源码。
- 商业机密。
- 用户数据。
- 合作方私有资料。
- 安全漏洞细节。

---

## 42. 开源许可

AI 生成代码可能受训练来源影响。

开发者必须检查：

- 是否包含可识别第三方代码。
- 是否包含不兼容 License。
- 是否复制大段外部实现。
- 是否需要署名。
- 是否符合项目许可要求。

---

## 43. AI 输出质量等级

### Draft

仅供讨论，未验证。

### Reviewable

上下文完整，可以进入人工 Review。

### Verified

已经人工理解、编译和测试。

### Approved

已通过项目评审。

AI 默认输出只能视为 Draft 或 Reviewable。

---

## 44. AI 输出标记

复杂输出建议标记：

```markdown
## Verification Status

- Source baseline checked: Yes
- Compiled in Unity: No
- EditMode tests: Not run
- PlayMode tests: Not run
- Documentation updated: Yes
```

---

## 45. AI 修改记录

重要 AI 修改建议记录：

- 使用工具。
- 模型。
- 日期。
- 任务。
- 输入基线。
- 输出文件。
- 人工评审人。
- 验证结果。

不要求记录完整聊天内容。

---

## 46. 多 AI 协作

不同 AI 可以承担不同角色：

```text
ChatGPT
  → Architecture / Documentation

Codex
  → Repository Implementation

Claude Code
  → Large Code Analysis / Refactor Review

Gemini
  → Alternative Analysis / Documentation Review
```

角色只是建议，所有工具仍必须遵循同一基线。

---

## 47. 多 AI 冲突处理

当不同 AI 给出不同方案时：

1. 不直接选择更长或更自信的答案。
2. 对照 Architecture。
3. 对照当前源码。
4. 比较假设。
5. 比较风险。
6. 运行实验。
7. 记录决策。

---

## 48. AI 会话长度

长会话可能导致：

- 较早接口被遗忘。
- 不同版本混淆。
- 已废弃方案重新出现。
- 命名漂移。
- 生命周期漂移。

应定期：

- 更新文档。
- 生成 Handoff。
- 固定 Commit 基线。
- 开启新任务会话。
- 重新上传关键文件。

---

## 49. 聊天上下文不是长期存储

禁止将以下内容只保存在聊天中：

- 核心架构。
- 接口定义。
- 生命周期。
- 重大决策。
- 重要 Bug 根因。
- 发布流程。
- 数据迁移。
- 测试结论。

必须写入 `Docs/` 或源码。

---

## 50. AI Prompt 模板

### 50.1 代码修改

```markdown
你正在修改 Project Aether。

Unity: 2022.3.51f1c1
Module:
Current Branch:
Current Commit:

Task:

Constraints:
- 不修改公共接口。
- 不修改 Namespace。
- 不修改生命周期。
- 不引入新第三方库。
- 使用紧凑排版。
- 增加测试。

Relevant Documents:

Relevant Source Files:

Output:
1. 变更分析。
2. 修改文件。
3. 完整代码或补丁。
4. 测试。
5. 未验证内容。
```

---

### 50.2 Bug 分析

```markdown
Project:
Module:
Unity:
Commit:

Observed:
Expected:
Reproduction:
Logs:
Related Code:

请：
1. 区分事实和推断。
2. 分析根因。
3. 给出最小修复。
4. 给出回归测试。
5. 不修改无关接口。
```

---

### 50.3 架构设计

```markdown
Project:
Module:
Current Architecture:

Problem:
Goals:
Non-Goals:
Constraints:

请输出：
1. 候选方案。
2. 方案对比。
3. 推荐方案。
4. 生命周期。
5. 依赖关系。
6. 风险。
7. 测试策略。
8. 需要人工决策的问题。
```

---

## 51. AI 代码评审模板

```markdown
请基于以下内容进行 Review：

- 04_CodingStandard.md
- 05_ReviewStandard.md
- Current Architecture
- Current Diff

重点检查：
- 架构一致性
- 生命周期
- 状态转换
- 异步取消
- 资源释放
- GC
- 测试
- 文档

使用：
[Blocker]
[Major]
[Minor]
[Suggestion]
[Question]

不要修改代码，只输出问题和建议。
```

---

## 52. AI 输出检查清单

### Context

- [ ] 当前任务明确。
- [ ] 当前分支明确。
- [ ] 当前 Commit 明确。
- [ ] 相关文档已提供。
- [ ] 相关源码完整。
- [ ] 相关测试已提供。
- [ ] 禁止修改事项明确。

### Architecture

- [ ] 没有重新发明接口。
- [ ] 没有改变模块边界。
- [ ] 没有新增循环依赖。
- [ ] 没有引入隐藏全局状态。
- [ ] 生命周期保持一致。
- [ ] 状态机保持一致。

### Code

- [ ] 命名正确。
- [ ] Namespace 正确。
- [ ] asmdef 依赖正确。
- [ ] 排版符合规范。
- [ ] 异步可取消。
- [ ] 错误可诊断。
- [ ] 资源正确释放。
- [ ] 没有明显 GC 风险。
- [ ] 没有虚构 API。

### Test

- [ ] 正常路径测试。
- [ ] 失败路径测试。
- [ ] 边界测试。
- [ ] 回归测试。
- [ ] 测试未被削弱。
- [ ] 未虚构执行结果。

### Documentation

- [ ] 文档已同步。
- [ ] 状态正确。
- [ ] 版本正确。
- [ ] 索引已更新。
- [ ] 未验证内容已标记。

### Safety

- [ ] 没有敏感信息。
- [ ] 没有危险命令。
- [ ] 没有自动发布。
- [ ] 没有强制推送。
- [ ] 没有未确认的数据迁移。

---

## 53. AI Review 门禁

AI 生成的核心修改在合入前必须满足：

- 人工阅读全部 Diff。
- 人工理解实现。
- 编译通过。
- 相关测试通过。
- 文档同步。
- Code Review 通过。
- 没有未说明假设。
- 没有未确认接口变化。
- 没有未确认第三方依赖。
- 没有敏感信息。

---

## 54. AI 使用中的常见错误

### 54.1 只提供报错，不提供源码

AI 缺少调用上下文，容易误判。

### 54.2 同时让 AI 重构多个模块

上下文过大，容易产生接口漂移。

### 54.3 不固定版本

AI 根据旧代码生成新实现。

### 54.4 将建议当作事实

AI 的合理推断并不等于项目真实状态。

### 54.5 未运行测试就合入

隐藏错误进入共享分支。

### 54.6 不检查删除内容

AI 可能在重写时丢失已有能力。

### 54.7 多窗口同时推进同一模块

容易生成不同命名和不同架构。

### 54.8 只复制最终代码

丢失修改原因、风险和验证方式。

---

## 55. 多窗口协作规则

同一个模块原则上只在一个主会话中推进。

如果必须多窗口：

- 固定同一 Commit。
- 提供同一文档。
- 明确各窗口职责。
- 禁止同时修改同一文件。
- 合并前人工对比。
- 更新 Handoff。

---

## 56. AI 与 Codex 仓库协作

将聊天方案交给 Codex 时，应提供：

- Design 文档。
- 当前任务。
- 禁止修改事项。
- 验收标准。
- 测试要求。
- 目标文件。
- 当前 Commit。

Codex 完成后必须检查：

```bash
git status
git diff --stat
git diff
```

---

## 57. AI 与 Claude Code 协作

Claude Code 适合：

- 大范围源码阅读。
- 调用链分析。
- 重构风险分析。
- 跨文件一致性检查。

使用时应限制：

- 允许读取的目录。
- 允许修改的文件。
- 允许执行的命令。
- 禁止破坏性命令。
- 输出验证要求。

---

## 58. AI 与 Gemini 协作

Gemini 可以用于：

- 第二方案分析。
- 文档校对。
- 多模态资源分析。
- 代码解释。

关键实现仍必须基于项目真实源码验证。

---

## 59. AI Agent 权限

推荐权限等级：

### Read Only

- 搜索。
- 读取文件。
- 分析。
- 生成建议。

### Safe Write

- 修改指定文件。
- 新增测试。
- 新增文档。
- 运行格式化。
- 运行测试。

### Elevated

- 批量重构。
- 修改构建配置。
- 修改 Package。
- 修改 Git 历史。
- 发布。

Elevated 必须人工确认。

---

## 60. 命令确认

以下命令默认要求人工确认：

- 删除文件。
- 覆盖大量文件。
- 修改系统配置。
- 安装全局软件。
- 修改 Hosts。
- 修改证书。
- 强制 Git 操作。
- 上传文件。
- 发布构建。
- 访问生产服务。

---

## 61. AI 失败处理

当 AI 输出失败时，应：

1. 保存错误。
2. 检查上下文。
3. 检查版本。
4. 缩小任务。
5. 恢复基线。
6. 重新生成最小修改。
7. 不在错误输出上继续叠加修改。

---

## 62. AI 输出回滚

AI 修改前推荐：

```bash
git status
git switch -c backup/before-ai-change
```

或确保已有干净 Commit。

AI 修改后：

```bash
git diff
```

不符合预期时通过 Git 恢复。

---

## 63. AI 生成文件

AI 生成文件必须：

- 使用正确路径。
- 使用正确命名。
- 不覆盖未知文件。
- 明确替换关系。
- 检查编码。
- 检查换行。
- 检查内容完整性。

---

## 64. AI 生成文档包

批量生成文档前应先确认：

- 文档索引。
- 编号。
- 文件名。
- 状态。
- 模板。
- 依赖关系。

生成后必须逐篇检查，不应只确认文件存在。

---

## 65. AI 生成资源

AI 生成图片、音频、文本资源时必须记录：

- 使用工具。
- 生成日期。
- Prompt。
- 许可条件。
- 是否允许商用。
- 是否包含第三方风格风险。
- 是否经过人工验收。

---

## 66. 性能分析边界

AI 可以分析 Profiler 数据，但不能仅根据代码推断最终性能。

性能结论必须基于：

- Profiler。
- 真机。
- 目标场景。
- 可重复测试。

---

## 67. 安全分析边界

AI 可以辅助发现风险，但安全结论必须由具备责任的人员评审。

关键安全问题不能只依赖 AI。

---

## 68. 网络协议修改

AI 提议修改协议时必须输出：

- 旧格式。
- 新格式。
- 版本号。
- 兼容策略。
- 迁移策略。
- Server/Client 发布顺序。
- 回滚策略。
- 测试计划。

---

## 69. 存档修改

AI 提议修改存档时必须输出：

- 旧结构。
- 新结构。
- 迁移函数。
- 缺失字段处理。
- 无效数据处理。
- 回滚策略。
- 测试样本。

---

## 70. AI 文档可信度

AI 生成文档的状态默认应为：

```text
Draft
```

只有通过人工评审后才能改为：

```text
Approved
```

---

## 71. AI 输出审计

关键 AI 输出应可追踪到：

- 任务。
- 输入。
- 源码版本。
- 文档版本。
- 输出。
- Review。
- Commit。
- 测试结果。

---

## 72. AI Standard Review Checklist

### Governance

- [ ] AI 角色明确。
- [ ] 人工责任明确。
- [ ] 高风险操作需要确认。
- [ ] 项目事实来源明确。
- [ ] 输出状态明确。

### Context

- [ ] 使用当前源码。
- [ ] 使用当前文档。
- [ ] 固定 Commit。
- [ ] 任务范围明确。
- [ ] 禁止事项明确。

### Implementation

- [ ] 增量修改。
- [ ] 接口未漂移。
- [ ] 生命周期未漂移。
- [ ] 命名未漂移。
- [ ] 依赖未漂移。
- [ ] 第三方库未漂移。

### Verification

- [ ] 人工理解。
- [ ] 编译验证。
- [ ] 测试验证。
- [ ] Diff 检查。
- [ ] 文档同步。
- [ ] Review 通过。

### Security

- [ ] 无密钥。
- [ ] 无隐私数据。
- [ ] 无未经授权代码上传。
- [ ] 无危险命令自动执行。
- [ ] 无生产环境自动操作。

---

## 73. 验收标准

本规范执行后，应达到：

- AI 使用统一项目基线。
- 新会话可以通过文档快速恢复上下文。
- 不同 AI 工具不会随意改变已有接口。
- AI 输出明确区分事实、推断和未验证内容。
- 关键代码在合入前经过人工理解、编译、测试和 Review。
- 敏感信息不会被随意提供给外部 AI。
- 高风险操作不会由 AI 无人监督执行。
- AI 生成代码、测试、文档和 Git 信息符合 Project Aether 规范。
- 项目知识沉淀在源码和文档，而不是依赖单一聊天记录。
- AI 能够提高开发效率，同时不降低工程质量。

---

## 74. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-08-06 | 创建 Project Aether AI 协作规范正式初稿 |

---

# End
