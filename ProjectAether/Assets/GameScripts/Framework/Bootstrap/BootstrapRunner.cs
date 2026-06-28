using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework
{
    /// <summary>
    /// BootstrapRunner class for running the bootstrap process.
    /// </summary>
    public class BootstrapRunner : MonoBehaviour
    {
        void Start()
        {
            Bootstrap.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            ModuleManager.UpdateAll();
        }

        void OnApplicationQuit()
        {
            Bootstrap.Shutdown();
        }
    }
}