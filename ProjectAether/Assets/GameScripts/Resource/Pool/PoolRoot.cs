using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource.Pool
{
    internal static class PoolRoot
    {
        private static Transform _root;

        public static Transform Root
        {
            get
            {
                if (_root == null)
                {
                    CreateRoot();
                }
                return _root;
            }
        }

        private static void CreateRoot()
        {
            var go = new GameObject("[PoolRoot]");
            Object.DontDestroyOnLoad(go);
            _root = go.transform;
        }
    }
}