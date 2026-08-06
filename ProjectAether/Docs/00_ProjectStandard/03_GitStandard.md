# Project Aether Git 规范

> **文件名：** `03_GitStandard.md`  
> **文档编号：** PAS-003  
> **版本：** v1.0  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **最后更新：** 2026-08-06  

---

## 1. 文档目的

本文档定义 Project Aether 的 Git 使用规范，用于统一分支管理、提交信息、代码合并、版本发布和文档变更流程。

本规范的目标是：

- 保证提交历史清晰、可追踪。
- 保证每次修改可以独立理解和回滚。
- 降低多人协作时的合并风险。
- 统一代码、文档、测试和工具的提交方式。
- 支持长期商业项目的版本维护。
- 为代码评审、问题定位和版本发布提供可靠依据。

---

## 2. 适用范围

本规范适用于以下内容：

- Unity Runtime 代码。
- Unity Editor 代码。
- 工具代码。
- 配置系统。
- 资源系统。
- 网络系统。
- 测试代码。
- 项目设置。
- Package 配置。
- 项目文档。
- 构建脚本。
- CI/CD 配置。

所有参与 Project Aether 开发的成员都必须遵循本文档。

---

## 3. 核心原则

### 3.1 每次提交只做一件事

一个 Commit 应表达一个明确目的。

正确示例：

```text
[Resource][Feature] Add resource reference counting
```

错误示例：

```text
修改资源系统、修复 UI、更新文档、调整配置
```

如果一次修改包含多个无关目标，必须拆分为多个 Commit。

---

### 3.2 提交必须可以独立理解

任何成员只查看 Commit 信息和 Diff，就应能够理解：

- 修改了什么。
- 为什么修改。
- 影响了哪个模块。
- 是否包含风险。
- 是否可以回滚。

禁止使用以下提交信息：

```text
update
fix
test
修改一下
临时提交
最终版本
```

---

### 3.3 提交必须尽量保持可编译

普通开发提交应满足：

- 项目可以编译。
- 不提交明显损坏的接口。
- 不提交未完成的重构中间状态。
- 不提交会阻塞其他成员的代码。

确实需要保存中间状态时，应使用个人临时分支，禁止直接进入共享分支。

---

### 3.4 代码和文档同步提交

以下变化必须在同一个功能分支或 Pull Request 中同步更新文档：

- 架构变化。
- 公共 API 变化。
- 生命周期变化。
- 模块职责变化。
- 配置格式变化。
- 资源流程变化。
- 网络协议变化。
- 测试验收规则变化。

---

### 3.5 Git 历史是项目变更记录

禁止通过反复覆盖文件、删除历史文档或使用含糊提交信息来隐藏变更过程。

重要变更必须能够通过 Git 历史追踪到：

- 提交人。
- 修改时间。
- 修改原因。
- 评审结论。
- 对应任务或 RFC。

---

## 4. 仓库管理范围

### 4.1 必须提交

Unity 项目通常必须提交：

```text
Assets/
Packages/
ProjectSettings/
Docs/
```

根据项目实际情况，还应提交：

```text
Tools/
BuildScripts/
CI/
README.md
CHANGELOG.md
.gitignore
.gitattributes
```

---

### 4.2 禁止提交

以下目录通常不得提交：

```text
Library/
Temp/
Logs/
Obj/
Build/
Builds/
UserSettings/
MemoryCaptures/
Recordings/
```

还应排除：

- IDE 本地缓存。
- 操作系统临时文件。
- 用户个人配置。
- 本地日志。
- 构建产物。
- 本地测试输出。
- 密钥。
- Token。
- 私人证书。
- 账号信息。

---

### 4.3 敏感信息

禁止提交：

- API Key。
- Access Token。
- 私钥。
- 服务端密码。
- 数据库连接密码。
- 云服务凭据。
- 签名证书密码。
- 个人账号信息。

发现敏感信息已经提交后，仅删除文件是不够的。

必须：

