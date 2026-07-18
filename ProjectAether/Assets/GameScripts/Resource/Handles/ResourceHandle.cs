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
        public virtual void Release()
        {
            Error = null;
            State = ResourceHandleState.Released;
        }
    }
}