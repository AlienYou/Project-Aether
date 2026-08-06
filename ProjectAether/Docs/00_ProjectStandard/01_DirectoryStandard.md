# Project Aether Directory Standard

> Document ID: PAS-001\
> Version: v1.0\
> Status: Draft\
> Category: Project Standard\
> Last Updated: 2026-07-28

------------------------------------------------------------------------

# 1. Overview

## 1.1 Purpose

本文档定义 Project Aether 工程目录结构规范。

目标：

-   保持项目结构清晰
-   降低团队协作成本
-   明确不同类型文件职责
-   避免资源、代码、工具混乱
-   支持长期商业级游戏项目维护

所有新增目录和文件必须符合本文档规范。

------------------------------------------------------------------------

# 2. Root Directory Structure

Project Aether 根目录结构：

    ProjectAether/

    ├── Assets/

    ├── Packages/

    ├── ProjectSettings/

    ├── Docs/

    ├── Tests/

    ├── Tools/

    ├── Build/

    ├── README.md

    └── .gitignore

------------------------------------------------------------------------

# 3. Root Directory Responsibility

## 3.1 Assets

路径：

    Assets/

职责：

存放 Unity 项目运行时资源。

包括：

-   Scripts
-   Prefabs
-   Scenes
-   Materials
-   Shaders
-   Textures
-   Animations
-   Audio
-   Models
-   Addressables Resources

禁止：

-   存放项目文档
-   存放临时文件
-   存放外部工具

------------------------------------------------------------------------

## 3.2 Packages

路径：

    Packages/

职责：

管理 Unity Package。

包括：

-   Unity Package Manager 配置
-   第三方 Package
-   自研 Package

禁止：

直接复制第三方源码到 Assets。

------------------------------------------------------------------------

## 3.3 ProjectSettings

路径：

    ProjectSettings/

职责：

Unity 工程配置。

包括：

-   Player Settings
-   Quality Settings
-   Graphics Settings
-   Input Settings
-   Build Settings

禁止：

手动修改未确认的配置文件。

------------------------------------------------------------------------

## 3.4 Docs

路径：

    Docs/

职责：

存放项目所有文档。

结构：

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

------------------------------------------------------------------------

## 3.5 Tests

路径：

    Tests/

职责：

存放测试代码和测试资源。

包括：

-   Unit Tests
-   Integration Tests
-   Performance Tests

------------------------------------------------------------------------

## 3.6 Tools

路径：

    Tools/

职责：

存放项目辅助工具。

包括：

-   Export Tools
-   Build Tools
-   Data Tools
-   Automation Scripts

禁止：

存放游戏运行时代码。

------------------------------------------------------------------------

## 3.7 Build

路径：

    Build/

职责：

存放构建相关输出。

包括：

-   Build Scripts
-   Build Config
-   Release Notes

禁止：

提交生成文件。

------------------------------------------------------------------------

# 4. Unity Assets Structure

Assets 内部结构：

    Assets/

    ├── Game/

    ├── Framework/

    ├── ThirdParty/

    ├── Editor/

    ├── Resources/

    ├── StreamingAssets/

    └── Addressables/

------------------------------------------------------------------------

# 5. Framework Directory

路径：

    Assets/Framework/

职责：

存放基础框架代码。

例如：

    Framework/

    ├── Core/

    ├── Module/

    ├── Resource/

    ├── Config/

    ├── Event/

    ├── Pool/

    └── Utility/

Framework 规则：

-   不允许依赖 Gameplay
-   不允许依赖具体游戏内容
-   提供通用能力

------------------------------------------------------------------------

# 6. Game Directory

路径：

    Assets/Game/

职责：

存放具体游戏逻辑。

例如：

    Game/

    ├── Gameplay/

    ├── Combat/

    ├── Character/

    ├── Weapon/

    ├── UI/

    └── Network/

Game 层可以依赖 Framework。

Framework 不允许反向依赖 Game。

------------------------------------------------------------------------

# 7. ThirdParty Directory

路径：

    Assets/ThirdParty/

职责：

存放第三方插件。

例如：

-   UniTask
-   VContainer
-   FishNet

规则：

-   不直接修改第三方源码
-   如需修改必须记录 Decision Log
-   升级前必须测试

------------------------------------------------------------------------

# 8. Editor Directory

路径：

    Assets/Editor/

职责：

存放 Unity Editor 工具。

包括：

-   Inspector 扩展
-   Asset Pipeline
-   自动化工具

禁止：

存放 Runtime 代码。

------------------------------------------------------------------------

# 9. File Naming Convention

## 9.1 C# Script

规则：

    PascalCase

示例：

    ResourceManager.cs

    ConfigLoader.cs

    PlayerController.cs

------------------------------------------------------------------------

## 9.2 Folder

规则：

    PascalCase

示例：

    Resource

    Gameplay

    Combat

------------------------------------------------------------------------

## 9.3 Document

规则：

    编号_名称.md

示例：

    02_Resource.md

    01_DirectoryStandard.md

------------------------------------------------------------------------

# 10. Temporary Files

禁止提交：

    *.tmp

    *.bak

    Logs/

    Library/

    Temp/

    Obj/

临时文件必须存放：

    Temp/

并加入 Git Ignore。

------------------------------------------------------------------------

# 11. Directory Change Rules

新增一级目录必须：

1.  明确职责
2.  更新目录规范文档
3.  经过 Review
4.  提交 Git

禁止：

随意创建目录。

------------------------------------------------------------------------

# 12. Acceptance Criteria

本规范完成后：

-   项目目录结构统一
-   新成员可以快速理解工程
-   AI 可以根据目录定位文件
-   模块边界清晰
-   工程长期可维护

------------------------------------------------------------------------

# End
