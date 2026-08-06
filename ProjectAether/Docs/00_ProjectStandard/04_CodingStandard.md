# Project Aether Coding Standard

> Document ID: PAS-004
> Version: v1.0

本文档定义 Project Aether 编码规范。

## 核心原则
- Architecture First
- Single Responsibility
- Explicit Design

## 命名规范
- Class: PascalCase
- Interface: I 前缀
- Private Field: _camelCase
- Async 方法: Async 后缀

## Unity
- MonoBehaviour 仅负责生命周期
- 禁止大量 GameObject.Find
- 优先事件驱动

## 异步
- UniTask
- CancellationToken
- 处理异常

## Review Checklist
- 编译通过
- 生命周期正确
- 文档同步
- 测试通过

# End
