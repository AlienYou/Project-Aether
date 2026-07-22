# 48_核心框架实现_PoolManager

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：设计版

关联文档：

40_核心框架实现_ResourceManager

42_核心框架实现_ResourceHandle

46_核心框架实现_ResourceCache

47_核心框架实现_ResourceGC

---

# 1. 文档目标

建立 Project Aether 第一版对象池系统（PoolManager）。

实现：

* 对象复用
* 减少 Instantiate
* 减少 Destroy
* 降低 GC 压力
* 为技能系统、怪物系统、UI系统提供运行时支持

---

# 2. 设计背景

当前资源生命周期：

```text
LoadAsync

↓

Instantiate

↓

Destroy
```

存在：

* Instantiate开销
* Destroy开销
* GC压力
* CPU抖动

对于：

* 技能特效
* 投射物
* 伤害数字
* UI窗口
* 怪物

都不适合直接使用 Instantiate / Destroy。

---

# 3. 架构设计

整体结构：

```text
Game Logic

↓

PoolManager

↓

ResourceManager

↓

ResourceProvider
```

原则：

PoolManager 不允许直接访问：

```csharp
Resources.Load()

Addressables.LoadAssetAsync()
```

所有资源必须通过：

```csharp
ResourceManager
```

统一管理。

---

# 4. 工程目录

```text
Assets/GameScripts

└── Resource
    │
    ├── Runtime
    │
    ├── Pool
    │   ├── PoolManager.cs
    │   ├── Pool.cs
    │   └── PoolItem.cs
    │
    └── ProjectAether.Resource.asmdef
```

---

# 5. 生命周期

对象创建：

```text
Prewarm

↓

创建对象

↓

进入池
```

对象使用：

```text
Spawn

↓

Active
```

对象回收：

```text
Recycle

↓

Inactive

↓

返回池
```

对象再次使用：

```text
Spawn

↓

复用
```

---

# 6. PoolRoot设计

启动时创建：

```text
[PoolRoot]
```

结构：

```text
DontDestroyOnLoad

└── PoolRoot
```

作用：

* 管理所有池对象
* 避免场景切换丢失
* 方便运行时调试

---

# 7. PoolItem设计

每个池对象挂载：

```csharp
PoolItem
```

职责：

* 记录所属对象池
* 支持回收定位

字段：

```csharp
public string PoolKey
{
    get;
    internal set;
}
```

---

# 8. Pool设计

一个资源对应一个 Pool。

例如：

```text
Effect/FireBall
```

拥有：

```text
FireBall Pool
```

管理：

```text
Inactive Queue

Active Count
```

---

# 9. PoolManager设计

管理所有 Pool。

结构：

```csharp
Dictionary<string, Pool>
```

Key：

```text
资源路径
```

Value：

```text
Pool实例
```

---

# 10. Prewarm接口

接口：

```csharp
UniTask PrewarmAsync(
    string assetPath,
    int count)
```

示例：

```csharp
await PoolManager.PrewarmAsync(
    "Effect/FireBall",
    20);
```

执行：

```text
加载Prefab

↓

实例化20个对象

↓

进入Pool
```

---

# 11. Spawn接口

接口：

```csharp
UniTask<GameObject> SpawnAsync(
    string assetPath)
```

流程：

```text
Pool存在
    ↓
取缓存对象

Pool为空
    ↓
自动扩容
```

---

# 12. Recycle接口

接口：

```csharp
void Recycle(
    GameObject instance)
```

流程：

```text
PoolItem

↓

找到Pool

↓

SetActive(false)

↓

返回队列
```

---

# 13. 自动扩容

预热：

```text
20
```

实际需求：

```text
30
```

执行：

```text
Pool为空

↓

自动Instantiate

↓

继续提供对象
```

保证：

```text
不会因为池耗尽导致游戏异常
```

---

# 14. MVP验收标准

支持：

* PoolRoot
* PoolManager
* Pool
* PoolItem
* Prewarm
* Spawn
* Recycle
* 自动扩容

不支持：

* 容量上限
* 自动收缩
* Addressables对象池
* 统计面板

---

# 15. Git提交规范

```bash
git commit -m "[Pool][Feature] Add gameobject pool system"
```

Tag：

```text
v0.1.22
```

---

# 16. 下一阶段

49_核心框架实现_PoolHandle

实现：

* 池对象生命周期管理
* 活跃对象统计
* 防止重复回收
* Pool调试信息

---

# 17. 结论

PoolManager 正式成为 Project Aether 第一个运行时对象复用系统。

整体架构升级为：

```text
Game Logic

↓

PoolManager

↓

ResourceManager

↓

ResourceCache

↓

ResourceGC

↓

Provider
```

为后续：

* SkillSystem
* EffectSystem
* BulletSystem
* MonsterSystem
* UIModule

提供统一对象复用能力。
