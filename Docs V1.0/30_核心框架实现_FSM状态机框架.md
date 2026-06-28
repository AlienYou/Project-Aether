# 30_核心框架实现_FSM状态机框架

版本：v1.0

项目：Project Aether

引擎版本：Unity 2022.3.51f1c1

文档状态：开发实施版

---

# 1. 文档目标

实现 Project Aether 通用状态机框架。

用于驱动：

* Character FSM
* Monster FSM
* Boss FSM
* Skill FSM
* AI FSM

---

# 2. 所属程序集

Game.Framework

---

# 3. 目录结构

Assets
└── GameScripts
└── Framework
└── FSM
├── IState.cs
├── StateMachine.cs
├── FSMOwner.cs
└── Test
├── IdleState.cs
├── MoveState.cs
└── FSMTest.cs

---

# 4. 架构设计

采用：

Finite State Machine

结构：

FSMOwner
│
▼
StateMachine
│
┌──┴──┐
▼     ▼
Idle  Move

---

# 5. IState接口

文件：

IState.cs

代码：

```csharp
namespace ProjectAether.Framework
{
    public interface IState
    {
        void Enter();

        void Update();

        void Exit();
    }
}
```

---

# 6. StateMachine

文件：

StateMachine.cs

代码：

```csharp
using System.Collections.Generic;

namespace ProjectAether.Framework
{
    public class StateMachine
    {
        private readonly Dictionary<
            System.Type,
            IState> _states =
                new();

        private IState _currentState;

        public IState CurrentState =>
            _currentState;

        public void AddState<T>(
            T state)
            where T : IState
        {
            _states[typeof(T)] =
                state;
        }

        public void ChangeState<T>()
            where T : IState
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            if (_states.TryGetValue(
                    typeof(T),
                    out IState state))
            {
                _currentState = state;

                _currentState.Enter();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}
```

---

# 7. FSMOwner

文件：

FSMOwner.cs

代码：

```csharp
namespace ProjectAether.Framework
{
    public abstract class FSMOwner
    {
        protected readonly StateMachine
            StateMachine =
                new();

        public virtual void Update()
        {
            StateMachine.Update();
        }
    }
}
```

---

# 8. IdleState

文件：

IdleState.cs

代码：

```csharp
using ProjectAether.Core;

namespace ProjectAether.Framework.Test
{
    public class IdleState : IState
    {
        public void Enter()
        {
            Log.Info(
                "Enter Idle");
        }

        public void Update()
        {
        }

        public void Exit()
        {
            Log.Info(
                "Exit Idle");
        }
    }
}
```

---

# 9. MoveState

文件：

MoveState.cs

代码：

```csharp
using ProjectAether.Core;

namespace ProjectAether.Framework.Test
{
    public class MoveState : IState
    {
        public void Enter()
        {
            Log.Info(
                "Enter Move");
        }

        public void Update()
        {
        }

        public void Exit()
        {
            Log.Info(
                "Exit Move");
        }
    }
}
```

---

# 10. FSM测试脚本

文件：

FSMTest.cs

代码：

```csharp
using UnityEngine;

namespace ProjectAether.Framework.Test
{
    public class FSMTest
        : MonoBehaviour
    {
        private StateMachine _fsm;

        private void Start()
        {
            _fsm =
                new StateMachine();

            _fsm.AddState(
                new IdleState());

            _fsm.AddState(
                new MoveState());

            _fsm.ChangeState<
                IdleState>();

            _fsm.ChangeState<
                MoveState>();
        }

        private void Update()
        {
            _fsm?.Update();
        }
    }
}
```

---

# 11. Unity测试步骤

创建：

FSMTester

挂载：

FSMTest

运行。

---

# 12. 预期输出

```text
[INFO] Enter Idle

[INFO] Exit Idle

[INFO] Enter Move
```

---

# 13. 实际项目应用

角色状态：

```text
Idle
Move
Attack
Hit
Dead
```

---

怪物状态：

```text
Idle
Patrol
Chase
Attack
Return
Dead
```

---

Boss状态：

```text
Phase1
Phase2
Phase3
Dead
```

---

# 14. MVP验收标准

支持：

AddState()

ChangeState()

Update()

Enter()

Exit()

---

支持：

多状态切换

---

支持：

角色FSM

怪物FSM

BossFSM

复用

---

# 15. Git提交规范

提交：

```text
[Feature] Add FSM Framework
```

Tag：

```text
v0.1.3
```

---

# 16. 后续升级计划（暂不实现）

V2：

* StateID
* PreviousState
* 状态切换事件

V3：

* 参数化状态
* 条件切换

V4：

* PushDown FSM

V5：

* HFSM（层级状态机）

---

# 17. 文档关联

上游：

29_核心框架实现_ServiceLocator系统.md

下游：

31_核心框架实现_Bootstrap启动框架.md

---

# 18. 结论

FSM是 Project Aether 所有玩法逻辑的基础。

后续：

Character

Monster

Boss

Skill

AI

全部基于此框架扩展实现。
