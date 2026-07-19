using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource.Handles
{
    public class ResourceHandle<T> : ResourceHandle
    {
        public T Asset { get; internal set; }
        internal void SetLoaded(string path, T asset)
        {
            AssetPath = path;
            Asset = asset;
            State = ResourceHandleState.Loaded;
            Error = null;
            ReferenceCount = 1;
        }
        internal void SetFailed(string path, string error)
        {
            AssetPath = path;
            Asset = default;
            State = ResourceHandleState.Failed;
            Error = error;
            ReferenceCount = 0;
        }
    }
}
