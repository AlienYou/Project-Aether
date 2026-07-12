using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public class BinaryConfigLoader : IConfigLoader
    {
        public ConfigContainer<T> LoadConfig<T>(string path) where T : IConfigRow
        {
            ConfigContainer<T> container = new();
            /*
            * V1阶段
            *
            * 仅保留框架
            *
            * 后续接入： 
            *
            * BinaryReader
            *
            * MessagePack
            *
            * ProtoBuf
            */
            return container;
        }
    }
}