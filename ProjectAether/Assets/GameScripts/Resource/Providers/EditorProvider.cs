using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Resource.Providers
{
    public class EditorProvider : IResourceProvider
    {
        public void Initialize()
        {
            Log.Info("EditorProvider: Initialize");
        }

        public void Shutdown()
        {
            Log.Info("EditorProvider: Shutdown");
        }
    }
}