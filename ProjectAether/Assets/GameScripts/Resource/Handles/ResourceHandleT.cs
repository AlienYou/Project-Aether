using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource.Handles
{
    public class ResourceHandle<T> : ResourceHandle
    {
        public T Asset { get; internal set; }
    }
}
