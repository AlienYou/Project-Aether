using System.Collections;
using System.Collections.Generic;
using UnityEditor.Analytics;
using UnityEngine;

namespace ProjectAether.Resource.Handles
{
    public class ResourceHandle<T> : ResourceHandleBase where T : Object
    {
        public T Asset { get; protected set; }
        public override Object AssetObject
        {
            get
            {
                return Asset;
            }
        }

        internal void SetLoaded(AssetKey assetKey, T asset)
        {
            AssetKey = assetKey;
            Asset = asset;
            State = ResourceHandleState.Loaded;
            Error = null;
            Retain();
        }
        internal void SetFailed(AssetKey assetKey, string error)
        {
            AssetKey = assetKey;
            Asset = default;
            State = ResourceHandleState.Failed;
            Error = error;
        }
    }
}
