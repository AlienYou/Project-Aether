using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Framework.Test
{
    public class TestModule : IGameModule
    {
        public string ModuleName => "TestModule";

        public ModuleState State { get; private set; } = ModuleState.None;

        public void Create()
        {
            State = ModuleState.Created;
            Log.Info("Create TestModule");
        }

        public void Initialize()
        {
            State = ModuleState.Initialized;
            Log.Info("Initialize TestModule");
        }

        public void Shutdown()
        {
            State = ModuleState.Shutdown;
            Log.Info("Shutdown TestModule");
        }

        public void Update()
        {
        }
    }
}
