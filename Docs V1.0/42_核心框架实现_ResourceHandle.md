# 42A_ResourceHandle设计修正

版本：v1.0

项目：Project Aether

状态：修正文档

关联文档：

42_核心框架实现_ResourceHandle

43_核心框架实现_ResourceManager_LoadAsync

---

# 1. 问题说明

42文档定义：

```csharp
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
}
```

43文档中：

```csharp
handle.AssetPath = assetPath;

handle.State =
    ResourceHandleState.Loaded;
```

由于：

```csharp
protected set;
```

限制，

EditorProvider 无法访问。

将导致编译错误：

```text
CS0272
The property or indexer cannot be used in this context because the set accessor is inaccessible
```

---

# 2. 修正原则

禁止：

```csharp
public set;
```

原因：

```text
业务代码可以随意修改资源状态
破坏封装
```

---

保持：

```csharp
protected set;
```

设计不变。

---

增加：

```csharp
internal
```

初始化接口。

---

# 3. ResourceHandle<T>最终实现

文件：

```text
Assets/GameScripts/Resource/Handles/ResourceHandleT.cs
```

修改为：

```csharp
namespace ProjectAether.Resource
{
    public class ResourceHandle<T>
        : ResourceHandle
    {
        public T Asset
        {
            get;
            private set;
        }

        internal void SetLoaded(
            string assetPath,
            T asset)
        {
            AssetPath = assetPath;

            Asset = asset;

            State =
                ResourceHandleState.Loaded;
        }
    }
}
```

---

# 4. Provider修正

错误写法：

```csharp
handle.AssetPath =
    assetPath;

handle.State =
    ResourceHandleState.Loaded;
```

删除。

---

改为：

```csharp
var handle =
    new ResourceHandle<T>();

handle.SetLoaded(
    assetPath,
    asset);

return handle;
```

---

# 5. asmdef分析

当前：

```text
ProjectAether.Resource
```

包含：

```text
ResourceManager

IResourceProvider

EditorProvider

ResourceHandle
```

属于同一个程序集。

因此：

```csharp
internal
```

可正常访问。

---

程序集外：

```text
ProjectAether.UI

ProjectAether.Config

ProjectAether.Combat
```

无法调用：

```csharp
SetLoaded()
```

满足封装要求。

---

# 6. MVP验收标准

支持：

* AssetPath只读
* State只读
* Asset只读
* Provider初始化Handle

禁止：

* 业务代码修改状态
* 外部模块修改状态

---

# 7. 对43文档影响

43文档中：

```csharp
handle.AssetPath = assetPath;

handle.State =
    ResourceHandleState.Loaded;
```

全部作废。

统一替换为：

```csharp
handle.SetLoaded(
    assetPath,
    asset);
```

---

# 8. 结论

ResourceHandle保持封装性。

Provider拥有初始化权限。

业务层只有读取权限。

符合 Project Aether 工业级资源系统设计规范。
