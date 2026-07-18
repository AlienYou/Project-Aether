using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource
{
    public interface IResourceProvider
    {
        void Initialize();
        void Shutdown();
    }
}