1. 立即撤销或更换泄露凭据。
2. 通知项目负责人。
3. 清理 Git 历史。
4. 检查远程仓库和构建日志。
5. 记录安全事件。

---

## 5. 分支模型

Project Aether 推荐使用以下长期分支：

```text
main
develop
```

短期分支：

```text
feature/*
fix/*
refactor/*
release/*
hotfix/*
docs/*
test/*
chore/*
```

---

## 6. 长期分支

### 6.1 main

`main` 保存已验证的稳定版本。

规则：

- 禁止直接开发。
- 禁止普通成员直接 Push。
- 只能通过 Pull Request 或受控合并更新。
- 每次正式发布必须在 `main` 创建 Tag。
- 合入前必须完成发布验证。
- `main` 上的版本应始终具备可发布能力。

---

### 6.2 develop

`develop` 是日常功能集成分支。

规则：

- 已完成的 Feature 分支合入 `develop`。
- 合入前必须通过基础编译和测试。
- 禁止直接进行长期功能开发。
- 禁止提交明显不完整的功能。
- 保持能够供团队继续开发。

---

## 7. 短期分支

### 7.1 Feature Branch

用于开发新功能。

命名格式：

```text
feature/<module>-<description>
```

示例：

```text
feature/resource-manager
feature/config-loader
feature/module-lifecycle
feature/combat-hit-system
```

Feature 分支通常从 `develop` 创建，并最终合回 `develop`。

---

### 7.2 Fix Branch

用于修复普通缺陷。

命名格式：

```text
fix/<module>-<description>
```

示例：

```text
fix/resource-release-count
fix/config-duplicate-key
fix/bootstrap-init-order
```

---

### 7.3 Refactor Branch

用于不改变外部行为的结构重构。

命名格式：

```text
refactor/<module>-<description>
```

示例：

```text
refactor/resource-handle-lifecycle
refactor/module-registration
```

重构分支必须明确说明：

- 外部行为是否保持不变。
- 公共接口是否变化。
- 是否需要同步更新测试。
- 是否影响性能。

---

### 7.4 Release Branch

用于版本发布准备。

命名格式：

```text
release/<version>
```

示例：

```text
release/0.1.0
release/1.0.0
```

Release 分支允许：

- 修复发布阻塞问题。
- 更新版本号。
- 更新 ChangeLog。
- 更新发布文档。
- 调整构建配置。

Release 分支禁止加入新的大型功能。

---

### 7.5 Hotfix Branch

用于修复已发布版本的紧急问题。

命名格式：

```text
hotfix/<version>-<description>
```

示例：

```text
hotfix/1.0.1-resource-crash
```

Hotfix 通常从 `main` 创建。

完成后必须合入：

- `main`。
- `develop`。
- 当前仍在维护的相关 Release 分支。

---

### 7.6 Docs Branch

用于独立文档工作。

命名格式：

```text
docs/<description>
```

示例：

```text
docs/project-standard
docs/resource-architecture
```

如果文档与代码修改属于同一功能，不必强制拆分为 Docs 分支。

---

### 7.7 Test Branch

用于新增或调整测试基础设施。

命名格式：

```text
test/<module>-<description>
```

示例：

```text
test/resource-lifecycle
test/module-init-order
```

---

## 8. 分支创建规则

创建分支前必须：

1. 切换到正确的基础分支。
2. 拉取远程最新代码。
3. 确认工作区干净。
4. 使用符合规范的分支名。

示例：

```bash
git switch develop
git pull --ff-only
git switch -c feature/resource-manager
```

禁止从长期未更新的本地分支直接创建新分支。

---

## 9. Commit 信息格式

Project Aether 统一使用：

```text
[模块][类型] 描述
```

示例：

```text
[Framework][Feature] Add module lifecycle management
[Resource][Fix] Prevent duplicate release
[Resource][Refactor] Simplify handle state transitions
[Docs][Update] Update resource architecture document
[Test][Add] Add resource lifecycle tests
```

