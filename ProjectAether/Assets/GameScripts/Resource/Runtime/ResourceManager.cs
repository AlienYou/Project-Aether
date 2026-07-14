using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Resource
{
    public static class ResourceManager
    {
        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            Log.Info("ResourceManager: Initialization started.");
        }

        public static void Shutdown()
        {
            Log.Info("ResourceManager: Shutdown started.");
        }
    }
}
