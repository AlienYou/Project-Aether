using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
    }
}