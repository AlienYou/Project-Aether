using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProjectAether.Core;
using ProjectAether.Resource.Handles;
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

        public static UniTask<ResourceHandle<T>> LoadAsync<T>(string assetPath) where T : Object
        {
            if (!IsInitialized)
            {
                Log.Error("[ResourceManager] Not initialized.");
                throw new System.Exception("ResourceManager not initialized.");
            }
            return _provider.LoadAsync<T>(assetPath);
        }

        public static UniTask<GameObject> InstantiateAsync(string assetPath)
        {
            if (!IsInitialized)
            {
                Log.Error("[ResourceManager] Not initialized.");
                throw new System.Exception("ResourceManager not initialized.");
            }
            return _provider.InstantiateAsync(assetPath);
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
