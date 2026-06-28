using System.Collections;
using System.Collections.Generic;
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
                return;
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