此格式是 Project Aether 的项目标准。

禁止擅自替换为：

```text
feat(Resource):
fix(Resource):
```

除非未来通过正式 RFC 修改项目规范。

---

## 10. Commit 模块

第一个方括号表示修改范围或模块。

常见模块：

```text
[Framework]
[Bootstrap]
[Module]
[Resource]
[Config]
[Pool]
[Gameplay]
[Combat]
[Character]
[Network]
[UI]
[Editor]
[Tools]
[Build]
[Test]
[Docs]
[Project]
```

模块名称要求：

- 使用英文。
- 使用 PascalCase。
- 与项目目录和 asmdef 命名保持一致。
- 能明确表示主要影响范围。

一次提交影响多个模块时，应优先考虑拆分。

确实无法拆分时，可以使用最主要模块，或使用：

```text
[Project]
```

---

## 11. Commit 类型

第二个方括号表示修改类型。

### 11.1 Feature

新增功能或能力。

```text
[Resource][Feature] Add asynchronous asset loading
```

---

### 11.2 Fix

修复缺陷。

```text
[Resource][Fix] Prevent handle release below zero
```

---

### 11.3 Refactor

重构代码结构，但不改变预期外部行为。

```text
[Framework][Refactor] Extract module state validation
```

---

### 11.4 Update

更新已有内容、规则或实现。

```text
[Docs][Update] Update document naming rules
```

---

### 11.5 Add

新增文件、测试、文档或配置。

```text
[Test][Add] Add module initialization order tests
```

---

### 11.6 Remove

删除不再使用的内容。

```text
[Resource][Remove] Remove deprecated release interface
```

---

### 11.7 Optimize

性能优化。

```text
[Pool][Optimize] Reduce temporary allocations during release
```

---

### 11.8 Chore

工程维护，不直接改变业务功能。

```text
[Project][Chore] Update package lock file
```

---

### 11.9 Revert

回滚已有修改。

```text
[Resource][Revert] Revert asynchronous cache release
```

正文中必须说明被回滚的 Commit。

---

### 11.10 Docs Commit

纯文档新增推荐：

```text
[Docs][Add] Add Git standard
```

文档修改推荐：

```text
[Docs][Update] Update coding standard
```

文档删除推荐：

```text
[Docs][Remove] Remove deprecated resource design
```

---

## 12. Commit 描述规范

描述部分必须：

- 使用英文。
- 使用动词开头。
- 清楚说明修改结果。
- 不使用句号。
- 不超过合理长度。
- 避免模糊词。

推荐：

```text
Add resource handle state validation
Fix duplicate module registration
Update resource architecture document
```

不推荐：

```text
Changes
Update code
Fix bug
Modify resource
Try new solution
```

---

## 13. Commit 正文

简单提交可以只写标题。

复杂提交建议添加正文：

```text
[Resource][Refactor] Separate cache and handle responsibilities

- Move cache ownership out of ResourceHandle
- Keep reference counting inside ResourceManager
- Preserve existing public loading API

Reason:
The previous implementation mixed asset state and cache ownership.
```

正文应说明：

- 修改原因。
- 主要实现。
- 风险。
- 兼容性。
- 测试方式。

---

## 14. Commit Footer

需要关联任务、RFC 或问题时，可在正文末尾添加：

```text
Task: PA-102
RFC: RFC-Resource-003
Issue: #128
```

破坏性变更应明确标记：

```text
Breaking Change:
ResourceManager.Release(string key) has been removed.
Use ResourceHandle.Dispose() instead.
```

---

## 15. Commit 拆分规则

应拆分的情况：

- 功能修改和无关格式化同时发生。
- 多个模块可以独立提交。
- 重构和功能修改可以分开。
- 代码和大规模资源文件无直接关系。
- 测试基础设施与业务功能无直接关系。

可以放在同一 Commit 的情况：

- 功能代码及其直接测试。
- API 修改及相关调用方调整。
- 代码修改及对应文档更新。
- Bug 修复及回归测试。

