using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource.Pool
{
    public sealed class PoolConfig
    {
        public AssetKey AssetKey { get; private set; }

        public int InitialSize { get; private set; }

        public int MaxSize { get; private set; }

        public bool Prewarm { get; private set; }
        
        public PoolConfig(AssetKey assetKey, int initialSize, int maxSize, bool prewarm)
        {
            AssetKey = assetKey;
            InitialSize = initialSize;
            MaxSize = maxSize;
            Prewarm = prewarm;
        }
    }
}
