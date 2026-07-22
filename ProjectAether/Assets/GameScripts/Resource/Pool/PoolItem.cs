using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource.Pool
{
    public sealed class PoolItem : MonoBehaviour
    {
        public string PoolKey
        {
            get;
            internal set;
        }
    }
}