using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource
{
    public static class AssetKeys
    {
        public static class Effect
        {
            public static readonly AssetKey FireBall = new AssetKey("Effect/FireBall");
            public static readonly AssetKey Explosion = new AssetKey("Effect/Explosion");
        }
        public static class Character
        {
            public static readonly AssetKey Hero = new AssetKey("Character/Player");
        }
    
        public static class UI
        {
            public static readonly AssetKey MainPanel = new AssetKey("UI/MainPanel");
        }
    }
}
