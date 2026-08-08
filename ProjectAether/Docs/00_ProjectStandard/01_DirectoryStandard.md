# Project Aether 工程目录规范

> **文件名：** `01_DirectoryStandard.md`  
> **文档编号：** PAS-001  
> **版本：** v1.1  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **Unity 版本：** Unity 2022.3.51f1c1  
> **最后更新：** 2026-08-08  

---

## 1. 文档目的

本文档定义 Project Aether 的统一工程目录、模块分层、Assembly Definition、测试目录、第三方依赖、文档目录、构建脚本和生成产物管理规范。

本规范的目标是：

- 统一所有成员对项目目录结构的理解。
- 让模块职责可以从目录位置直接判断。
- 保证 Runtime、Editor、Tests、Tools、Build Pipeline 边界清晰。
- 保证 Framework、Gameplay、Presentation 依赖方向稳定。
- 避免第三方代码、生成文件和项目源码混杂。
- 为 asmdef、Namespace、文档和测试建立一致映射。
- 为 ChatGPT、Codex、Claude Code、Gemini 等 AI 工具提供稳定、可读的仓库结构。
- 支持 Project Aether 从当前基础框架阶段扩展到长期商业项目。

---

## 2. 本版本修订说明

`v1.1` 用于修正早期目录规范与后续 Project Standard 文档之间的不一致。

本次统一：

- 项目业务源码统一放置于 `Assets/ProjectAether/`。
- 使用 `Framework / Gameplay / Presentation` 作为一级 Runtime 分层。
- 测试统一放置于 `Assets/ProjectAether/Tests/`。
- 第三方 UPM Package 优先通过 `Packages/` 管理。
- 非 UPM 第三方资产统一进入 `Assets/ThirdParty/`。
- 构建脚本统一进入仓库根目录 `BuildScripts/`。
- Build 输出目录与 Build Script 源码目录明确分离。
- `Docs/` 目录与 `02_DocumentStandard.md` 保持一致。
- Runtime 与 Editor 通过 asmdef 隔离。
- Namespace、目录和 asmdef 必须保持逻辑一致。

本版本完成后，将作为 Project Aether 后续所有模块新增目录的基线。

---

## 3. 核心原则

### 3.1 Project-Owned Code Must Be Obvious

所有 Project Aether 自有 Runtime / Editor / Tests 代码必须能够从路径中明确识别。

统一根目录：

```text
Assets/ProjectAether/
```

禁止将项目源码散落在：

```text
Assets/Scripts/
Assets/Game/
Assets/Framework/
Assets/Common/
Assets/Code/
```

这些旧式模糊目录不作为 Project Aether 正式结构使用。

---

### 3.2 Third-Party Code Must Be Isolated

第三方内容不得与项目源码混合。

优先：

```text
Packages/
```

只有无法通过 UPM 管理的第三方 Unity Asset 才进入：

```text
Assets/ThirdParty/
```

禁止：

```text
Assets/ProjectAether/Framework/SomeVendorPlugin/
```

除非该代码已经被正式 fork，并明确成为 Project Aether 自维护代码。

---

### 3.3 Directory Reflects Architecture

目录不是简单的文件分类。

目录必须表达：

- 层级。
- 模块职责。
- 依赖方向。
- Runtime / Editor 边界。
- 测试范围。
- 第三方边界。

---

### 3.4 Runtime and Editor Must Be Separated

Runtime 代码不得依赖 `UnityEditor`。

Editor 代码必须放入：

```text
Assets/ProjectAether/Editor/
```

或模块内部受 asmdef 控制的 `Editor/` 子目录。

---

### 3.5 Tests Are First-Class Project Code

测试不是临时脚本。

正式测试代码统一放置：

```text
Assets/ProjectAether/Tests/
```

禁止在仓库根目录维护第二套独立测试源码目录。

---

### 3.6 Generated Files Must Be Isolated

生成文件必须与手写源码分离。

任何自动生成内容都必须：

- 明确来源。
- 明确是否提交 Git。
- 明确重新生成方式。
- 禁止人工直接编辑，除非规范明确允许。

---

### 3.7 No Catch-All Folders

禁止长期使用：

```text
Misc/
Temp/
Others/
Common/
Utils/
NewFolder/
Backup/
```

作为无法分类内容的收容目录。

如果一个文件无法判断应该放在哪里，应先确认其职责，而不是创建模糊目录。

---

## 4. 仓库根目录

Project Aether 推荐仓库根结构：

```text
ProjectAether/
├── Assets/
├── Packages/
├── ProjectSettings/
├── Docs/
├── Tools/
├── BuildScripts/
├── Builds/
├── README.md
├── CHANGELOG.md
├── .gitignore
└── .gitattributes
```

其中：

| 目录 / 文件 | 职责 |
|---|---|
| `Assets/` | Unity Asset Database 内容 |
| `Packages/` | UPM Package 配置和本地 Package |
| `ProjectSettings/` | Unity 项目配置 |
| `Docs/` | 项目长期知识库 |
| `Tools/` | Unity 外部开发工具和辅助脚本 |
| `BuildScripts/` | 构建、CI、发布脚本 |
| `Builds/` | 本地构建输出，通常不提交 |
| `README.md` | 项目入口 |
| `CHANGELOG.md` | 正式版本变更记录 |
| `.gitignore` | Git 忽略规则 |
| `.gitattributes` | Git 文本、LFS、Merge 等规则 |

---

## 5. Assets 根目录

推荐：

```text
Assets/
├── ProjectAether/
├── ThirdParty/
└── StreamingAssets/
```

根据实际需要还可能出现 Unity 或第三方工具要求的固定目录，但必须避免业务源码直接散落在 `Assets/` 根目录。

---

## 6. Assets/ProjectAether 总体结构

正式项目源码根目录：

```text
Assets/
└── ProjectAether/
    ├── Framework/
    ├── Gameplay/
    ├── Presentation/
    ├── Content/
    ├── Editor/
    ├── Tests/
    └── ProjectAether.asmdef
```

`ProjectAether.asmdef` 是否保留取决于实际依赖方案。

如果各一级模块都已独立 asmdef，可以不建立一个覆盖全部代码的根 asmdef。

禁止为了“方便引用”建立一个所有模块都依赖的万能根程序集。

---

## 7. 一级分层

### 7.1 Framework

路径：

```text
Assets/ProjectAether/Framework/
```

职责：

- 通用基础设施。
- 生命周期。
- 资源。
- 配置。
- Pool。
- Logging。
- 时间。
- 服务注册。
- 网络基础设施。
- 文件和平台适配。
- 不包含具体游戏规则。

Framework 不得依赖 Gameplay 或 Presentation。

