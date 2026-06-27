# 27_核心框架实现_Log系统

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 目标

实现统一日志系统：

Log.Info()

Log.Warning()

Log.Error()

Log.Exception()

---

替代：

Debug.Log()

Debug.LogWarning()

Debug.LogError()

---

# 2. 所属程序集

Game.Core

---

目录：

Assets/GameScripts/Core/Log

---

# 3. 文件结构

Log

├── Log.cs

├── LogLevelEnum.cs

└── LogConfig.cs

---

# 4. 日志等级

创建：

LogLevelEnum.cs

```csharp
namespace ProjectAether.Core
{
    public enum LogLevelEnum
    {
        Info,
        Warning,
        Error
    }
}
```

---

# 5. 日志配置

创建：

LogConfig.cs

```csharp
namespace ProjectAether.Core
{
    public static class LogConfig
    {
        public static bool EnableLog = true;

        public static LogLevelEnum Level =
            LogLevelEnum.Info;
    }
}
```

---

# 6. Log主类

创建：

Log.cs

```csharp
using UnityEngine;

namespace ProjectAether.Core
{
    public static class Log
    {
        public static void Info(object message)
        {
            if (!LogConfig.EnableLog)
                return;

            if (LogConfig.Level >
                LogLevelEnum.Info)
                return;

            Debug.Log(
                $"[INFO] {message}");
        }

        public static void Warning(
            object message)
        {
            if (!LogConfig.EnableLog)
                return;

            if (LogConfig.Level >
                LogLevelEnum.Warning)
                return;

            Debug.LogWarning(
                $"[WARNING] {message}");
        }

        public static void Error(
            object message)
        {
            if (!LogConfig.EnableLog)
                return;

            if (LogConfig.Level >
                LogLevelEnum.Error)
                return;

            Debug.LogError(
                $"[ERROR] {message}");
        }

        public static void Exception(
            System.Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
```

---

# 7. 测试脚本

创建：

LogTest.cs

```csharp
using UnityEngine;
using ProjectAether.Core;

public class LogTest : MonoBehaviour
{
    private void Start()
    {
        Log.Info("Game Start");

        Log.Warning("Config Missing");

        Log.Error("Load Failed");
    }
}
```

---

# 8. 测试步骤

创建：

LogTestObject

挂载：

LogTest

---

运行后输出：

```text
[INFO] Game Start

[WARNING] Config Missing

[ERROR] Load Failed
```

---

# 9. 推荐扩展

第二阶段：

增加模块标签

例如：

```csharp
Log.Info(
    "Combat",
    "Damage = 100");
```

输出：

```text
[Combat][INFO]
Damage = 100
```

---

# 10. 推荐扩展

增加颜色输出：

```text
Info
白色

Warning
黄色

Error
红色
```

---

# 11. 推荐扩展

增加时间戳：

```text
[14:35:21]
[INFO]
Player Spawn
```

---

# 12. 推荐扩展

增加文件输出：

Logs/

Game.log

Combat.log

Network.log

````

---

# 13. 推荐扩展

增加运行环境控制：

```csharp
#if UNITY_EDITOR
#endif
````

---

Release关闭：

Info日志

---

# 14. 推荐扩展

支持：

```csharp
Log.Combat()

Log.Network()

Log.UI()

Log.Skill()
```

---

# 15. MVP验收标准

完成：

Log.Info()

Log.Warning()

Log.Error()

Log.Exception()

---

运行成功

无编译错误

---

# 16. Git提交

```text
[Feature] Add Log System
```

---

# 17. 文档关联

上游：

26_核心框架代码规范.md

---

下游：

28_核心框架实现_EventBus系统.md

---

# 18. 结论

Log系统是 Project Aether 第一块基础设施。

后续：

EventBus

ServiceLocator

FSM

ConfigManager

ResourceManager

全部依赖 Log 进行调试与诊断。
