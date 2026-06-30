using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using ProjectAether.Framework;
using UnityEngine;

namespace ProjectAether.Config
{
    /// <summary>
    /// ConfigModule is responsible for managing configuration settings for the game.
    /// </summary>
    public class ConfigModule : IGameModule
    {
        public string ModuleName => "ConfigModule";

        public ModuleState State { get; private set; } = ModuleState.None;

        public void Create()
        {
            State = ModuleState.Created;
            Log.Info("[Config] Create");
        }

        public void Initialize()
        {
            State = ModuleState.Initialized;
            Log.Info("[Config] Initialize");
        }

        public void Shutdown()
        {
            State = ModuleState.Shutdown;
            Log.Info("[Config] Shutdown");
        }

        public void Update()
        {
        }
    }
}