---

### 7.2 Gameplay

路径：

```text
Assets/ProjectAether/Gameplay/
```

职责：

- 游戏规则。
- Character。
- Combat。
- Ability。
- AI。
- Interaction。
- Quest。
- World gameplay rules。
- 与具体玩法相关的状态和业务逻辑。

Gameplay 可以依赖 Framework。

Gameplay 不应依赖具体 UI 实现。

---

### 7.3 Presentation

路径：

```text
Assets/ProjectAether/Presentation/
```

职责：

- UI。
- HUD。
- Camera。
- VFX 表现。
- Audio 表现。
- 动画表现桥接。
- View。
- Input presentation integration。

Presentation 可以依赖 Gameplay 和 Framework。

---

### 7.4 Content

路径：

```text
Assets/ProjectAether/Content/
```

职责：

- Project Aether 自有内容资源。
- Prefab。
- Scene。
- Material。
- Texture。
- Model。
- Animation。
- Audio。
- VFX。
- ScriptableObject 数据资源。

Content 不作为 C# 业务模块层级。

---

### 7.5 Editor

路径：

```text
Assets/ProjectAether/Editor/
```

职责：

- 项目级 Editor 工具。
- Import Processor。
- Custom Window。
- Build Menu。
- Validation Tool。
- Debug Tool。
- Asset Processing。

模块专属 Editor 工具可以放在模块内部的 `Editor/` 子目录中。

---

### 7.6 Tests

路径：

```text
Assets/ProjectAether/Tests/
```

职责：

- EditMode。
- PlayMode。
- Integration。
- Performance。
- Network。
- Module-specific tests。
- Test data。

详细规范见 `06_TestStandard.md`。

---

## 8. Framework 推荐结构

推荐：

```text
Assets/ProjectAether/Framework/
├── Bootstrap/
├── Module/
├── Service/
├── Logging/
├── Resource/
├── Config/
├── Pool/
├── Time/
├── Networking/
├── Platform/
├── Serialization/
├── Diagnostics/
└── Common/
```

注意：

`Common/` 只允许存放经过确认的真正跨模块基础类型。

禁止将无法分类的代码随意放入 `Common/`。

---

## 9. Framework/Bootstrap

推荐：

```text
Framework/
└── Bootstrap/
    ├── Runtime/
    │   ├── Bootstrap.cs
    │   └── BootstrapRunner.cs
    ├── Editor/
    ├── Tests/
    └── ProjectAether.Framework.Bootstrap.asmdef
```

职责：

- 游戏启动入口。
- 全局初始化流程。
- Application Quit 桥接。
- 根生命周期调度。

不负责：

- 资源实现。
- 配置实现。
- 战斗规则。
- UI 初始化细节。

---

## 10. Framework/Module

推荐：

```text
Framework/
└── Module/
    ├── Runtime/
    │   ├── IGameModule.cs
    │   ├── ModuleManager.cs
    │   └── ModuleState.cs
    ├── Tests/
    └── ProjectAether.Framework.Module.asmdef
```

职责：

- Module 注册。
- Create。
- Initialize。
- Update。
- Shutdown。
- Module 状态管理。
- 生命周期顺序。

---

## 11. Framework/Service

推荐：

```text
Framework/
└── Service/
    ├── Runtime/
    ├── Tests/
    └── ProjectAether.Framework.Service.asmdef
```

用于：

- 服务注册。
- 服务查询。
- 生命周期明确的基础服务。

不得演变为：

- 任意全局对象仓库。
- 隐式依赖入口。
- 绕过 VContainer 的万能 Locator。

如果 ServiceLocator 被保留，应明确使用边界。

---

## 12. Framework/Logging

推荐：

```text
Framework/
└── Logging/
    ├── Runtime/
    ├── Editor/
    ├── Tests/
    └── ProjectAether.Framework.Logging.asmdef
```

职责：

- 日志接口。
- 日志级别。
- Unity Logger Adapter。
- Release Filter。
- Diagnostics integration。

---

## 13. Framework/Resource

推荐：

```text
Framework/
└── Resource/
    ├── Runtime/
    │   ├── Manager/
    │   ├── Handle/
    │   ├── Cache/
    │   ├── Request/
    │   ├── Loader/
    │   └── Addressables/
    ├── Editor/
    ├── Tests/
    └── ProjectAether.Framework.Resource.asmdef
```

当前核心类型例如：

```text
ResourceManager
ResourceHandle<T>
ResourceHandleBase
ResourceState
AssetKey
```

模块内部目录应围绕职责划分，而不是围绕文件数量划分。

---

## 14. Framework/Config

推荐：

```text
Framework/
└── Config/
    ├── Runtime/
    │   ├── Manager/
    │   ├── Loader/
    │   ├── Schema/
    │   └── Validation/
    ├── Editor/
    │   ├── Importer/
    │   └── Validator/
    ├── Tests/
    └── ProjectAether.Framework.Config.asmdef
```

当前核心概念例如：

```text
IConfigRow
ConfigManager
ConfigLoader
```

---

## 15. Framework/Pool

推荐：

```text
Framework/
└── Pool/
    ├── Runtime/
    ├── Tests/
    └── ProjectAether.Framework.Pool.asmdef
```

职责：

- Pool 创建。
- Get。
- Release。
- Reset。
- Clear。
- Capacity。
- Shutdown。

---

## 16. Framework/Networking

推荐：

```text
Framework/
└── Networking/
    ├── Runtime/
    │   ├── Core/
    │   ├── Transport/
    │   ├── Serialization/
    │   ├── Session/
    │   └── FishNet/
    ├── Tests/
    └── ProjectAether.Framework.Networking.asmdef
```

说明：

FishNet 是底层网络技术，但业务模块不应直接散布底层框架依赖。

推荐通过 Project Aether 网络层封装稳定接口。

---

## 17. Gameplay 推荐结构

推荐：

```text
Assets/ProjectAether/Gameplay/
├── Character/
├── Combat/
├── Ability/
├── AI/
├── Interaction/
├── Quest/
├── Inventory/
├── Progression/
└── World/
```

这些目录应根据真实需求逐步创建。

禁止一次性创建大量空目录。

---

## 18. Gameplay/Character

推荐：

```text
Gameplay/
└── Character/
    ├── Runtime/
    │   ├── Core/
    │   ├── Movement/
    │   ├── State/
    │   └── Network/
    ├── Tests/
    └── ProjectAether.Gameplay.Character.asmdef
```

Character 负责角色领域逻辑。

具体输入 UI、HUD、特效不应进入 Character Runtime 核心。

---

## 19. Gameplay/Combat

推荐：

