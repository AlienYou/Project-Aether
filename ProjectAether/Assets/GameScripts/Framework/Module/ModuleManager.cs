using System;
using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Framework
{
    public static class ModuleManager
    {
        private static readonly List<IGameModule> _modules = new List<IGameModule>();

        public static void Register(IGameModule module)
        {
            if (module == null)
            {
                Log.Error("ModuleManager: Attempted to register a null module.");
                return;
            }
            Type moduleType = module.GetType();
            foreach (var existingModule in _modules)
            {
                if (existingModule.GetType() == moduleType)
                {
                    Log.Error($"ModuleManager: Module of type {moduleType.Name} is already registered.");
                    return;
                }
            }

            _modules.Add(module);
            module.Create();
        }

        public static void UpdateAll()
        {
            foreach (var module in _modules)
            {
                module.Update();
            }
        }

        public static void InitializeAll()
        {
            foreach (var module in _modules)
            {
                module.Initialize();
            }
        }

        public static void ShutdownAll()
        {
            for (int i = _modules.Count - 1; i >= 0; i--)
            {
                _modules[i].Shutdown();
            }
            _modules.Clear();
        }
    }
}