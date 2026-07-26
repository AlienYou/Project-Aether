using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Resource.Pool.Test
{
    public class PoolTest : MonoBehaviour
    {
        PoolHandle _handle;
        async UniTaskVoid Start()
        {
            //测试使用，要等BootStrap初始化
            await UniTask.NextFrame();
            
            _handle = await PoolManager.SpawnAsync(AssetKeys.Effect.FireBall.Value);
            Log.Info($"Spawn Instance: {_handle.Instance.name}");
            _handle.Instance.transform.position = Vector3.zero;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_handle != null)
                {
                    _handle.Release();
                    Log.Info($"Released:{_handle.IsReleased}");
                    _handle.Release();
                    Log.Info($"Released:{_handle.IsReleased}");
                }
            }
        }

        void OnDestroy()
        {
        }
    }
}