```text
Gameplay/
└── Combat/
    ├── Runtime/
    │   ├── Core/
    │   ├── Damage/
    │   ├── Hit/
    │   ├── State/
    │   └── Network/
    ├── Tests/
    └── ProjectAether.Gameplay.Combat.asmdef
```

职责：

- 攻击规则。
- 伤害。
- 命中。
- 受击。
- 死亡。
- 战斗状态。
- 权威战斗逻辑。

---

## 20. Presentation 推荐结构

推荐：

```text
Assets/ProjectAether/Presentation/
├── UI/
├── Camera/
├── Input/
├── Audio/
├── Animation/
└── VFX/
```

---

## 21. Presentation/UI

推荐：

```text
Presentation/
└── UI/
    ├── Runtime/
    │   ├── Core/
    │   ├── Screens/
    │   ├── Widgets/
    │   └── Binding/
    ├── Editor/
    ├── Tests/
    └── ProjectAether.Presentation.UI.asmdef
```

UI 不应拥有 Gameplay 核心状态。

UI 负责：

- 展示。
- 输入转发。
- 用户反馈。
- View 生命周期。

---

## 22. Presentation/Camera

推荐：

```text
Presentation/
└── Camera/
    ├── Runtime/
    ├── Tests/
    └── ProjectAether.Presentation.Camera.asmdef
```

职责：

- Camera Rig。
- Follow。
- Aim。
- Shake。
- Presentation-only camera logic。

---

## 23. Content 目录

推荐：

```text
Assets/ProjectAether/Content/
├── Scenes/
├── Prefabs/
├── Materials/
├── Textures/
├── Models/
├── Animations/
├── Audio/
├── VFX/
├── Fonts/
└── DataAssets/
```

---

## 24. Content/Scenes

推荐：

```text
Content/
└── Scenes/
    ├── Bootstrap/
    ├── Gameplay/
    ├── Test/
    └── Development/
```

规则：

- 正式场景与测试场景分离。
- 临时实验场景不得进入正式 Build。
- 场景命名必须明确用途。

---

## 25. Content/Prefabs

推荐：

```text
Content/
└── Prefabs/
    ├── Characters/
    ├── Enemies/
    ├── Environment/
    ├── UI/
    ├── VFX/
    └── Gameplay/
```

Prefab 分类按业务领域，而不是按制作人员分类。

---

## 26. Content/DataAssets

用于 ScriptableObject 数据资产。

推荐：

```text
Content/
└── DataAssets/
    ├── Gameplay/
    ├── Presentation/
    └── Development/
```

不得将运行时玩家状态存入 ScriptableObject 资产。

---

## 27. Tests 总体结构

与 `06_TestStandard.md` 对齐：

```text
Assets/ProjectAether/Tests/
├── EditMode/
├── PlayMode/
├── Integration/
├── Performance/
├── Network/
├── Resource/
├── Config/
└── TestData/
```

---

## 28. 测试位置策略

Project Aether 支持两种测试组织方式。

### 28.1 集中测试

统一放置：

```text
Assets/ProjectAether/Tests/
```

适合：

- 跨模块测试。
- 系统测试。
- 集成测试。
- PlayMode。
- Performance。
- Network。

### 28.2 模块邻近测试

模块内部：

```text
Framework/Resource/Tests/
Gameplay/Combat/Tests/
```

适合：

- 模块私有单元测试。
- 与模块实现强绑定的 EditMode 测试。

两种方式可以共存，但团队必须保持职责一致。

---

## 29. 测试 asmdef

示例：

```text
ProjectAether.Tests.EditMode
ProjectAether.Tests.PlayMode
ProjectAether.Tests.Integration
ProjectAether.Tests.Performance
```

模块局部测试：

```text
ProjectAether.Framework.Resource.Tests
ProjectAether.Gameplay.Combat.Tests
```

Runtime asmdef 禁止引用 Tests asmdef。

---

## 30. Editor 总体结构

项目级 Editor：

```text
Assets/ProjectAether/Editor/
├── Validation/
├── Build/
├── Debug/
├── Import/
└── Windows/
```

模块专属 Editor：

```text
Framework/Config/Editor/
Framework/Resource/Editor/
Presentation/UI/Editor/
```

原则：

- 项目通用 Editor 工具放根 `Editor/`。
- 模块专属工具放模块内部 `Editor/`。
- 业务 Runtime 不引用 Editor。

---

## 31. ThirdParty

非 UPM 第三方资产统一：

```text
Assets/ThirdParty/
```

示例：

```text
Assets/ThirdParty/
├── VendorA/
├── VendorB/
└── LegacyPlugin/
```

规则：

- 保持原始目录结构优先。
- 避免直接修改 Vendor 源码。
- 必须修改时记录 patch 或 fork。
- 不在 ThirdParty 中新增 Project Aether 业务逻辑。

---

## 32. Packages

UPM 依赖通过：

```text
Packages/manifest.json
Packages/packages-lock.json
```

管理。

例如：

- UniTask。
- Addressables。
- VContainer。
- FishNet（根据实际接入方式）。
- Unity 官方 Package。

如果第三方库通过 Git URL 或本地 Package 接入，应保持版本可追踪。

---

## 33. 本地 Package

Project Aether 将来若存在可独立复用模块，可考虑：

```text
Packages/
└── com.projectaether.somepackage/
```

只有当模块具备：

- 独立版本。
- 独立依赖。
- 独立发布意义。
- 可跨项目复用价值。

才建议迁移为 Package。

不要为了“模块化”过早 Package 化。

---

## 34. StreamingAssets

只有必须以原始文件形式随包存在的内容放入：

```text
Assets/StreamingAssets/
```

使用前必须考虑：

- 平台读取方式不同。
- Android 通常位于 APK/JAR 内。
- 不等同于普通文件系统。
- 不自动具备热更新能力。
- 不应替代 Addressables。

---

## 35. Resources 目录

Project Aether 原则上禁止业务系统依赖 Unity `Resources/` 作为主资源方案。

如果第三方工具或极少数启动资源必须使用：

```text
Resources/
```

必须：

- 范围最小。
- 有明确原因。
- 记录用途。
- 不把大型资源系统建立在 `Resources.Load` 上。

---

## 36. Addressables

Addressables 资源不要求物理目录与 Group 完全相同。

但必须保证：

- Asset 目录职责明确。
- Addressable Group 策略明确。
- Label 规则明确。
- Build / Load Path 明确。
- Group 不用于掩盖混乱目录。

---

## 37. Generated

如果模块存在自动生成 C#：

推荐：

```text
<Module>/
└── Generated/
```

例如：

```text
Framework/Config/Generated/
```

规则：

