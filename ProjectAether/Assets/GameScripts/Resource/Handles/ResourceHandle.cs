using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource.Handles
{
    public enum ResourceHandleState
    {
        None,
        Loading,
        Loaded,
        Failed,
        Released,
    }
    public abstract class ResourceHandleBase
    {
        protected AssetKey AssetKey { get; private protected set; }
        public string AssetPath => AssetKey.Value;
        public ResourceHandleState State { get; protected set; } = ResourceHandleState.None;
        public string Error { get; protected set; }
        public int ReferenceCount { get; protected set; }
        public bool CanRelease => ReferenceCount <= 0;
        public abstract UnityEngine.Object AssetObject
        {
            get;
        }

        public virtual void Release()
        {
            if (ReferenceCount > 0)
            {
                ReferenceCount--;
            }
            if (ReferenceCount <= 0)
            {
                State = ResourceHandleState.Released;
                ResourceGC.MarkForRelease(this);
            }
        }
        public virtual void Retain()
        {
            ReferenceCount++;
        }
    }
}