using System.Collections;
using System.Collections.Generic;
using ProjectAether.Resource.Handles;
using UnityEngine;

namespace ProjectAether.Resource
{
    internal readonly struct ResourceKey
    {
        public readonly string AssetPath;
        public readonly System.Type AssetType;

        public ResourceKey(string assetPath, System.Type assetType)
        {
            AssetPath = assetPath;
            AssetType = assetType;
        }

        public override int GetHashCode()
        {
            return AssetPath.GetHashCode() ^ AssetType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ResourceKey other)
            {
                return AssetPath == other.AssetPath && AssetType == other.AssetType;
            }
            return false;
        }
    }
    /// <summary>
    /// ResourceCache is a MonoBehaviour that can be attached to a GameObject in the scene.
    /// It can be used to manage and cache resources during runtime.
    /// </summary>
    internal static class ResourceCache
    {
        private static readonly Dictionary<ResourceKey, ResourceHandle> _cache = new Dictionary<ResourceKey, ResourceHandle>();

        public static void Add(ResourceKey key, ResourceHandle handle)
        {
            _cache[key] = handle;
        }

        public static bool TryGet(ResourceKey key, out ResourceHandle handle)
        {
            return _cache.TryGetValue(key, out handle);
        }

        public static void Clear()
        {
            _cache.Clear();
        }
    }
}