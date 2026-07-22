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

        public static async UniTask PrewarmAsync(string assetPath, int count)
        {
            var handle = await ResourceManager.LoadAsync<GameObject>(assetPath);

            if (!Pools.TryGetValue(assetPath, out var pool))
            {
                pool = new Pool(assetPath, handle);
                Pools.Add(assetPath, pool);
            }

            pool.Prewarm(count);
        }

        public static async UniTask<GameObject> SpawnAsync(string assetPath)
        {
            if (!Pools.TryGetValue(assetPath, out var pool))
            {
                await PrewarmAsync(assetPath, 1);

                pool = Pools[assetPath];
            }
            return pool.Spawn();
        }

        public static void Recycle(GameObject instance)
        {
            var item = instance.GetComponent<PoolItem>();
            if (item == null)
            {
                Log.Error("Object is not Pool Object");
                return;
            }
            if (Pools.TryGetValue(item.PoolKey, out var pool))
            {
                pool.Recycle(instance);
            }
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