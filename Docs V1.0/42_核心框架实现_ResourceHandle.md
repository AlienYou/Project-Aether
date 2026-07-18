# 42_核心框架实现_ResourceHandle

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：实施版

关联文档：

40_核心框架实现_ResourceManager

41_核心框架实现_IResourceProvider

ADR-004_程序集与命名空间统一规范

---

# 1. 文档目标

建立 Project Aether 资源句柄系统（Resource Handle System）。

本阶段目标：

* 建立 ResourceHandle 基类
* 建立资源生命周期管理入口
* 建立资源状态管理
* 为后续 Addressables 接入做好准备

本阶段不实现：

* 引用计数
* Addressables Handle封装
* 自动释放
* 资源依赖管理

---

# 2. 为什么需要Handle

错误做法：

```csharp
var prefab =
    await ResourceManager.LoadAsync<GameObject>(
        "Player");
```

业务层直接持有：

```csharp
GameObject
```

未来：

```text
无法统计资源

无法释放资源

无法做引用计数

无法检测泄漏
```

---

工业级做法：

```csharp
var handle =
    await ResourceManager.LoadAsync<GameObject>(
        "Player");
```

业务层持有：

```csharp
ResourceHandle<GameObject>
```

---

# 3. 架构定位

```text
Game Logic

↓

ResourceManager

↓

ResourceHandle<T>

↓

IResourceProvider

↓

EditorProvider

↓

AddressablesProvider（未来）
```

---

# 4. 工程目录

```text
Assets/GameScripts

└── Resource
    │
    ├── Runtime
    │   └── ResourceManager.cs
    │
    ├── Providers
    │
    ├── Handles
    │   ├── ResourceHandle.cs
    │   └── ResourceHandleT.cs
    │
    └── ProjectAether.Resource.asmdef
```

---

# 5. 生命周期设计

资源状态：

```csharp
namespace ProjectAether.Resource
{
    public enum ResourceHandleState
    {
        None,

        Loading,

        Loaded,

        Released,
    }
}
```

---

状态流转：

```text
None

↓

Loading

↓

Loaded

↓

Released
```

---

# 6. ResourceHandle基类

文件：

```text
Assets/GameScripts/Resource/Handles/ResourceHandle.cs
```

代码：

```csharp
namespace ProjectAether.Resource
{
    public abstract class ResourceHandle
    {
        public string AssetPath
        {
            get;
            protected set;
        }

        public ResourceHandleState State
        {
            get;
            protected set;
        }

        public virtual void Release()
        {
            State =
                ResourceHandleState.Released;
        }
    }
}
```

---

# 7. 泛型Handle

文件：

```text
Assets/GameScripts/Resource/Handles/ResourceHandleT.cs
```

代码：

```csharp
namespace ProjectAether.Resource
{
    public class ResourceHandle<T>
        : ResourceHandle
    {
        public T Asset
        {
            get;
            internal set;
        }
    }
}
```

---

# 8. 为什么不直接返回T

例如：

```csharp
GameObject player =
    await LoadAsync<GameObject>();
```

问题：

```text
无法知道谁持有资源

无法统计资源数量

无法统一释放
```

---

改为：

```csharp
ResourceHandle<GameObject>
```

未来：

```text
支持引用计数

支持资源分析

支持内存统计

支持自动释放
```

---

# 9. ResourceManager规划

当前：

```csharp
Initialize()

Shutdown()
```

---

下一阶段扩展：

```csharp
LoadAsync<T>()

Release()
```

返回：

```csharp
ResourceHandle<T>
```

而不是：

```csharp
T
```

---

# 10. Unity验证

测试代码：

```csharp
var handle =
    new ResourceHandle<GameObject>();
```

验证：

```text
编译通过
```

即可。

---

# 11. MVP验收标准

支持：

* ResourceHandle
* ResourceHandle<T>
* ResourceHandleState
* Release()

不支持：

* 引用计数
* 自动释放
* Addressables Handle

---

# 12. Git提交规范

Commit：

```bash
git commit -m "[Resource][Feature] Add resource handle system"
```

Tag：

```text
v0.1.15
```

---

# 13. 下一阶段

43_核心框架实现_ResourceManager_LoadAsync

实现：

```csharp
LoadAsync<T>()
```

正式打通：

```text
ResourceManager

↓

IResourceProvider

↓

ResourceHandle<T>
```

形成完整资源加载链路。

---

# 14. 结论

ResourceHandle 是 Project Aether 资源生命周期管理的基础设施。

未来所有资源加载接口统一返回：

```csharp
ResourceHandle<T>
```

而不是直接返回资源对象。

这是后续引用计数、自动释放、内存分析系统的基础。