---

## 16. 禁止提交的内容

禁止提交：

- 注释掉的大段废弃代码。
- 临时调试日志。
- 本地路径。
- 无关格式化。
- IDE 自动生成缓存。
- 未说明来源的大型二进制文件。
- 无法确认授权的第三方资源。
- 未完成且会破坏编译的接口。
- 密钥和账号信息。

调试代码示例：

```csharp
Debug.Log("here");
Debug.Log("test");
```

在正式提交前必须清理或替换为符合日志规范的内容。

---

## 17. 提交前检查

每次 Commit 前必须确认：

- [ ] 当前分支正确。
- [ ] 修改内容属于同一目的。
- [ ] 没有提交敏感信息。
- [ ] 没有提交本地缓存。
- [ ] 没有无关文件。
- [ ] 代码可以编译。
- [ ] 基础测试通过。
- [ ] 调试代码已清理。
- [ ] 文档已经同步。
- [ ] Commit 信息符合规范。

推荐先执行：

```bash
git status
git diff
git diff --staged
```

---

## 18. 暂存规则

禁止无检查地执行：

```bash
git add .
```

在大型修改中应优先按文件或区块暂存：

```bash
git add Assets/ProjectAether/Framework/
git add Docs/01_Architecture/Framework/
git add -p
```

使用 `git add -p` 可以避免将无关修改放进同一个 Commit。

---

## 19. Pull Request 规范

Pull Request 必须包含：

```markdown
## Summary

说明本次修改解决了什么问题。

## Changes

列出主要修改。

## Reason

说明为什么需要修改。

## Test

说明测试方式和结果。

## Impact

说明影响范围。

## Risk

说明潜在风险。

## Related Documents

列出相关 Architecture、Design、RFC 或 Test 文档。
```

---

## 20. Pull Request 标题

Pull Request 标题建议与主要 Commit 格式一致：

```text
[Resource][Feature] Add resource lifecycle management
```

一个 PR 包含多个 Commit 时，标题应描述整体目标。

---

## 21. Pull Request 提交条件

创建 PR 前必须确认：

- [ ] 功能已经完成。
- [ ] 代码可以编译。
- [ ] 自动化测试通过。
- [ ] 手动验证完成。
- [ ] 文档同步完成。
- [ ] 不包含无关修改。
- [ ] 不包含敏感信息。
- [ ] 提交历史清晰。
- [ ] 已处理已知冲突。
- [ ] PR 描述完整。

---

## 22. Review 处理规则

收到 Review 意见后：

- 必须明确回应。
- 必须说明是否采纳。
- 不采纳时必须说明原因。
- 修改后应通知评审人重新检查。
- 不允许无说明地标记为已解决。

Review 中发现的独立问题，可以：

- 在当前分支修复。
- 创建后续任务。
- 创建 RFC。
- 记录到 Decision Log。

---

## 23. 合并策略

Project Aether 推荐按场景使用以下策略。

### 23.1 Squash Merge

适合：

- 小型 Feature。
- 修复分支。
- 提交历史较碎。
- 中间 Commit 没有长期保留价值。

Squash 后的 Commit 信息必须重新整理为符合规范的正式信息。

---

### 23.2 Merge Commit

适合：

- 大型功能。
- 分支历史本身有保留价值。
- Release 分支。
- Hotfix 回合并。

---

### 23.3 Rebase

适合：

- 合入前同步最新 `develop`。
- 整理个人分支历史。
- 尚未被其他成员依赖的分支。

禁止随意 Rebase 已被多人共同使用的远程分支。

---

## 24. 冲突处理

发生冲突时必须：

1. 理解双方修改目的。
2. 查看相关文档。
3. 确认当前架构基线。
4. 手动解决冲突。
5. 重新编译。
6. 重新测试。
7. 检查是否需要更新文档。

禁止：

- 无理解地选择 Accept Current。
- 无理解地选择 Accept Incoming。
- 通过删除一侧代码快速消除冲突。
- 解决冲突后不测试。

