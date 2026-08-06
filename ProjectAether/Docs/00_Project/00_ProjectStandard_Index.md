# Project Aether Project Standard Index

> Document ID: PAS-000\
> Version: v1.0\
> Status: Draft\
> Category: Project Standard\
> Last Updated: 2026-07-28

------------------------------------------------------------------------

# 1. Overview

## 1.1 Purpose

本文档定义 Project Aether 工程规范体系的整体结构。

Project Aether 是一个面向商业级 3D 游戏开发的长期项目，需要具备：

-   清晰的工程结构
-   稳定的模块边界
-   可维护的代码体系
-   可扩展的架构设计
-   可追踪的技术决策
-   规范化的团队协作流程

本文档作为 Project Aether 所有工程规范文档的入口。

所有开发人员、工具以及 AI 辅助开发流程均需要遵循本规范体系。

------------------------------------------------------------------------

# 2. Project Documentation Structure

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

# 3. Document Category Definition

## 3.1 Project Standard

路径：

Docs/00_ProjectStandard/

职责：

定义项目开发基础规则。

包含：

-   工程目录规范
-   文档规范
-   Git规范
-   编码规范
-   Review规范
-   测试规范
-   AI协作规范

------------------------------------------------------------------------

## 3.2 Architecture

路径：

Docs/01_Architecture/

职责：

定义系统长期稳定架构。

包含：

-   系统职责
-   模块边界
-   生命周期
-   核心接口
-   依赖关系

------------------------------------------------------------------------

## 3.3 Design

路径：

Docs/02_Design/

职责：

记录系统设计过程。

包含：

-   设计目标
-   技术方案
-   方案比较
-   设计原因
-   扩展方向

------------------------------------------------------------------------

## 3.4 Review

路径：

Docs/03_Review/

职责：

记录架构评审和代码评审结果。

------------------------------------------------------------------------

## 3.5 RFC

路径：

Docs/04_RFC/

职责：

处理重大技术变更。

流程：

提出 RFC

↓

Review

↓

Approved

↓

更新 Architecture

↓

修改代码

------------------------------------------------------------------------

## 3.6 Test

路径：

Docs/05_Test/

职责：

记录测试方案、测试结果以及性能数据。

------------------------------------------------------------------------

## 3.7 Decision Log

路径：

Docs/06_DecisionLog/

职责：

记录重要技术决策和历史原因。

------------------------------------------------------------------------

## 3.8 AI

路径：

Docs/07_AI/

职责：

定义 AI 参与项目开发规则。

------------------------------------------------------------------------

# 4. Document Priority

文档优先级：

Architecture

↓

Approved RFC

↓

Decision Log

↓

Design

↓

Implementation Document

↓

Chat Discussion

------------------------------------------------------------------------

# 5. Document Lifecycle

状态：

Draft

↓

Review

↓

Approved

↓

Frozen

↓

Deprecated

------------------------------------------------------------------------

# 6. Project Principles

## Architecture First

先设计，再编码。

流程：

Architecture

↓

Review

↓

Implementation

↓

Test

↓

Freeze

------------------------------------------------------------------------

## Single Source of Truth

正式文档优先于聊天记录。

------------------------------------------------------------------------

## Incremental Change

采用增量修改。

禁止：

-   未评审修改核心接口
-   大规模重写已有系统

------------------------------------------------------------------------

## Module Isolation

模块保持独立边界。

Framework 不依赖 Gameplay。

Resource 不依赖 Combat。

Config 不依赖 UI。

------------------------------------------------------------------------

## Maintainability First

优先保证长期维护能力。

------------------------------------------------------------------------

# 7. Related Documents

后续文档：

-   01_DirectoryStandard.md
-   02_DocumentStandard.md
-   03_GitStandard.md
-   04_CodingStandard.md
-   05_ReviewStandard.md
-   06_TestStandard.md
-   07_AIStandard.md
-   08_ProjectWorkflow.md
-   09_ProjectMilestone.md

------------------------------------------------------------------------

# End
