using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using ProjectAether.Framework;
using UnityEngine;

namespace ProjectAether.Resource
{
    public class ResourceModule : IGameModule
    {
        public string ModuleName => "Resource";

        public ModuleState State { get;  private set;} = ModuleState.None;

        public void Create()
        {
            State = ModuleState.Created;
            Log.Info($"[Resource] Create");
        }

        public void Initialize()
        {
            ResourceManager.Initialize();
            State = ModuleState.Initialized;
            Log.Info($"[Resource] Initialize");
        }

        public void Shutdown()
        {
            ResourceManager.Shutdown();
            State = ModuleState.Shutdown;
            Log.Info($"[Resource] Shutdown");
        }

        public void Update()
        {
            if (State == ModuleState.Initialized)
            {
                State = ModuleState.Running;
            }
        }
    }
}