---

## 25. 推送规则

首次推送新分支：

```bash
git push -u origin feature/resource-manager
```

后续：

```bash
git push
```

禁止对共享分支使用普通强制推送：

```bash
git push --force
```

确实需要整理个人分支时，优先使用：

```bash
git push --force-with-lease
```

但必须确保该分支没有被其他成员使用。

---

## 26. Tag 规范

正式版本使用语义化版本：

```text
vMajor.Minor.Patch
```

示例：

```text
v0.1.0
v1.0.0
v1.1.0
v1.1.1
```

规则：

- Major：不兼容的重大变化。
- Minor：向后兼容的新功能。
- Patch：向后兼容的问题修复。

---

## 27. Tag 创建

推荐使用附注标签：

```bash
git tag -a v1.0.0 -m "Project Aether v1.0.0"
git push origin v1.0.0
```

禁止在未验证提交上创建正式版本 Tag。

---

## 28. Release 流程

推荐流程：

```text
Feature Complete
  ↓
Merge to develop
  ↓
Create release branch
  ↓
Version Update
  ↓
Regression Test
  ↓
Fix Release Blockers
  ↓
Update ChangeLog
  ↓
Merge to main
  ↓
Create Tag
  ↓
Merge Back to develop
```

---

## 29. ChangeLog 规范

正式版本应维护 `CHANGELOG.md`。

每个版本至少包含：

```markdown
## [1.0.0] - 2026-08-06

### Added

### Changed

### Fixed

### Removed

### Known Issues
```

ChangeLog 面向版本使用者，不应简单复制所有 Commit。

---

## 30. Hotfix 流程

紧急修复流程：

```text
Create hotfix from main
  ↓
Fix
  ↓
Test
  ↓
Review
  ↓
Merge to main
  ↓
Create patch tag
  ↓
Merge to develop
```

Hotfix 必须包含：

- 问题原因。
- 影响范围。
- 修复方式。
- 回归测试。
- 是否需要补充长期改进。

---

## 31. 大文件管理

大型二进制资源应评估是否使用 Git LFS。

适合 Git LFS 的内容：

- 大型 PSD。
- 高分辨率源贴图。
- 音频源文件。
- 视频文件。
- 大型模型文件。
- 无法有效 Diff 的二进制文件。

禁止未经评估直接向普通 Git 历史提交超大文件。

---

## 32. Unity YAML 合并

Unity 项目应保持：

- Asset Serialization Mode：Force Text。
- Version Control Mode：Visible Meta Files。

目的：

- 保证 `.meta` 文件可追踪。
- 提高场景和 Prefab 的可合并性。
- 降低 GUID 丢失风险。

所有资源文件和对应 `.meta` 必须一起提交。

---

## 33. 场景和 Prefab 协作

大型场景和 Prefab 应尽量避免多人同时修改。

推荐：

- 拆分子场景。
- 拆分 Prefab。
- 使用嵌套 Prefab。
- 明确资源负责人。
- 提交前同步远程分支。

冲突无法可靠解决时，应由熟悉双方修改的成员统一处理。

---

## 34. 第三方依赖

新增第三方依赖必须记录：

- 名称。
- 版本。
- 来源。
- License。
- 引入原因。
- 更新方式。
- 风险。
- 替代方案。

对应 Commit 示例：

```text
[Project][Add] Add UniTask package
```

涉及核心架构时，还必须创建 RFC 或 Decision Log。

---

## 35. 回滚规范

普通回滚优先使用：

```bash
git revert <commit>
```

避免在共享分支通过 `reset` 改写历史。

回滚 Commit 示例：

```text
[Resource][Revert] Revert deferred cache release
```

正文必须说明：

- 回滚哪个 Commit。
- 为什么回滚。
- 是否需要后续修复。
- 是否影响数据或兼容性。

---

## 36. Cherry-pick 规范

Cherry-pick 仅用于明确的跨分支补丁，例如：

