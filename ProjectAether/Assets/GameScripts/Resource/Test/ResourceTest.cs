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
            var handle = await ResourceManager.LoadAsync<GameObject>(AssetKeys.Character.Hero);
            Log.Info($"Loaded asset: {handle.Asset.name}, State: {handle.State}");

            Log.Info($"Handle Reference Count: {handle.ReferenceCount}, Can Release: {handle.CanRelease}");
            handle.Retain();
            Log.Info($"After Retain - Handle Reference Count: {handle.ReferenceCount}, Can Release: {handle.CanRelease}");
            handle.Release();
            Log.Info($"After Release - Handle Reference Count: {handle.ReferenceCount}, Can Release: {handle.CanRelease}");
            handle.Release();
            Log.Info($"After Release - Handle Reference Count: {handle.ReferenceCount}, Can Release: {handle.CanRelease}");

            var h1 = await ResourceManager.LoadAsync<GameObject>(AssetKeys.Character.Hero);
            var h2 = await ResourceManager.LoadAsync<GameObject>(AssetKeys.Character.Hero);
            Log.Info(h1 == h2 ? "Handles are the same instance." : "Handles are different instances.");

            //Release Test
            // var h3 = await ResourceManager.LoadAsync<GameObject>("Prefabs/Player");
            // Log.Info($"Before Release RefCount: {h3.ReferenceCount}");
            // h3.Release();
            // Log.Info($"After Release RefCount: {h3.ReferenceCount}");
            // Log.Info($"Before Pending:{ResourceGC.count} {ResourceCache.count}");
            // await UniTask.NextFrame();
            // Log.Info($"After Pending:{ResourceGC.count} {ResourceCache.count}");
        }
    }
}