- 文件头标记自动生成。
- 禁止手动修改。
- 生成器路径明确。
- 是否提交 Git 必须统一。

---

## 38. 配置生成产物

推荐区分：

```text
Source Config
  ↓
Importer / Generator
  ↓
Generated Runtime Data
```

原始策划数据、生成工具、运行时数据必须职责分离。

不应把所有文件混入同一目录。

---

## 39. Tools 根目录

仓库根：

```text
Tools/
```

用于 Unity 工程外工具。

例如：

```text
Tools/
├── ConfigExporter/
├── ProtocolGenerator/
├── AssetAudit/
└── Scripts/
```

这些工具可以使用：

- C# Console。
- Python。
- Go。
- PowerShell。
- Bash。

但必须有 README 或使用说明。

---

## 40. BuildScripts 根目录

统一：

```text
BuildScripts/
```

用于：

- CI 构建入口。
- 本地自动构建。
- 平台脚本。
- 发布脚本。
- 版本脚本。
- Addressables Build 脚本。
- Package 脚本。

推荐：

```text
BuildScripts/
├── Unity/
├── Windows/
├── Android/
├── Server/
├── CI/
└── Release/
```

---

## 41. Builds

本地构建输出：

```text
Builds/
```

推荐：

```text
Builds/
├── Windows/
├── Android/
├── Server/
└── Development/
```

通常加入 `.gitignore`。

`BuildScripts/` 是源码。

`Builds/` 是产物。

两者严禁混淆。

---

## 42. CI 目录

如果 CI 配置数量较少，可使用平台固定目录：

```text
.github/
.gitlab/
```

大型自建流水线也可以建立：

```text
CI/
```

如果未来新增 `CI/`，必须同步更新本文档和索引。

---

## 43. Docs 总体结构

与 `02_DocumentStandard.md` 对齐：

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

---

## 44. Docs/00_ProjectStandard

正式文档：

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

---

## 45. Docs/01_Architecture

推荐：

```text
Docs/01_Architecture/
├── 00_Architecture_Index.md
├── Framework/
├── Resource/
├── Config/
├── Networking/
├── Character/
└── Combat/
```

目录根据真实架构逐步创建。

---

## 46. Docs/02_Design

推荐：

```text
Docs/02_Design/
├── 00_Design_Index.md
├── Framework/
├── Resource/
├── Config/
├── Networking/
└── ProjectPlan/
```

`ProjectPlan/` 可以保存里程碑实例等项目设计文档。

新增该子目录不意味着新增一级 Docs 分类。

---

## 47. Docs/03_Review

推荐：

```text
Docs/03_Review/
├── ProjectStandard/
├── Architecture/
├── Module/
└── Release/
```

例如本次规范包一致性评审应存放：

```text
Docs/03_Review/ProjectStandard/20260808_ProjectStandard_ConsistencyReview.md
```

---

## 48. Docs/04_RFC

推荐：

```text
Docs/04_RFC/
├── Open/
├── Approved/
├── Rejected/
└── Archived/
```

也可以使用状态字段而不分目录。

两种方式选择其一后应保持一致。

---

## 49. Docs/05_Test

推荐：

```text
Docs/05_Test/
├── Framework/
├── Resource/
├── Config/
├── Network/
├── Performance/
└── Release/
```

---

## 50. Docs/06_DecisionLog

推荐：

```text
Docs/06_DecisionLog/
├── 00_DecisionLog_Index.md
└── ADR-XXX_*.md
```

---

## 51. Docs/07_AI

推荐：

```text
Docs/07_AI/
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

## 52. Docs/Templates

推荐：

```text
Docs/Templates/
├── ArchitectureTemplate.md
├── DesignTemplate.md
├── ReviewTemplate.md
├── RFCTemplate.md
├── TestTemplate.md
├── DecisionTemplate.md
├── MilestoneTemplate.md
└── AIHandoffTemplate.md
```

模板只定义格式。

不得作为项目事实来源。

---

## 53. asmdef 总原则

每个核心模块应有明确 Assembly Definition。

目标：

- 编译边界清晰。
- 依赖方向明确。
- 降低全项目重编译。
- 支持测试隔离。
- 阻止非法引用。
- 提高模块可维护性。

---

## 54. asmdef 命名

统一：

```text
ProjectAether.<Layer>.<Module>
```

示例：

```text
ProjectAether.Framework.Resource
ProjectAether.Framework.Config
ProjectAether.Gameplay.Combat
ProjectAether.Presentation.UI
```

Editor：

```text
ProjectAether.Framework.Config.Editor
```

Tests：

```text
ProjectAether.Framework.Config.Tests
```

---

## 55. asmdef 文件位置

asmdef 应放在其覆盖范围的逻辑根目录。

示例：

```text
Framework/Resource/
└── ProjectAether.Framework.Resource.asmdef
```

禁止把一个 asmdef 放在过高层级，意外覆盖多个应独立编译的模块。

---

## 56. asmdef 依赖方向

核心方向：

```text
Presentation
    ↓
Gameplay
    ↓
