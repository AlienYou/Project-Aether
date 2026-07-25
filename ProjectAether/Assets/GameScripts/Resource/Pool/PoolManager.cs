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

        public static async UniTask<PoolHandle> SpawnAsync(string assetPath)
        {
            if (!Pools.TryGetValue(assetPath, out var pool))
            {
                await CreatePoolAsync(assetPath);

                pool = Pools[assetPath];
            }
            return pool.Spawn();
        }

        private static async UniTask CreatePoolAsync(string assetPath)
        {
            var handle = await ResourceManager.LoadAsync<GameObject>(assetPath);
            var pool = new Pool(assetPath, handle);
            Pools.Add(assetPath, pool);
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