- Hotfix 同步。
- Release 分支补丁。
- 特定修复迁移。

执行后必须：

- 检查冲突。
- 重新测试。
- 确认没有遗漏依赖 Commit。

禁止用 Cherry-pick 替代正常分支合并流程。

---

## 37. Git 操作安全

执行以下高风险命令前必须明确影响：

```bash
git reset --hard
git clean -fd
git rebase
git push --force
git filter-repo
```

执行前应：

- 确认当前分支。
- 确认工作区状态。
- 创建备份分支。
- 确认远程影响。
- 与相关成员沟通。

---

## 38. AI 使用 Git 的规则

AI 可以协助：

- 生成 Commit 信息。
- 分析 Diff。
- 建议拆分 Commit。
- 编写 PR 描述。
- 检查提交是否符合规范。
- 生成 ChangeLog 草稿。

AI 不应在未确认的情况下：

- 强制推送。
- 删除分支。
- 重写共享历史。
- 自动合并冲突。
- 提交敏感信息。
- 执行破坏性 Git 命令。

使用 AI 生成 Commit 信息时，必须由开发者检查其是否准确反映实际 Diff。

---

## 39. 常用 Commit 示例

### Framework

```text
[Framework][Feature] Add service registration lifecycle
[Framework][Fix] Prevent duplicate module registration
[Framework][Refactor] Separate bootstrap and module initialization
```

### Resource

```text
[Resource][Feature] Add asynchronous resource loading
[Resource][Fix] Prevent reference count underflow
[Resource][Refactor] Move cache ownership to ResourceManager
[Resource][Optimize] Reduce temporary allocations during loading
```

### Config

```text
[Config][Feature] Add typed config row loading
[Config][Fix] Reject duplicate config keys
[Config][Update] Update config import pipeline
```

### Pool

```text
[Pool][Feature] Add object pool capacity control
[Pool][Fix] Prevent duplicate object release
[Pool][Optimize] Reduce collection allocations
```

### Documentation

```text
[Docs][Add] Add Git standard
[Docs][Update] Update resource architecture document
[Docs][Remove] Remove deprecated module design
```

### Test

```text
[Test][Add] Add resource handle lifecycle tests
[Test][Update] Extend module shutdown test coverage
```

---

## 40. Git Review Checklist

### Commit

- [ ] Commit 只包含一个明确目的。
- [ ] Commit 格式为 `[模块][类型] 描述`。
- [ ] 模块名称正确。
- [ ] 类型正确。
- [ ] 描述清晰。
- [ ] 没有无关文件。
- [ ] 没有敏感信息。
- [ ] 没有临时调试代码。
- [ ] 代码可以编译。
- [ ] 测试已执行。
- [ ] 文档已同步。

### Pull Request

- [ ] PR 标题准确。
- [ ] PR 描述完整。
- [ ] 影响范围清晰。
- [ ] 风险已说明。
- [ ] 测试结果已记录。
- [ ] 相关文档已关联。
- [ ] 已解决冲突。
- [ ] Review 意见已处理。
- [ ] 合并策略正确。

### Release

- [ ] Release 分支来源正确。
- [ ] 版本号正确。
- [ ] ChangeLog 已更新。
- [ ] 回归测试通过。
- [ ] 已知问题已记录。
- [ ] `main` 合并完成。
- [ ] Tag 已创建。
- [ ] 修改已同步回 `develop`。

---

## 41. 验收标准

本规范执行后，应达到：

- 每次修改都可以追踪到明确目的。
- Commit 历史可以用于定位问题。
- 分支职责清晰。
- 代码和文档保持同步。
- Release 和 Hotfix 流程可重复执行。
- 敏感信息不会进入仓库。
- Unity 资源冲突风险得到控制。
- AI 工具能够按照统一规范辅助 Git 工作。
- 新成员可以通过 Git 历史理解项目演进。

---

## 42. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-08-06 | 创建 Project Aether Git 规范正式初稿 |

---

# End
