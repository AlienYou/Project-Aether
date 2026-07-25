using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ProjectAether.Resource.Pool.Test
{
    public class PoolTest : MonoBehaviour
    {
        async UniTaskVoid Start()
        {
            //测试使用，要等BootStrap初始化
            await UniTask.NextFrame();

            await PoolManager.PrewarmAsync("Effect/FireBall", 10);

            var handle = await PoolManager.SpawnAsync("Effect/FireBall");

            handle.Instance.transform.position = Vector3.zero;

            await UniTask.Delay(3000);

            handle.Release();
        }

        void OnDestroy()
        {
        }
    }
}