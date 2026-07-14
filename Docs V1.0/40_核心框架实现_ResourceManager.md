# 40_核心框架实现_ResourceManager

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

状态：设计阶段

关联文档：

39_核心框架实现_ResourceModule

ADR-004_程序集与命名空间统一规范

---

# 1. 文档目标

建立 Project Aether 统一资源访问入口。

ResourceManager 是整个资源系统的唯一对外入口。

未来：

* Config
* UI
* Character
* Combat
* Audio
* VFX
* Scene

全部通过 ResourceManager 访问资源。

---

# 2. 设计原则

禁止：

```csharp
Addressables.LoadAssetAsync()

Resources.Load()

AssetBundle.LoadAsset()
```

直接出现在业务代码中。

---

允许：

```csharp
await ResourceManager.LoadAsync<T>();
```

---

# 3. 职责

负责：

* 资源加载
* 资源实例化
* 资源释放
* Provider切换

不负责：

* Addressables实现细节
* AssetBundle实现细节

---

# 4. 所属程序集

```text
ProjectAether.Resource
```

---

# 5. 工程目录

```text
Assets/GameScripts/Resource

├── Module
├── Runtime
│   └── ResourceManager.cs
│
├── Providers
├── Handles
└── ProjectAether.Resource.asmdef
```

---

# 6. 当前阶段目标（MVP）

本阶段仅实现：

```csharp
Initialize()

Shutdown()
```

暂不实现：

```csharp
LoadAsync<T>()

InstantiateAsync()

Release()
```

原因：

IResourceProvider 还未实现。

---

# 7. ResourceManager职责

ResourceManager 作为资源系统统一入口。

负责：

```text
资源系统初始化

资源系统关闭

Provider管理
```

---

# 8. 生命周期

启动：

```text
ResourceModule

↓

ResourceManager.Initialize()
```

---

关闭：

```text
ResourceModule

↓

ResourceManager.Shutdown()
```

---

# 9. 第一阶段接口

```csharp
public static class ResourceManager
{
    public static bool IsInitialized
    {
        get;
        private set;
    }

    public static void Initialize()
    {
    }

    public static void Shutdown()
    {
    }
}
```

---

# 10. 初始化规范

Initialize 必须保证：

```text
只允许执行一次
```

重复调用：

```csharp
ResourceManager.Initialize();

ResourceManager.Initialize();
```

不得产生异常。

---

# 11. 关闭规范

Shutdown 必须保证：

```text
允许重复调用
```

例如：

```csharp
ResourceManager.Shutdown();

ResourceManager.Shutdown();
```

安全返回。

---

# 12. 日志规范

初始化：

```text
[ResourceManager] Initialize
```

---

关闭：

```text
[ResourceManager] Shutdown
```

---

# 13. MVP验收标准

支持：

* ResourceManager创建
* Initialize
* Shutdown
* 重复调用保护
* 生命周期日志

---

# 14. Git提交规范

Commit：

```bash
[Resource][Feature] Add ResourceManager
```

---

Tag：

```text
v0.1.13
```

---

# 15. 下一阶段

41_核心框架实现_IResourceProvider

实现：

```csharp
LoadAsync<T>()

InstantiateAsync()

Release()
```

并接入：

```text
EditorProvider
```

---

# 16. 结论

ResourceManager 是 Project Aether 资源系统统一入口。

当前阶段先建立生命周期管理。

资源加载能力将在后续 Provider 架构完成后接入。
