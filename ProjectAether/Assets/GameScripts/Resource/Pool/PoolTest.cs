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

            var obj = await PoolManager.SpawnAsync("Effect/FireBall");

            obj.transform.position = Vector3.zero;

            await UniTask.Delay(3000);

            PoolManager.Recycle(obj);
        }

        void OnDestroy()
        {
        }
    }
}