Framework
```

并且：

```text
Editor → Runtime
Tests  → Runtime
Runtime ✗ Editor
Runtime ✗ Tests
Framework ✗ Gameplay
Framework ✗ Presentation
```

---

## 57. 网络依赖

Gameplay 可以依赖 Project Aether 网络抽象。

不推荐 Gameplay 直接强耦合 FishNet 具体 API。

如果实际实现需要 FishNet 类型，应尽量限制在：

```text
Framework/Networking/FishNet/
Gameplay/<Module>/Network/
```

并持续评估依赖扩散。

---

## 58. VContainer 目录原则

VContainer 注册应集中在 Composition Root 或模块安装器。

推荐目录：

```text
Framework/Bootstrap/
Framework/DependencyInjection/
```

或模块内部：

```text
Gameplay/Combat/Runtime/Installer/
```

不要在每个业务类附近创建随意的 Container 配置。

---

## 59. Namespace 与目录

Namespace 必须反映架构层级。

例如文件：

```text
Assets/ProjectAether/Framework/Resource/Runtime/Manager/ResourceManager.cs
```

推荐：

```csharp
namespace ProjectAether.Framework.Resource
{
}
```

不要求 Namespace 机械包含 `Runtime/Manager` 每一级目录。

目录用于物理组织。

Namespace 用于稳定逻辑边界。

---

## 60. Runtime / Editor / Tests 子目录

是否每个模块都创建：

```text
Runtime/
Editor/
Tests/
```

取决于实际需要。

不要提前创建空目录。

如果模块规模较小，可以：

```text
Resource/
├── ResourceManager.cs
├── ResourceHandle.cs
└── ProjectAether.Framework.Resource.asmdef
```

随着规模扩大再引入子目录。

---

## 61. 目录深度

建议目录深度控制在合理范围。

不推荐：

```text
Assets/ProjectAether/Framework/Resource/Runtime/Core/Manager/Internal/Implementation/Default/
```

过深目录增加：

- 导航成本。
- 移动成本。
- Namespace 混乱。
- AI 上下文搜索成本。

---

## 62. 文件夹命名

统一：

- 英文。
- PascalCase。
- 使用完整单词。
- 避免缩写。
- 表达职责。

推荐：

```text
Resource
Networking
Diagnostics
Serialization
```

不推荐：

```text
Res
Net
Diag
Ser
```

除非缩写已经是行业和项目统一术语。

---

## 63. 文件命名

C# 文件：

```text
TypeName.cs
```

文档：

```text
编号_名称.md
```

资源根据资产专项规范执行。

禁止：

```text
NewScript.cs
Test1.cs
Temp.cs
Final.cs
Final2.cs
```

---

## 64. Meta 文件

Unity `.meta` 必须与对应 Asset 一起提交。

禁止：

- 删除 Asset 后保留无意义 `.meta`。
- 移动 Asset 时通过文件系统破坏 GUID。
- 只复制 Asset 不复制 `.meta`。

推荐在 Unity Editor 或支持 Meta 的工具内移动 Asset。

---

## 65. Scene 文件

Scene 统一进入：

```text
Assets/ProjectAether/Content/Scenes/
```

测试 Scene：

```text
Assets/ProjectAether/Content/Scenes/Test/
```

开发实验 Scene：

```text
Assets/ProjectAether/Content/Scenes/Development/
```

---

## 66. Prefab 文件

Prefab 统一进入：

```text
Assets/ProjectAether/Content/Prefabs/
```

除非 Prefab 属于第三方 Asset。

---

## 67. Material / Texture / Model

统一放在 `Content/` 对应分类。

可以按功能继续细分。

禁止每个代码模块内部重复建立：

```text
Textures/
Materials/
Models/
```

除非该模块确实作为一个高度自包含内容包。

---

## 68. Shader

未来 Shader 规范未独立建立前，推荐：

```text
Assets/ProjectAether/Presentation/Shaders/
```

或：

```text
Assets/ProjectAether/Content/Shaders/
```

团队选择后必须统一。

建议：

- Shader 源码属于 Presentation 技术实现时放 `Presentation/Shaders/`。
- ShaderGraph 与 Material 资产属于 Content 时放 `Content/`。

---

## 69. Audio

音频资源：

```text
Assets/ProjectAether/Content/Audio/
```

音频 Runtime 逻辑：

```text
Assets/ProjectAether/Presentation/Audio/
```

资源与代码职责分离。

---

## 70. Animation

Animation Clip、Controller 等资源：

```text
Assets/ProjectAether/Content/Animations/
```

动画状态驱动和表现桥接代码：

```text
Assets/ProjectAether/Presentation/Animation/
```

---

## 71. VFX

VFX 资源：

```text
Assets/ProjectAether/Content/VFX/
```

VFX Runtime 控制：

```text
Assets/ProjectAether/Presentation/VFX/
```

---

## 72. Gizmos

若 Unity Gizmos 资源有固定要求，可使用：

```text
Assets/Gizmos/
```

这属于 Unity 特殊目录例外。

不得将项目业务代码放入该目录。

---

## 73. Plugins

如果原生插件必须使用 Unity 固定：

```text
Assets/Plugins/
```

则遵循 Unity 规则。

项目自有原生插件应有明确子目录和文档。

---

## 74. Resources 特殊目录

Unity 特殊目录可能改变构建行为。

任何新增以下目录前必须了解其语义：

```text
Editor
Resources
StreamingAssets
Plugins
Gizmos
Editor Default Resources
```

不得把它们当普通分类目录使用。

---

## 75. 临时文件

临时文件不得进入正式目录。

本地临时内容使用操作系统临时目录、工具缓存目录或 Git Ignore 目录。

禁止：

```text
Assets/ProjectAether/Temp/
```

长期存在。

---

## 76. Backup

禁止手工复制：

```text
ResourceManager_old.cs
ResourceManager_backup.cs
ResourceManager_final.cs
```

历史由 Git 管理。

---

## 77. Deprecated 代码

暂时保留的废弃代码应：

- 使用 `[Obsolete]`。
- 有迁移计划。
- 有删除版本。
- 不创建 `Old/` 作为长期垃圾场。

---

## 78. 示例与 Sample

正式项目内部示例可放：

```text
Assets/ProjectAether/Samples/
```

仅当项目确实需要。

Package Sample 应遵循 UPM Sample 结构。

---

## 79. Demo 与 Prototype

Spike 或原型应与正式代码隔离。

推荐：

```text
Assets/ProjectAether/Prototypes/
```

但只在确有长期验证需要时创建。

Prototype 代码默认不能直接成为正式模块。

---

## 80. Debug 工具

Runtime Debug 工具推荐：

```text
Assets/ProjectAether/Framework/Diagnostics/
```

Editor Debug 工具：

```text
Assets/ProjectAether/Editor/Debug/
```

开发工具不得污染正式业务 API。

---

## 81. Platform

平台适配：

```text
Framework/
└── Platform/
    ├── Runtime/
    ├── Android/
    ├── Windows/
    └── Server/
