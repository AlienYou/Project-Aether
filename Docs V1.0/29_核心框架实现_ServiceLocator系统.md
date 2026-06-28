# 29_核心框架实现_ServiceLocator系统

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

实现 Project Aether 服务定位器（Service Locator）。

用于统一管理：

* ConfigManager
* ResourceManager
* NetworkManager
* UIManager

等全局服务。

---

# 2. 所属程序集

```text
Game.Framework
```

---

# 3. 目录结构

```text
Assets
└── GameScripts
    └── Framework
        └── ServiceLocator
            ├── IService.cs
            ├── ServiceLocator.cs
            └── Test
                ├── TestService.cs
                └── ServiceLocatorTest.cs
```

---

# 4. 架构设计

采用：

```text
Service Locator Pattern
```

结构：

```text
ServiceLocator
        │
        ├── ConfigManager
        ├── ResourceManager
        ├── NetworkManager
        └── UIManager
```

---

# 5. IService

文件：

```text
IService.cs
```

代码：

```csharp
namespace ProjectAether.Framework
{
    public interface IService
    {
    }
}
```

---

# 6. ServiceLocator实现

文件：

```text
ServiceLocator.cs
```

代码：

```csharp
using System;
using System.Collections.Generic;

namespace ProjectAether.Framework
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, IService>
            Services = new();

        public static void Register<T>(T service)
            where T : class, IService
        {
            Type type = typeof(T);

            if (Services.ContainsKey(type))
            {
                throw new Exception(
                    $"Service Already Registered : {type.Name}");
            }

            Services.Add(type, service);
        }

        public static T Get<T>()
            where T : class, IService
        {
            Type type = typeof(T);

            if (Services.TryGetValue(
                    type,
                    out IService service))
            {
                return service as T;
            }

            throw new Exception(
                $"Service Not Found : {type.Name}");
        }

        public static bool TryGet<T>(
            out T service)
            where T : class, IService
        {
            Type type = typeof(T);

            if (Services.TryGetValue(
                    type,
                    out IService instance))
            {
                service = instance as T;
                return true;
            }

            service = null;
            return false;
        }

        public static void UnRegister<T>()
            where T : class, IService
        {
            Services.Remove(typeof(T));
        }

        public static void Clear()
        {
            Services.Clear();
        }
    }
}
```

---

# 7. 测试服务

文件：

```text
TestService.cs
```

代码：

```csharp
using ProjectAether.Framework;

namespace ProjectAether.Test
{
    public class TestService : IService
    {
        public string Name => "TestService";
    }
}
```

---

# 8. 测试脚本

文件：

```text
ServiceLocatorTest.cs
```

代码：

```csharp
using UnityEngine;
using ProjectAether.Framework;
using ProjectAether.Core;
using ProjectAether.Test;

public class ServiceLocatorTest : MonoBehaviour
{
    private void Start()
    {
        TestService service =
            new TestService();

        ServiceLocator.Register(service);

        TestService result =
            ServiceLocator.Get<TestService>();

        Log.Info(
            $"Service : {result.Name}");
    }
}
```

---

# 9. Unity测试步骤

创建空物体：

```text
ServiceLocatorTester
```

挂载：

```text
ServiceLocatorTest
```

运行项目。

---

# 10. 预期输出

```text
[INFO]
Service : TestService
```

控制台无报错。

---

# 11. 实际项目应用

## 注册ConfigManager

```csharp
ServiceLocator.Register(
    new ConfigManager());
```

获取：

```csharp
ConfigManager config =
    ServiceLocator.Get<ConfigManager>();
```

---

## 注册ResourceManager

```csharp
ServiceLocator.Register(
    new ResourceManager());
```

获取：

```csharp
ResourceManager resource =
    ServiceLocator.Get<ResourceManager>();
```

---

## 注册NetworkManager

```csharp
ServiceLocator.Register(
    new NetworkManager());
```

获取：

```csharp
NetworkManager network =
    ServiceLocator.Get<NetworkManager>();
```

---

## 注册UIManager

```csharp
ServiceLocator.Register(
    new UIManager());
```

获取：

```csharp
UIManager ui =
    ServiceLocator.Get<UIManager>();
```

---

# 12. MVP验收标准

支持：

```csharp
Register<T>()

Get<T>()

TryGet<T>()

UnRegister<T>()

Clear()
```

支持：

* ConfigManager
* ResourceManager
* NetworkManager
* UIManager

注册与获取。

---

# 13. Git提交规范

提交：

```text
[Feature] Add ServiceLocator
```

Tag：

```text
v0.1.2
```

---

# 14. 后续升级计划（暂不实现）

V2：

* 生命周期管理
* 自动注册
* Service初始化顺序

V3：

* IOC容器
* 依赖注入
* 自动发现服务

V4：

* 热重载支持
* 编辑器调试工具

---

# 15. 文档关联

上游：

```text
28_核心框架实现_EventBus系统.md
```

下游：

```text
30_核心框架实现_FSM状态机框架.md
```

---

# 16. 结论

ServiceLocator 是 Project Aether 的服务管理中心。

负责：

* 服务注册
* 服务获取
* 生命周期入口

为后续：

* ConfigManager
* ResourceManager
* NetworkManager
* UIManager

提供统一访问方式。
