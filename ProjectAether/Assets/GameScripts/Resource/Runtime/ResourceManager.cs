using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProjectAether.Core;
using ProjectAether.Resource.Handles;
using UnityEditor.VersionControl;
using UnityEngine;

namespace ProjectAether.Resource
{
    public static class ResourceManager
    {
        public static bool IsInitialized { get; private set; }
        internal static IResourceProvider Provider
        {
            get
            {
                return _provider;
            }
        }

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

        public async static UniTask<ResourceHandle<T>> LoadAsync<T>(AssetKey assetKey) where T : UnityEngine.Object
        {
            return await LoadAsyncInternal<T>(assetKey);
        }

        [System.Obsolete("Use AssetKey instead")]
        public async static UniTask<ResourceHandle<T>> LoadAsync<T>(string path) where T : UnityEngine.Object
        {
            return await LoadAsyncInternal<T>(new AssetKey(path));
        }

        private async static UniTask<ResourceHandle<T>> LoadAsyncInternal<T>(AssetKey assetKey) where T : UnityEngine.Object
        {
            if (!IsInitialized)
            {
                Log.Error("[ResourceManager] Not initialized.");
                throw new System.Exception("ResourceManager not initialized.");
            }
            var path = assetKey.Value;
            var key = new ResourceKey(path, typeof(T));
            if (ResourceCache.TryGet(key, out var cachedHandle))
            {
                if (cachedHandle is ResourceHandle<T> typedHandle)
                {
                    typedHandle.Retain();
                    return typedHandle;
                }
                throw new InvalidOperationException($"Cache Type Mismatch : {assetKey}");
            }
            var handle = await _provider.LoadAsync<T>(path);
            switch (handle.State)
            {
                case ResourceHandleState.Loaded:
                    ResourceCache.Add(key, handle);
                    break;
                case ResourceHandleState.Failed:
                    throw new Exception($"[ResourceManager] key:{assetKey} loaded failed");
                default:
                    break;
            }
            return handle;
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
