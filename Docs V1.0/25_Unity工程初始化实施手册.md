# 25_Unity工程初始化实施手册

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：实施版

---

# 1. 文档目标

完成：

Project Aether

客户端工程初始化。

最终目标：

创建可开发状态工程。

---

# 2. 环境要求

Unity：

2022.3.51f1c1

---

Visual Studio：

2022

或

JetBrains Rider

---

Git：

Latest

---

Git LFS：

Latest

---

推荐系统：

Windows 11

---

# 3. Unity Hub准备

安装：

Unity Hub

---

安装版本：

Unity 2022.3.51f1c1

---

安装模块：

Windows Build Support

Android Build Support

iOS Build Support（可选）

---

# 4. 创建工程

工程名称：

ProjectAether

---

模板：

3D Core

---

渲染管线：

URP

---

目录：

D:\ProjectAether

（示例）

---

# 5. 创建Git仓库

进入工程目录：

git init

---

创建远程仓库：

Github

Gitlab

Gitea

任选

---

首次提交：

Initial Commit

---

# 6. 配置Git LFS

执行：

git lfs install

---

跟踪资源：

*.psd

*.fbx

*.wav

*.mp4

*.png

---

生成：

.gitattributes

---

# 7. Unity项目设置

Project Settings：

---

Color Space：

Linear

---

API Compatibility：

.NET Standard 2.1

---

Active Input Handling：

Input System Package

---

Texture Compression：

ASTC

---

# 8. 安装Package

打开：

Package Manager

---

安装：

Addressables

Input System

Cinemachine

AI Navigation

---

第一阶段必须安装。

---

# 9. UniTask

导入：

Cysharp UniTask

---

用途：

异步框架

---

# 10. DOTween

导入：

DOTween Pro（推荐）

或

DOTween Free

---

用途：

UI动画

战斗动画

---

# 11. Odin Inspector

推荐：

Odin Inspector

---

用途：

编辑器效率提升

---

# 12. Newtonsoft Json

安装：

Newtonsoft Json

---

用于：

工具链

配置解析

---

# 13. HybridCLR（预留）

第二阶段安装。

---

暂不启用。

---

# 14. 创建工程目录

Assets

├── Art

├── Audio

├── Config

├── GameScripts

├── Prefabs

├── Scenes

├── UI

├── VFX

├── Addressables

└── ThirdParty

---

# 15. 创建GameScripts目录

GameScripts

├── Core

├── Framework

├── Config

├── Resource

├── Network

├── UI

├── Gameplay

└── Account

---

# 16. Gameplay目录

Gameplay

├── Character

├── Attribute

├── Combat

├── Skill

├── Buff

├── AI

├── Animation

├── Monster

├── Boss

├── Drop

├── Equipment

├── Inventory

└── Settlement

---

# 17. 创建程序集

创建：

Game.Core.asmdef

---

# 18. 创建程序集

创建：

Game.Framework.asmdef

---

# 19. 创建程序集

创建：

Game.Config.asmdef

---

# 20. 创建程序集

创建：

Game.Resource.asmdef

---

# 21. 创建程序集

创建：

Game.Network.asmdef

---

# 22. 创建程序集

创建：

Game.UI.asmdef

---

# 23. 创建程序集

创建：

Game.Character.asmdef

Game.Attribute.asmdef

Game.Combat.asmdef

Game.Skill.asmdef

Game.Buff.asmdef

Game.AI.asmdef

Game.Animation.asmdef

---

# 24. 创建程序集

创建：

Game.Monster.asmdef

Game.Boss.asmdef

Game.Drop.asmdef

Game.Equipment.asmdef

Game.Inventory.asmdef

Game.Settlement.asmdef

Game.Account.asmdef

---

# 25. 配置程序集依赖

按照：

21_客户端程序集设计.md

执行。

---

禁止：

循环依赖。

---

# 26. 创建启动场景

Assets/Scenes

创建：

Bootstrap.unity

---

作为项目入口。

---

# 27. 创建场景

MainMenu.unity

---

主菜单场景。

---

# 28. 创建场景

GameWorld.unity

---

游戏主场景。

---

# 29. 创建场景

BattleTest.unity

---

战斗测试场景。

---

# 30. Bootstrap设计

负责：

初始化框架

初始化配置

初始化资源系统

初始化网络

进入登录流程

---

# 31. 创建Bootstrap模块

目录：

GameScripts/Framework/Bootstrap

---

创建：

Bootstrap.cs

---

# 32. 初始化日志系统

创建：

Log.cs

---

位置：

Game.Core

---

# 33. 初始化事件系统

创建：

EventBus.cs

---

位置：

Game.Framework

---

# 34. 初始化服务定位器

创建：

ServiceLocator.cs

---

位置：

Game.Framework

---

# 35. 初始化配置系统

创建：

ConfigManager.cs

---

位置：

Game.Config

---

# 36. 初始化资源系统

创建：

ResourceManager.cs

---

位置：

Game.Resource

---

# 37. 初始化UI系统

创建：

UIManager.cs

---

位置：

Game.UI

---

# 38. 初始化网络系统

创建：

NetworkManager.cs

---

位置：

Game.Network

---

# 39. Addressables初始化

创建：

AddressableGroups

---

分组：

Character

Monster

UI

Scene

VFX

Audio

---

# 40. 创建测试Prefab

创建：

Player_Test

---

作为角色验证对象。

---

# 41. 创建测试怪物

创建：

Monster_Test

---

用于战斗验证。

---

# 42. 创建输入配置

InputActions

---

支持：

移动

攻击

闪避

跳跃

锁定

---

# 43. 创建摄像机

Cinemachine Camera

---

支持：

第三人称跟随。

---

# 44. 创建版本规范

版本：

0.1.0

---

命名：

Alpha

---

# 45. 创建README

记录：

项目目标

技术栈

目录结构

开发规范

---

# 46. 第一次编译验证

目标：

无报错

无循环引用

无Missing Package

---

# 47. Git提交

提交：

Project Skeleton

---

Tag：

v0.1.0

---

# 48. MVP完成标准

完成：

工程创建

程序集创建

目录创建

场景创建

基础框架创建

---

达到：

可开发状态

---

# 49. 验收标准

启动：

Bootstrap

↓

初始化系统

↓

进入MainMenu

---

成功。

---

# 50. 结论

完成本手册后：

Project Aether 将拥有工业级Unity工程骨架。

具备：

目录规范

程序集规范

场景规范

框架入口

资源入口

开发基础设施

正式进入编码阶段。
