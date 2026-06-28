using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework
{
    public interface IGameModule
    {
        string ModuleName { get; }
        ModuleState State { get; }
        void Create();
        void Initialize();
        void Update();
        void Shutdown();
    }
}