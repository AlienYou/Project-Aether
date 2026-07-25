using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ProjectAether.Resource.Pool
{
    public sealed class PoolHandle
    {
        private readonly GameObject _instance;
        private readonly Pool _ownerPool;

        private bool _released;

        public GameObject Instance => _instance;

        public bool IsReleased => _released;

        internal PoolHandle(Pool ownerPool, GameObject instance)
        {
            _ownerPool = ownerPool;
            _instance = instance;
        }
        
        public void Release()
        {
            if (_released)
            {
                return;
            }
            _released = true;
            _ownerPool.Recycle(Instance);
        }
    }
}