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

        public void Shutdown()
        {
            Log.Info("EditorProvider: Shutdown");
        }
    }
}