```

具体目录按真实目标平台创建。

---

## 82. Server

如果 Dedicated Server 与 Client 共用 Unity Project：

推荐通过：

- asmdef。
- 条件编译。
- Server-specific composition。
- Build Profile / Build Script。

管理。

不应直接复制一套 Gameplay 代码。

---

## 83. Build Symbols

Build Symbol 管理由构建配置负责。

禁止在目录结构中复制：

```text
ClientCode/
ServerCode/
```

来替代合理的编译边界，除非确实完全不同实现。

---

## 84. Addressables Generated Data

Addressables 自动生成数据按 Unity / Package 约定管理。

禁止手工移动 Package 需要的生成目录。

是否提交 Git 根据官方建议和项目构建策略统一决定。

---

## 85. Local Cache

以下内容通常不提交：

```text
Library/
Temp/
Logs/
Obj/
Builds/
UserSettings/
MemoryCaptures/
Recordings/
```

详细 Git 规则见 `03_GitStandard.md`。

---

## 86. Library

`Library/` 是 Unity 本地缓存。

禁止提交。

删除后 Unity 可重新生成。

---

## 87. ProjectSettings

必须提交：

```text
ProjectSettings/
```

修改前应明确影响。

涉及：

- Physics。
- Input。
- Quality。
- Graphics。
- Player。
- Package。
- Serialization。

的重要设置应经过 Review。

---

## 88. UserSettings

通常不提交：

```text
UserSettings/
```

因为包含个人 Editor 配置。

如果未来存在团队共享需求，应使用正式 ProjectSettings 或工具配置，不依赖个人 UserSettings。

---

## 89. README

仓库根 `README.md` 至少包含：

- 项目名称。
- Unity 版本。
- 获取代码方式。
- 首次启动步骤。
- Package 安装说明。
- 文档入口。
- Build 入口。
- 测试入口。
- 常见问题。

---

## 90. CHANGELOG

正式版本维护：

```text
CHANGELOG.md
```

不用于记录每个开发 Commit。

---

## 91. Git Ignore

`.gitignore` 必须覆盖 Unity 本地生成内容。

不得因为“方便同步”提交 Library。

---

## 92. Git Attributes

`.gitattributes` 应考虑：

- 文本换行。
- Unity YAML。
- Git LFS。
- 二进制文件。
- Merge Driver。

---

## 93. Git LFS

适合大型二进制：

- PSD。
- 大型音频源。
- 视频。
- 大型模型源。
- 高分辨率源文件。

是否使用 LFS 必须由项目统一决定。

---

## 94. 资源源文件

需要长期保留但不进入最终 Unity Runtime 的源文件，可以考虑：

```text
SourceAssets/
```

是否建立仓库根 `SourceAssets/` 取决于美术工作流。

建立前必须更新本文档。

---

## 95. 构建产物

Build 输出不能提交到项目源码目录。

错误：

```text
Assets/ProjectAether/Build/
```

正确：

```text
Builds/
```

---

## 96. 测试产物

测试报告建议输出到：

```text
Artifacts/TestResults/
```

或 CI Artifact。

如果未来建立 `Artifacts/` 根目录，应加入 `.gitignore` 并同步更新本文档。

测试源码仍位于：

```text
Assets/ProjectAether/Tests/
```

---

## 97. 性能产物

Profiler Capture、Memory Snapshot 等默认作为测试 Artifact，不放入 Runtime Asset 目录。

关键基线可以归档到文档系统或专用性能数据存储。

---

## 98. 目录创建规则

新增一级或关键二级目录前必须确认：

- 是否已有同类目录。
- 是否符合架构。
- 是否导致重复职责。
- 是否需要 asmdef。
- 是否需要 Namespace。
- 是否需要 Docs。
- 是否需要测试目录。

---

## 99. 禁止重复模块

禁止同时出现：

```text
Framework/Resource/
Framework/Resources/
Gameplay/Resource/
Common/Resource/
```

来表达同一个资源系统。

模块必须有唯一正式归属。

---

## 100. 模块移动

移动核心模块时必须评估：

- Namespace。
- asmdef。
- GUID。
- Prefab 引用。
- Scene 引用。
- Addressables。
- Tests。
- Docs。
- Git 历史。
- CI。

大型移动应单独 Commit。

---

## 101. 模块重命名

重命名必须同步：

- 文件夹。
- Namespace。
- asmdef。
- 文档。
- 测试。
- Git Commit。
- 相关配置。

---

## 102. 目录与 Architecture

Architecture 文档决定职责。

Directory Standard 决定物理组织。

当 Architecture 新增正式模块时，再根据本文档创建目录。

禁止为了目录好看提前创建大量没有 Architecture 支撑的模块。

---

## 103. 目录与 Design

Design 可以提出新目录需求。

在实现前应检查是否符合本文档。

重大目录重构应通过 Review。

---

## 104. 目录与 Review

Code Review 必须检查：

- 文件是否在正确目录。
- Runtime / Editor 是否隔离。
- asmdef 是否正确。
- Namespace 是否正确。
- 测试是否在合理位置。
- 第三方代码是否隔离。

---

## 105. 目录与 AI

AI 修改仓库前必须读取：

- 当前真实文件树。
- `01_DirectoryStandard.md`。
- 当前模块 Architecture。
- 当前 asmdef。

AI 不得根据通用 Unity 习惯自行创建：

```text
Scripts/
Managers/
Systems/
Utils/
```

而忽略 Project Aether 结构。

---

## 106. AI 新增文件规则

AI 新增文件时必须明确：

- 文件完整路径。
- 文件所属模块。
- Namespace。
- asmdef。
- 是否需要测试。
- 是否需要文档。

---

## 107. AI 不得自动大规模移动目录

目录移动可能影响：

- GUID。
- Scene。
- Prefab。
- Addressables。
- asmdef。
- Namespace。
- Git。

AI Agent 不得未经人工确认执行大规模目录移动。

---

## 108. 目录检查脚本

未来可以建立自动验证工具检查：

- 非法顶级目录。
- Runtime 引用 UnityEditor。
- Namespace 违规。
- asmdef 循环依赖。
- Tests 位置。
- ThirdParty 修改。
- 缺失 Meta。
- 临时文件。
- 命名违规。

---

## 109. 推荐最终树形结构

当核心框架逐步建立后，Project Aether 可以演进为：

```text
ProjectAether/
├── Assets/
│   ├── ProjectAether/
│   │   ├── Framework/
│   │   │   ├── Bootstrap/
│   │   │   ├── Module/
│   │   │   ├── Service/
│   │   │   ├── Logging/
│   │   │   ├── Resource/
│   │   │   ├── Config/
│   │   │   ├── Pool/
│   │   │   ├── Time/
│   │   │   ├── Networking/
│   │   │   ├── Platform/
│   │   │   ├── Serialization/
│   │   │   └── Diagnostics/
│   │   ├── Gameplay/
│   │   │   ├── Character/
│   │   │   ├── Combat/
│   │   │   ├── Ability/
│   │   │   ├── AI/
│   │   │   ├── Interaction/
│   │   │   ├── Quest/
│   │   │   └── World/
│   │   ├── Presentation/
│   │   │   ├── UI/
│   │   │   ├── Camera/
│   │   │   ├── Input/
│   │   │   ├── Audio/
│   │   │   ├── Animation/
│   │   │   └── VFX/
│   │   ├── Content/
│   │   │   ├── Scenes/
│   │   │   ├── Prefabs/
│   │   │   ├── Materials/
│   │   │   ├── Textures/
│   │   │   ├── Models/
│   │   │   ├── Animations/
│   │   │   ├── Audio/
│   │   │   ├── VFX/
│   │   │   ├── Fonts/
│   │   │   └── DataAssets/
│   │   ├── Editor/
│   │   └── Tests/
│   ├── ThirdParty/
│   └── StreamingAssets/
├── Packages/
├── ProjectSettings/
├── Docs/
│   ├── 00_ProjectStandard/
│   ├── 01_Architecture/
│   ├── 02_Design/
│   ├── 03_Review/
│   ├── 04_RFC/
│   ├── 05_Test/
│   ├── 06_DecisionLog/
│   ├── 07_AI/
│   └── Templates/
├── Tools/
├── BuildScripts/
├── Builds/
├── README.md
├── CHANGELOG.md
├── .gitignore
└── .gitattributes
```

此结构表示长期目标，不要求当前阶段一次性创建所有空目录。

---

## 110. 当前阶段最小目录

Project Aether 当前处于 Project Standard / Core Framework 基础阶段。

当前真正需要的最小结构可以保持：

```text
Assets/
└── ProjectAether/
    ├── Framework/
    │   ├── Bootstrap/
    │   ├── Module/
    │   ├── Resource/
    │   ├── Config/
    │   └── Pool/
    ├── Editor/
    └── Tests/
