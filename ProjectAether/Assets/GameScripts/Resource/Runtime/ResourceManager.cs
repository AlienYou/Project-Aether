using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Resource
{
    public static class ResourceManager
    {
        public static bool IsInitialized { get; private set; }
        private static IResourceProvider _provider;

        public static void Initialize()
        {
            if (IsInitialized)
            {
                Log.Warning("[ResourceManager] Already initialized.");
                return;
            }
            IsInitialized = true;
            _provider = new Providers.EditorProvider();
            _provider.Initialize();
            Log.Info("[ResourceManager] Initialize");
        }

        public static void Shutdown()
        {
            if (!IsInitialized)
            {
                Log.Warning("[ResourceManager] Not initialized.");
                return;
            }
            _provider?.Shutdown();
            _provider = null;
            IsInitialized = false;
            Log.Info("[ResourceManager]: Shutdown");
        }
    }
}
