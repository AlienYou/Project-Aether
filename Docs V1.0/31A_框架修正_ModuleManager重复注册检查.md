# 31A_框架修正_ModuleManager重复注册检查

版本：v1.0

项目：Project Aether

状态：立即实施

---

# 问题

当前 ModuleManager.Register() 允许同一个模块被重复注册。

例如：

```csharp
ModuleManager.Register(
    new ConfigModule());

ModuleManager.Register(
    new ConfigModule());
```

系统不会报错。

将导致：

* 生命周期重复执行
* 配置重复初始化
* 资源重复初始化
* 难以定位问题

---

# 设计目标

保证：

同一种模块只能注册一次。

---

# 实现方案

通过模块类型进行检查。

例如：

```csharp
typeof(ConfigModule)
```

---

# 修改代码

文件：

ModuleManager.cs

---

新增：

```csharp
using System;
```

---

修改 Register()

```csharp
public static void Register(
    IGameModule module)
{
    if (module == null)
    {
        Log.Error(
            "[ModuleManager] Register Failed : module is null");

        return;
    }

    Type moduleType =
        module.GetType();

    foreach (var item in _modules)
    {
        if (item.GetType() == moduleType)
        {
            Log.Error(
                $"[ModuleManager] Duplicate Module : {moduleType.Name}");

            return;
        }
    }

    _modules.Add(module);

    module.Create();
}
```

---

# 验证

测试：

```csharp
ModuleManager.Register(
    new ConfigModule());

ModuleManager.Register(
    new ConfigModule());
```

---

预期日志：

```text
[ModuleManager] Duplicate Module : ConfigModule
```

---

# MVP验收标准

支持：

* null检查
* 重复注册检查
* 日志提示
* 防止重复Create()

---

# 后续优化

V2阶段：

使用：

```csharp
Dictionary<Type, IGameModule>
```

替代 List。

提升查找效率。

当前阶段无需实施。
