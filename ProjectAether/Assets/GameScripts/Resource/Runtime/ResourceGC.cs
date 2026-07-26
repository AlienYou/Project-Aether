using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using ProjectAether.Resource.Handles;
using UnityEngine;
using UnityEngine.XR;

namespace ProjectAether.Resource
{
    internal static class ResourceGC
    {
        internal static readonly List<ResourceHandleBase> _pendingRelease = new();

        public static int count
        {
            get
            {
                return _pendingRelease.Count;
            }
        }

        public static void MarkForRelease(ResourceHandleBase handle)
        {
            if (handle == null)
            {
                return;
            }
            if (!handle.CanRelease)
            {
                return;
            }
            if (_pendingRelease.Contains(handle))
            {
                return;
            }
            _pendingRelease.Add(handle);
        }

        public static void Update()
        {
            for (int i = _pendingRelease.Count - 1; i >= 0; --i)
            {
                var handle = _pendingRelease[i];
                if (!handle.CanRelease)
                {
                    _pendingRelease.RemoveAt(i);
                    continue;
                }
                ReleaseInternal(handle);
                _pendingRelease.RemoveAt(i);
            }
        }

        private static void ReleaseInternal(ResourceHandleBase handle)
        {
            ResourceCache.Remove(handle);
            ResourceManager.Provider.Release(handle);
        }
    }
}