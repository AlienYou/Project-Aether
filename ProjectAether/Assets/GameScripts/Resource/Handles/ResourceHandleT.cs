using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource.Handles
{
    public class ResourceHandle<T> : ResourceHandle where T : Object
    {
        private T _asset;
        public T Asset
        {
            get
            {
                return _asset;
            }
        }

        public override Object AssetObject
        {
            get
            {
                return _asset;
            }
        }

        internal void SetLoaded(string path, T asset)
        {
            AssetPath = path;
            _asset = asset;
            State = ResourceHandleState.Loaded;
            Error = null;
            Retain();
        }
        internal void SetFailed(string path, string error)
        {
            AssetPath = path;
            _asset = default;
            State = ResourceHandleState.Failed;
            Error = error;
        }
    }
}
