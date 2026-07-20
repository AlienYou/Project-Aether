using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProjectAether.Core;
using ProjectAether.Resource.Handles;
using UnityEngine;

namespace ProjectAether.Resource.Providers
{
    public class EditorProvider : IResourceProvider
    {
        public void Initialize()
        {
            Log.Info("EditorProvider: Initialize");
        }

        public async UniTask<GameObject> InstantiateAsync(string assetPath)
        {
            var handle = await LoadAsync<GameObject>(assetPath);
            if (handle.State == ResourceHandleState.Failed)
            {
                Log.Error($"EditorProvider: InstantiateAsync failed to load asset at path: {assetPath}");
                return null;
            }
            return Object.Instantiate(handle.Asset);
        }

        public async UniTask<ResourceHandle<T>> LoadAsync<T>(string assetPath) where T : Object
        {
            await UniTask.Yield();
            //仅用于MVP验证
            T asset = Resources.Load<T>(assetPath);
            var handle = new ResourceHandle<T>()
            {
                Asset = asset
            };
            if (asset == null)
            {
                handle.SetFailed(assetPath, $"Asset Not Found: {assetPath}");
                return handle;
            }
            handle.SetLoaded(assetPath, asset);
            return handle;
        }

        public void Release(ResourceHandle handle)
        {
            //由于加载使用的是Resources.Load，无需主动释放单个资源
        }

        public void Shutdown()
        {
            Log.Info("EditorProvider: Shutdown");
        }
    }
}