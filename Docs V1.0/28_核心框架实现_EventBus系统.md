# 28_核心框架实现_EventBus系统

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 所属程序集

Game.Framework

---

# 2. 目录结构

Assets/GameScripts/Framework/EventBus

├── IEvent.cs

├── EventBus.cs

└── EventBusTest.cs

---

# 3. IEvent

事件标记接口。

创建：

IEvent.cs

```csharp
namespace ProjectAether.Framework
{
    public interface IEvent
    {
    }
}
```

---

# 4. EventBus

创建：

EventBus.cs

```csharp
using System;
using System.Collections.Generic;

namespace ProjectAether.Framework
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate>
            _eventTable =
                new();

        public static void Subscribe<T>(
            Action<T> listener)
            where T : IEvent
        {
            Type eventType = typeof(T);

            if (_eventTable.TryGetValue(
                    eventType,
                    out Delegate existing))
            {
                _eventTable[eventType] =
                    Delegate.Combine(
                        existing,
                        listener);
            }
            else
            {
                _eventTable.Add(
                    eventType,
                    listener);
            }
        }

        public static void UnSubscribe<T>(
            Action<T> listener)
            where T : IEvent
        {
            Type eventType = typeof(T);

            if (_eventTable.TryGetValue(
                    eventType,
                    out Delegate existing))
            {
                Delegate current =
                    Delegate.Remove(
                        existing,
                        listener);

                if (current == null)
                {
                    _eventTable.Remove(
                        eventType);
                }
                else
                {
                    _eventTable[eventType] =
                        current;
                }
            }
        }

        public static void Publish<T>(
            T eventData)
            where T : IEvent
        {
            Type eventType = typeof(T);

            if (_eventTable.TryGetValue(
                    eventType,
                    out Delegate callback))
            {
                (callback as Action<T>)
                    ?.Invoke(eventData);
            }
        }

        public static void Clear()
        {
            _eventTable.Clear();
        }
    }
}
```

---

# 5. 测试事件

创建：

PlayerDeadEvent.cs

```csharp
namespace ProjectAether.Framework
{
    public struct PlayerDeadEvent
        : IEvent
    {
        public int PlayerId;

        public PlayerDeadEvent(
            int playerId)
        {
            PlayerId = playerId;
        }
    }
}
```

---

# 6. 测试脚本

创建：

EventBusTest.cs

```csharp
using UnityEngine;
using ProjectAether.Framework;
using ProjectAether.Core;

public class EventBusTest
    : MonoBehaviour
{
    private void Start()
    {
        EventBus.Subscribe<PlayerDeadEvent>(
            OnPlayerDead);

        EventBus.Publish(
            new PlayerDeadEvent(1));
    }

    private void OnPlayerDead(
        PlayerDeadEvent evt)
    {
        Log.Info(
            $"Player Dead : {evt.PlayerId}");
    }

    private void OnDestroy()
    {
        EventBus.UnSubscribe<
            PlayerDeadEvent>(
            OnPlayerDead);
    }
}
```

---

# 7. 测试步骤

创建空物体：

EventBusTester

---

挂载：

EventBusTest

---

点击运行。

---

# 8. 预期输出

```text
[INFO]
Player Dead : 1
```

---

# 9. 使用示例

角色死亡：

```csharp
EventBus.Publish(
    new PlayerDeadEvent(
        playerId));
```

---

UI监听：

```csharp
EventBus.Subscribe<
    PlayerDeadEvent>(
    ShowDeadWindow);
```

---

# 10. MVP验收标准

支持：

Subscribe<T>()

Publish<T>()

UnSubscribe<T>()

---

支持：

多个监听者

---

支持：

结构体事件

---

支持：

类事件

---

# 11. Git提交

```text
[Feature] Add EventBus
```

---

# 12. 文档关联

上游：

27_核心框架实现_Log系统.md

---

下游：

29_核心框架实现_ServiceLocator系统.md

---

# 13. 结论

EventBus 是 Project Aether 的消息中枢。

后续：

UI

Combat

Skill

AI

Network

均通过 EventBus 解耦通信。

```
```
