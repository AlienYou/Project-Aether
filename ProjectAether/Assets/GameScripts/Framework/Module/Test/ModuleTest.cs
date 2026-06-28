using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework.Test
{
    public class ModuleTest : MonoBehaviour
    {
        void Start()
        {
            ModuleManager.Register(new TestModule());
            ModuleManager.InitializeAll();
        }

        void Update()
        {
            ModuleManager.UpdateAll();
        }
        
        void OnDestroy()
        {
            ModuleManager.ShutdownAll();
        }
    }
}