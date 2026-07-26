using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProjectAether.Core;
using UnityEditor.VersionControl;
using UnityEngine;

namespace ProjectAether.Resource.Pool
{
    public static class PoolManager
    {
        private static readonly Dictionary<string, Pool> Pools = new();

        public static async UniTask<PoolHandle> SpawnAsync(AssetKey key)
        {
            return await SpawnAsyncInternal(key);
        }

        private static async UniTask<PoolHandle> SpawnAsyncInternal(AssetKey key)
        {
            var assetPath = key.Value;
            if (!Pools.TryGetValue(assetPath, out var pool))
            {
                await CreatePoolAsync(key);

                pool = Pools[assetPath];
            }
            return pool.Spawn();
        }

        private static async UniTask CreatePoolAsync(AssetKey key)
        {
            var handle = await ResourceManager.LoadAsync<GameObject>(key);
            var pool = new Pool(key, handle);
            Pools.Add(key.Value, pool);
        }

        public static void Clear()
        {
            foreach (var pool in Pools.Values)
            {
                pool.Dispose();
            }
            Pools.Clear();
            Log.Info("[PoolManager] Clear");
        }
    }
}