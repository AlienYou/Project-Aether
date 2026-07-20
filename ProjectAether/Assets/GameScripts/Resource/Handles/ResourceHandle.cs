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
    public abstract class ResourceHandle
    {
        public string AssetPath { get; protected set; }
        public ResourceHandleState State { get; protected set; } = ResourceHandleState.None;
        public string Error { get; protected set; }
        public int ReferenceCount { get; private set; }
        public bool CanRelease => ReferenceCount <= 0;
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