using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Resource.Test
{
    public class ResourceTest : MonoBehaviour
    {
        async void Start()
        {
            await UniTask.NextFrame();
            var handle = await ResourceManager.LoadAsync<GameObject>("Prefabs/Player");
            Log.Info($"Loaded asset: {handle.Asset.name}, State: {handle.State}");

            GameObject gameObject = await ResourceManager.InstantiateAsync("Prefabs/Monster");
            Log.Info($"Instantiated GameObject: {gameObject.name}");
        }
    }
}