using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Framework
{
    /// <summary>
    /// Bootstrap class for initializing the framework.
    /// </summary>
    public static class Bootstrap
    {
        public static void Initialize()
        {
            // Initialization logic for the framework
            Log.Info("Bootstrap: Initialize");
            EventBus.Clear();
            ServiceLocator.Clear();
            RegisterModules();
            ModuleManager.InitializeAll();
        }

        private static void RegisterModules()
        {
            //Test
            ModuleManager.Register(new Test.TestModule());

            // 后续注册：
            // ConfigModule
            // ResourceModule
            // UIModule
            // NetworkModule
        }

        public static void Shutdown()
        {
            // Shutdown logic for the framework
            ModuleManager.ShutdownAll();
            Log.Info("Bootstrap: Shutdown");
        }
    }
}