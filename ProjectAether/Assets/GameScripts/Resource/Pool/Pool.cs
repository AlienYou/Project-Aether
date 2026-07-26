using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using ProjectAether.Resource.Handles;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.XR;

namespace ProjectAether.Resource.Pool
{
    internal sealed class Pool
    {
        public AssetKey AssetKey { get; }
        private readonly Queue<GameObject> _inactiveObjects = new();

        private readonly string _poolKey;
        private readonly ResourceHandle<GameObject> _prefabHandle;
        private readonly Transform _root;

        public Pool(AssetKey key, ResourceHandle<GameObject> handle)
        {
            _poolKey = key.Value;
            _prefabHandle = handle;

            // Pool持有Prefab引用
            _prefabHandle.Retain();

            var root = new GameObject(_poolKey);

            root.transform.SetParent(PoolRoot.Root);

            _root = root.transform;
        }

        public PoolHandle Spawn()
        {
            GameObject instance;
            if (_inactiveObjects.Count > 0)
            {
                instance = _inactiveObjects.Dequeue();
            }
            else
            {
                instance = CreateInstance();
            }
            instance.SetActive(true);
            return new PoolHandle(this, instance);
        }

        public void Recycle(GameObject instance)
        {
            instance.SetActive(false);

            instance.transform.SetParent(_root);

            _inactiveObjects.Enqueue(instance);
        }

        public void Prewarm(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                var obj = CreateInstance();
                obj.SetActive(false);
                _inactiveObjects.Enqueue(obj);
            }
        }

        private GameObject CreateInstance()
        {
            var instance = Object.Instantiate(_prefabHandle.Asset, _root);
            var item = instance.GetComponent<PoolItem>();

            if (item == null)
            {
                item = instance.AddComponent<PoolItem>();
            }
            item.PoolKey = _poolKey;
            return instance;
        }

        public void Dispose()
        {
            while (_inactiveObjects.Count > 0)
            {
                var obj = _inactiveObjects.Dequeue();
                Object.Destroy(obj);
            }
            // pool释放prefab引用
            _prefabHandle.Release();
        }
    }
}
