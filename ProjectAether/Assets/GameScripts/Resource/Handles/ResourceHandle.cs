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
        Released,
    }
    public abstract class ResourceHandle
    {
        public string AssetPath { get; protected set; }
        public ResourceHandleState State { get; protected set; } = ResourceHandleState.None;
        public virtual void Release()
        {
            State = ResourceHandleState.Released;
        }
    }
}