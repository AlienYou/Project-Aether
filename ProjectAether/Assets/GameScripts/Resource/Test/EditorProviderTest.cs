using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Resource.Test
{
    public class EditorProviderTest : MonoBehaviour
    {
        async void Start()
        {
            var handle = await ResourceManager.LoadAsync<GameObject>("Prefabs/Player");
            Log.Info($"Loaded asset: {handle.Asset.name}, State: {handle.State}");
        }
    }
}