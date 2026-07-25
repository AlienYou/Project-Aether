# 49_核心框架实现_PoolHandle设计文档

## 1. 文档目的

本文档定义 Project Aether 对象池系统中 PoolHandle 的设计。

当前 PoolManager 已完成基础对象池能力：

* Prefab ResourceHandle 生命周期绑定
* GameObject 实例复用
* Pool 创建与销毁管理

但是当前接口直接返回 GameObject：

```csharp
UniTask<GameObject> SpawnAsync(string assetPath)
```

存在生命周期管理问题。

因此引入 PoolHandle，对运行时实例进行统一管理。

---

# 2. 当前问题分析

## 2.1 GameObject 生命周期不可控

当前：

```csharp
GameObject obj =
    await PoolManager.SpawnAsync(path);
```

调用方可以：

```csharp
Object.Destroy(obj);
```

但是该对象属于对象池。

直接 Destroy 会导致：

* Pool 内部状态错误
* 对象数量统计错误
* 后续无法复用

---

## 2.2 Recycle责任不明确

当前：

```csharp
PoolManager.Recycle(obj);
```

依赖调用方主动调用。

存在：

* 忘记Recycle
* 重复Recycle
* 错误Pool回收

等问题。

---

## 2.3 缺少对象所有权概念

当前结构：

```
PoolManager
    |
    |
 GameObject
```

无法表达：

* 谁创建
* 谁负责释放
* 是否已经释放

---

# 3. PoolHandle设计目标

引入：

```
PoolHandle
```

作为运行时实例生命周期代理。

目标：

1. 管理实例引用
2. 保存所属Pool
3. 防止重复释放
4. 提供统一Release接口

---

# 4. 系统关系

完整生命周期：

```
ResourceHandle<GameObject>

        |
        |
        ↓

Pool

        |
        |
        ↓

PoolHandle

        |
        |
        ↓

GameObject Instance
```

职责划分：

| 模块             | 职责           |
| -------------- | ------------ |
| ResourceHandle | Prefab资源生命周期 |
| Pool           | 实例创建与缓存      |
| PoolHandle     | 实例使用生命周期     |
| GameObject     | 具体运行对象       |

---

# 5. API变化

## 修改前

```csharp
UniTask<GameObject>
SpawnAsync(string assetPath)
```

调用：

```csharp
var obj =
    await PoolManager.SpawnAsync(path);
```

释放：

```csharp
PoolManager.Recycle(obj);
```

---

## 修改后

```csharp
UniTask<PoolHandle>
SpawnAsync(string assetPath)
```

调用：

```csharp
var handle =
    await PoolManager.SpawnAsync(path);


var obj =
    handle.Instance;
```

释放：

```csharp
handle.Release();
```

---

# 6. PoolHandle结构设计

```
PoolHandle

    |
    |
    ├── GameObject Instance
    |
    ├── Pool Owner
    |
    ├── bool IsReleased
    |
    └── Release()
```

---

# 7. 核心字段设计

## Instance

作用：

保存实际运行对象。

```csharp
public GameObject Instance
{
    get;
}
```

---

## Owner Pool

作用：

记录对象来源。

```csharp
private Pool _ownerPool;
```

用于：

```text
Release()

↓

OwnerPool.Recycle()
```

---

## IsReleased

作用：

防止重复释放。

例如：

```csharp
handle.Release();

handle.Release();
```

第二次不应该进入Pool。

---

# 8. 生命周期流程

## Spawn流程

```
PoolManager

    |
    ↓

Pool.Spawn()

    |
    ↓

GameObject Instance

    |
    ↓

Create PoolHandle

    |
    ↓

返回 Gameplay
```

---

## Release流程

```
PoolHandle.Release()

        |
        ↓

检查 IsReleased

        |
        ↓

标记 Released

        |
        ↓

Pool.Recycle()

        |
        ↓

对象进入缓存池
```

---

# 9. 与Resource系统关系

注意：

PoolHandle 不负责资源释放。

错误：

```
PoolHandle.Release()

↓

Destroy Prefab Resource
```

正确：

```
PoolHandle

负责：

GameObject Instance


ResourceHandle

负责：

Prefab Asset

```

生命周期：

```
ResourceHandle<GameObject>

        |
        ↓

Pool

        |
        ↓

PoolHandle

        |
        ↓

GameObject
```

---

# 10. 不包含功能

本阶段不实现：

* 自动GC释放
* IDisposable
* Finalizer
* Addressables Pool
* 跨场景池迁移
* 自动生命周期绑定

保持系统简单。

---

# 11. 修改范围

## 新增

```
PoolHandle.cs
```

---

## 修改

```
PoolManager.cs

SpawnAsync返回类型:

GameObject

↓

PoolHandle
```

---

```
Pool.cs

增加Handle创建逻辑
```

---

## 保持不变

```
ResourceManager

ResourceHandle

ResourceCache

ResourceGC
```

---

# 12. 验证标准

完成后必须验证：

## 创建

```
SpawnAsync

↓

生成PoolHandle
```

## 使用

```
PoolHandle.Instance
```

正常访问对象。

## 回收

```
PoolHandle.Release()

↓

Pool.Recycle()
```

## 安全性

重复：

```
Release()
Release()
```

不会导致：

* 重复入池
* Queue污染

---

# 13. Git提交规范

代码：

```
[Pool][Feature] Implement PoolHandle lifecycle
```

文档：

```
[Docs][Update] Add PoolHandle design document
```

---

# 14. 下一阶段

进入：

```
49A_PoolHandle可编译代码版
```

实现：

* PoolHandle.cs
* PoolManager接口调整
* Pool适配修改
* 测试流程

并基于当前已有代码增量修改。