```

后续 Gameplay、Presentation、Networking 等目录在对应 Milestone 启动时创建。

---

## 111. 当前已确认模块

当前项目历史中已经明确讨论或实现过：

- Bootstrap。
- ModuleManager。
- Resource。
- Config。
- Pool。

这些模块应按本文档归入：

```text
Assets/ProjectAether/Framework/
```

---

## 112. Framework 当前基线

推荐当前落位：

```text
Assets/ProjectAether/Framework/
├── Bootstrap/
├── Module/
├── Resource/
├── Config/
└── Pool/
```

Namespace 统一：

```text
ProjectAether.Framework...
```

---

## 113. FSM 目录

若 FSM 属于通用基础框架，应位于：

```text
Assets/ProjectAether/Framework/FSM/
```

而不是 Gameplay。

若未来存在具体 Gameplay State Machine，则可以位于：

```text
Assets/ProjectAether/Gameplay/<Module>/State/
```

两者职责不同。

---

## 114. Config 与 Bootstrap 依赖

Config 是 Framework 模块。

Bootstrap 只负责组合和启动，不应把 Config 实现代码放入 Bootstrap 目录。

依赖关系通过 asmdef 和 Composition Root 明确。

---

## 115. Resource 与 Pool

Resource 和 Pool 是独立 Framework 模块。

禁止为了复用缓存逻辑将 Pool 直接嵌入 Resource 目录成为隐藏子系统。

如果 Resource 使用 Pool，应通过明确依赖关系表达。

---

## 116. Framework/Common

只有以下类型适合进入 `Framework/Common/`：

- 极基础 Value Object。
- 无业务意义的通用结果类型。
- 经多个 Framework 模块稳定复用的基础契约。

禁止将：

```text
ResourceHelper
CombatUtils
ConfigMisc
```

放入 Common。

---

## 117. Helper / Utils

原则上不建立全局：

```text
Helpers/
Utils/
```

如果存在工具类，应放入其所属模块。

例如：

```text
Framework/Resource/AssetKeyUtility.cs
```

优于：

```text
Framework/Common/Utils/AssetKeyUtility.cs
```

---

## 118. Manager 目录

禁止建立全局：

```text
Managers/
```

把所有 `xxxManager` 放在一起。

`ResourceManager` 应属于 Resource 模块。

`ConfigManager` 应属于 Config 模块。

目录按领域，不按类名后缀分类。

---

## 119. Systems 目录

禁止建立全局：

```text
Systems/
```

作为所有系统集合。

`CombatSystem` 应属于 Combat。

`ResourceSystem` 应属于 Resource。

---

## 120. Interfaces 目录

不建议全局：

```text
Interfaces/
```

接口应靠近其领域。

例如：

```text
Framework/Module/IGameModule.cs
```

而不是：

```text
Framework/Interfaces/IGameModule.cs
```

---

## 121. Enums 目录

不建议全局：

```text
Enums/
```

枚举应靠近所属模块。

---

## 122. Data 目录

`Data/` 必须有明确含义。

不要混淆：

- Runtime model。
- Config。
- ScriptableObject。
- Save data。
- Network DTO。

建议使用更具体名称。

---

## 123. Models 目录

避免使用模糊 `Models/` 表达 C# 数据模型，因为 Unity 内容层也有 3D Model。

如果是 C# 领域模型，使用：

```text
Domain/
Data/
DTO/
```

之一，并明确职责。

---

## 124. DTO

跨层或网络数据传输对象可以放：

```text
<Module>/Runtime/DTO/
```

但不要让 DTO 成为无约束共享数据层。

---

## 125. Adapter

第三方适配代码推荐：

```text
<Module>/Runtime/Adapters/
```

例如：

```text
Framework/Resource/Runtime/Addressables/
Framework/Networking/Runtime/FishNet/
```

---

## 126. Installer

VContainer Installer / LifetimeScope 配置可放：

```text
<Module>/Runtime/Installer/
```

或更高层 Composition Root。

位置必须反映依赖组装职责。

---

## 127. Generated Code Namespace

生成代码 Namespace 必须稳定。

禁止生成器根据物理临时目录产生不可预测 Namespace。

---

## 128. Editor Tests

Editor 工具测试可以位于：

```text
Assets/ProjectAether/Tests/EditMode/Editor/
```

或模块：

```text
Framework/Config/Tests/Editor/
```

但必须依赖 Editor asmdef，而 Runtime Tests 不得依赖 UnityEditor。

---

## 129. PlayMode Tests

统一建议：

```text
Assets/ProjectAether/Tests/PlayMode/
```

测试 Scene：

```text
Assets/ProjectAether/Content/Scenes/Test/
```

---

## 130. Integration Tests

跨模块：

```text
Assets/ProjectAether/Tests/Integration/
```

例如：

```text
Bootstrap + ModuleManager + Resource + Config
```

---

## 131. Performance Tests

统一：

```text
Assets/ProjectAether/Tests/Performance/
```

Profiler Capture 不应直接放入该源码目录。

---

## 132. Network Tests

统一：

```text
Assets/ProjectAether/Tests/Network/
```

需要专门 Server / Client Test Scene 时使用：

```text
Content/Scenes/Test/Network/
```

---

## 133. TestData

测试数据：

```text
Assets/ProjectAether/Tests/TestData/
```

不得使用真实生产用户数据。

---

## 134. Documentation and Source Mapping

核心模块推荐形成映射：

```text
Source:
Assets/ProjectAether/Framework/Resource/

Architecture:
Docs/01_Architecture/Resource/

Design:
Docs/02_Design/Resource/

Review:
Docs/03_Review/Module/

