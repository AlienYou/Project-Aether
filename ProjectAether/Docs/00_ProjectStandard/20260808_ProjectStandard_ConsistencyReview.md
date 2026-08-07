# Project Aether Project Standard 一致性评审

> **文件名：** `20260808_ProjectStandard_ConsistencyReview.md`  
> **Review ID：** REV-PS-001  
> **版本：** v1.0  
> **状态：** Changes Requested  
> **评审对象：** `Docs/00_ProjectStandard/00～09`  
> **评审日期：** 2026-08-08  

---

## 1. 评审目标

对 Project Aether `00_ProjectStandard` 文档包进行第一次整体一致性评审，检查：

- 文件编号与顺序。
- 文档状态。
- 目录结构。
- 技术栈。
- Git 规则。
- 测试目录。
- AI 工作流。
- Workflow 与 Milestone 的衔接。
- 文档之间是否存在互相冲突的规则。

---

## 2. 评审范围

本次评审包含：

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

---

## 3. 总体结论

**结论：Changes Requested**

`02～09` 已基本形成统一的工程规范体系，但 `00` 与 `01` 创建时间较早，仍保留旧版目录与文档体系描述。

当前不建议将整套 Project Standard 从 `Draft` 修改为 `Approved`。

必须先解决下列 Major 问题，再进行第二轮 Review。

---

## 4. Findings

| ID | Level | Document | Finding | Resolution |
|---|---|---|---|---|
| PSR-001 | Major | `01_DirectoryStandard.md` | 仍使用 `Assets/Framework/`、`Assets/Game/` 的旧目录结构，与后续规范中的 `Assets/ProjectAether/...` 结构不一致 | 下一步修订 `01_DirectoryStandard.md` |
| PSR-002 | Major | `01_DirectoryStandard.md` / `06_TestStandard.md` | `01` 使用根目录 `Tests/`，`06` 使用 `Assets/ProjectAether/Tests/` | 统一为 Unity 工程内 `Assets/ProjectAether/Tests/`，外部测试产物另设目录 |
| PSR-003 | Major | `00_ProjectStandard_Index.md` | 文档优先级包含 `Implementation Document`，但正式 Docs 分类中不存在该类别 | 新索引移除此类别，并使用“规范/架构/设计/源码/测试/聊天”的冲突处理规则 |
| PSR-004 | Minor | `00` / `01` | 文档头部仍使用旧英文元信息格式，与 `02～09` 不一致 | 后续修订时统一元信息 |
| PSR-005 | Minor | `00` / `01` | 更新时间仍为 `2026-07-28`，没有体现规范包完成后的整体评审 | 更新索引；`01` 修订时更新 |
| PSR-006 | Minor | 工具输出 | 存在 `04_CodingStandard_v1.0.md` 临时重复文件 | 不纳入工程，仅保留正式 `04_CodingStandard.md` |
| PSR-007 | Minor | `09_ProjectMilestone.md` | 提议 `Docs/02_Design/ProjectPlan/`；属于子目录扩展，可接受，但实际创建时必须同步目录规范 | 在创建该目录时更新 `01_DirectoryStandard.md` |
| PSR-008 | Pass | `03_GitStandard.md` | Git Commit 已统一为 `[模块][类型] 描述` | 保持 |
| PSR-009 | Pass | `04～07` | Unity 2022.3.51f1c1、UniTask、VContainer、Addressables、FishNet 约束基本一致 | 保持 |
| PSR-010 | Pass | `08` / `09` | Workflow 与 Milestone 的 Ready、Review、Test、Exit Gate 可以衔接 | 保持 |

---

## 5. 推荐统一目录基线

下一轮修订 `01_DirectoryStandard.md` 时，推荐以以下结构作为正式基线：

```text
ProjectAether/
├── Assets/
│   └── ProjectAether/
│       ├── Framework/
│       ├── Gameplay/
│       ├── Presentation/
│       ├── Editor/
│       └── Tests/
├── Packages/
├── ProjectSettings/
├── Docs/
├── Tools/
├── BuildScripts/
├── README.md
├── .gitignore
└── .gitattributes
```

第三方依赖优先通过 Unity Package Manager 管理。

只有无法通过 Package 管理的第三方资产，才根据其形式放入受控的第三方目录。

---

## 6. 正式文档包

正式 `00_ProjectStandard` 文档包仅包含：

```text
00_ProjectStandard_Index.md
01_DirectoryStandard.md
02_DocumentStandard.md
03_GitStandard.md
04_CodingStandard.md
05_ReviewStandard.md
06_TestStandard.md
07_AIStandard.md
08_ProjectWorkflow.md
09_ProjectMilestone.md
```

以下文件不属于正式规范包：

```text
04_CodingStandard_v1.0.md
02_DocumentStandard.docx
```

它们是生成过程中的临时或中间产物，不应复制到正式工程。

---

## 7. 状态建议

当前状态：

```text
00～09: Draft
```

完成以下步骤后再统一调整为 `Approved`：

1. 修订 `01_DirectoryStandard.md`。
2. 检查 `00～09` 内所有路径引用。
3. 检查所有文档相对链接。
4. 更新 `00_ProjectStandard_Index.md`。
5. 执行第二轮一致性 Review。
6. 解决所有 Major / Blocker。
7. 将 `00～09` 状态统一修改为 `Approved`。
8. 提交 Git。

---

## 8. 下一步

下一步进入：

```text
修订 01_DirectoryStandard.md
```

修订目标：

- 统一 `Assets/ProjectAether/` 工程根目录。
- 统一 Framework / Gameplay / Presentation 分层。
- 统一 Runtime / Editor / Tests 组织方式。
- 统一 asmdef 目录规则。
- 统一第三方 Package 管理方式。
- 统一 BuildScripts 与构建输出职责。
- 保证与 `04_CodingStandard.md`、`06_TestStandard.md`、`09_ProjectMilestone.md` 一致。

---

## 9. Review Decision

**Changes Requested**

当前 Project Standard 文档包结构已经建立完成，但仍需完成一次目录规范修订后才能进入正式批准阶段。

---

# End
