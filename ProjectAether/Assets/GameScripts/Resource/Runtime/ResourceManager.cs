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
            if (IsInitialized)
            {
                Log.Warning("ResourceManager: Already initialized.");
                return;
            }
            IsInitialized = true;
            Log.Info("ResourceManager: Initialize");
        }

        public static void Shutdown()
        {
            if (!IsInitialized)
            {
                Log.Warning("ResourceManager: Not initialized.");
                return;
            }
            IsInitialized = false;
            Log.Info("ResourceManager: Shutdown");
        }
    }
}
