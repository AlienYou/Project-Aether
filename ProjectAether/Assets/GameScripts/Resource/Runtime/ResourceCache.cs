using System;
using System.Collections;
using System.Collections.Generic;
using ProjectAether.Resource.Handles;
using UnityEngine;

namespace ProjectAether.Resource
{
    internal readonly struct ResourceKey : IEquatable<ResourceKey>
    {
        public readonly string AssetPath;
        public readonly System.Type AssetType;

        public ResourceKey(string assetPath, System.Type assetType)
        {
            AssetPath = assetPath;
            AssetType = assetType;
        }

        public bool Equals(ResourceKey other)
        {
            return AssetPath == other.AssetPath && AssetType == other.AssetType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AssetPath, AssetType);
        }

        public override bool Equals(object obj)
        {
            return obj is ResourceKey other && this.Equals(other);
        }

        public static bool operator ==(ResourceKey left, ResourceKey right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(ResourceKey left, ResourceKey right)
        {
            return !left.Equals(right);
        }
    }
    /// <summary>
    /// ResourceCache is a MonoBehaviour that can be attached to a GameObject in the scene.
    /// It can be used to manage and cache resources during runtime.
    /// </summary>
    internal static class ResourceCache
    {
        private static readonly Dictionary<ResourceKey, ResourceHandleBase> _cache = new Dictionary<ResourceKey, ResourceHandleBase>();
        
        public static int count
        {
            get
            {
                return _cache.Count;
            }
        }

        public static bool Add(ResourceKey key, ResourceHandleBase handle)
        {
            return _cache.TryAdd(key, handle);
        }

        public static bool TryGet(ResourceKey key, out ResourceHandleBase handle)
        {
            return _cache.TryGetValue(key, out handle);
        }

        public static void Clear()
        {
            _cache.Clear();
        }

        public static void Remove(ResourceHandleBase handle)
        {
            ResourceKey foundKey = default;
            bool found = false;
            foreach (var pair in _cache)
            {
                if (ReferenceEquals(pair.Value, handle))
                {
                    foundKey = pair.Key;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                _cache.Remove(foundKey);
            }
        }
    }
}