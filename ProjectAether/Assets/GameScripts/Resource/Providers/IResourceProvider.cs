using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProjectAether.Resource.Handles;
using UnityEngine;

namespace ProjectAether.Resource
{
    public interface IResourceProvider
    {
        void Initialize();
        void Shutdown();
        UniTask<ResourceHandle<T>> LoadAsync<T>(string assetPath) where T : UnityEngine.Object;
    }
}