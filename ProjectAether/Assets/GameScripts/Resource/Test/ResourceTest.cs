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
            Log.Info($"Handle Reference Count: {handle.ReferenceCount}, Can Release: {handle.CanRelease}");
            handle.Retain();
            Log.Info($"After Retain - Handle Reference Count: {handle.ReferenceCount}, Can Release: {handle.CanRelease}");
            handle.Release();
            Log.Info($"After Release - Handle Reference Count: {handle.ReferenceCount}, Can Release: {handle.CanRelease}");
            handle.Release();
            Log.Info($"After Release - Handle Reference Count: {handle.ReferenceCount}, Can Release: {handle.CanRelease}");
        }
    }
}