Test:
Docs/05_Test/Resource/
```

---

## 135. 项目计划文档

`09_ProjectMilestone.md` 中的具体 Milestone 实例可以保存：

```text
Docs/02_Design/ProjectPlan/
```

例如：

```text
PA-M0_ProjectStandard.md
PA-M1_ProjectStructure.md
PA-M2_CoreFramework.md
```

---

## 136. Review 记录位置

本规范包 Review：

```text
Docs/03_Review/ProjectStandard/
```

模块 Review：

```text
Docs/03_Review/Module/
```

Release Review：

```text
Docs/03_Review/Release/
```

---

## 137. Build Document

Build 架构属于：

```text
Docs/01_Architecture/Build/
```

具体 Build Pipeline 设计属于：

```text
Docs/02_Design/Build/
```

发布测试：

```text
Docs/05_Test/Release/
```

---

## 138. Asset Pipeline Document

Asset Pipeline Architecture：

```text
Docs/01_Architecture/AssetPipeline/
```

具体导入、压缩、Addressables 分组设计：

```text
Docs/02_Design/AssetPipeline/
```

---

## 139. 目录变化审批

以下目录变化需要 Standard / Architecture Review：

- 新增顶级 Runtime Layer。
- 移动核心 Framework 模块。
- 新增仓库根目录。
- 修改 Tests 根路径。
- 修改 Docs 一级分类。
- 修改 ThirdParty 管理策略。
- 修改 Package 管理策略。

---

## 140. 小型目录变化

模块内部新增：

```text
Cache/
Validation/
Requests/
```

等细分类通常不需要 RFC，但应在 Code Review 中确认合理性。

---

## 141. 迁移旧目录

如果现有工程仍存在旧结构：

```text
Assets/Framework/
Assets/Game/
Tests/
```

迁移时必须分阶段执行：

```text
Inventory Existing Files
  ↓
Map Old → New
  ↓
Check Meta / GUID
  ↓
Move in Unity
  ↓
Update Namespace
  ↓
Update asmdef
  ↓
Compile
  ↓
Run Tests
  ↓
Update Docs
  ↓
Commit
```

---

## 142. 迁移禁止事项

禁止一次性：

- 文件系统直接移动大量 Unity Asset。
- 批量重命名而不检查 GUID。
- 同时做目录迁移和功能重构。
- 移动后不编译。
- 移动后不检查 Scene / Prefab。

---

## 143. 目录迁移 Commit

建议单独 Commit：

```text
[Project][Refactor] Align source directories with project standard
```

如果只修改文档：

```text
[Docs][Update] Align directory standard with project architecture
```

---

## 144. 本文档修订后的当前建议

当前先不要立即创建所有长期目录。

推荐下一阶段只落实：

```text
Assets/ProjectAether/
├── Framework/
│   ├── Bootstrap/
│   ├── Module/
│   ├── Resource/
│   ├── Config/
│   └── Pool/
├── Editor/
└── Tests/
```

等进入：

```text
PA-M8 Networking Foundation
PA-M9 Character Prototype
PA-M10 Combat Prototype
```

再创建对应目录。

---

## 145. Directory Review Checklist

### Root

- [ ] 项目源码位于 `Assets/ProjectAether/`。
- [ ] UPM 依赖由 `Packages/` 管理。
- [ ] 非 UPM Vendor 位于 `Assets/ThirdParty/`。
- [ ] Docs 位于仓库根 `Docs/`。
- [ ] Tools 与 BuildScripts 分离。
- [ ] Build 输出进入 `Builds/`。

### Architecture

- [ ] Framework / Gameplay / Presentation 分层明确。
- [ ] Framework 不依赖 Gameplay。
- [ ] Gameplay 不依赖具体 UI。
- [ ] Content 与代码分离。
- [ ] 模块唯一归属。

### Runtime / Editor

- [ ] Runtime 不引用 UnityEditor。
- [ ] Editor 有独立 asmdef。
- [ ] Editor 工具位置合理。
- [ ] Unity 特殊目录没有被误用。

### Tests

- [ ] 测试源码位于 `Assets/ProjectAether/Tests/` 或模块 Tests。
- [ ] Tests asmdef 不被 Runtime 引用。
- [ ] TestData 与正式数据分离。
- [ ] PlayMode / EditMode / Integration 职责清晰。

### Third Party

- [ ] 第三方与项目源码分离。
- [ ] Package 版本可追踪。
- [ ] 没有随意修改 Vendor 源码。
- [ ] 修改第三方代码有记录。

### Generated

- [ ] Generated 文件与手写源码分离。
- [ ] 生成方式明确。
- [ ] 是否提交 Git 明确。
- [ ] 自动生成文件没有手改。

### Documentation

- [ ] Docs 一级目录与规范一致。
- [ ] Architecture / Design / Review / Test 路径明确。
- [ ] 新目录已更新索引。
- [ ] 文档链接有效。

### AI

- [ ] AI 新增文件使用正确路径。
- [ ] AI 没有创建 `Scripts/Managers/Utils` 等旧式目录。
- [ ] AI 读取了当前文件树。
- [ ] 大规模移动经过人工确认。

---

## 146. 验收标准

本规范执行后，应达到：

- 所有 Project Aether 自有源码可从 `Assets/ProjectAether/` 明确识别。
- Framework、Gameplay、Presentation 的依赖方向清晰。
- Runtime、Editor 和 Tests 不再混杂。
- Tests 路径与 `06_TestStandard.md` 一致。
- Docs 路径与 `02_DocumentStandard.md` 一致。
- asmdef 命名与 Namespace 规则一致。
- 第三方 UPM 与非 UPM 资产拥有明确边界。
- Build Script 和 Build 输出不再混淆。
- 当前 Framework 模块可以稳定落位。
- 后续 Network、Character、Combat 可以按 Milestone 逐步扩展。
- AI 工具可以根据目录准确判断模块职责。
- 不再使用旧版 `Assets/Framework/`、`Assets/Game/` 和根 `Tests/` 作为正式项目结构。

---

## 147. 后续 Review

完成本文档 v1.1 后，应重新执行：

```text
Project Standard Cross-Document Validation
```

重点检查：

- `00_ProjectStandard_Index.md`
- `02_DocumentStandard.md`
- `04_CodingStandard.md`
- `06_TestStandard.md`
- `08_ProjectWorkflow.md`
- `09_ProjectMilestone.md`

确认目录路径全部一致。

---

## 148. 建议 Git Commit

本次文档修订建议：

```text
[Docs][Update] Align directory standard with project architecture
```

---

## 149. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-07-28 | 创建初始工程目录规范 |
| v1.1 | 2026-08-08 | 统一 `Assets/ProjectAether/`、Framework / Gameplay / Presentation、Tests、ThirdParty、BuildScripts 与 Docs 目录基线 |

---

# End
