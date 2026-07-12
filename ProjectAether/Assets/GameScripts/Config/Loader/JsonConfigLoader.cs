using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public class JsonConfigLoader : IConfigLoader 
    {
        public ConfigContainer<T> LoadConfig<T>(string path) where T : IConfigRow
        {
            ConfigContainer<T> container = new ConfigContainer<T>();
            /*
            * V1阶段
            *
            * 仅保留框架
            *
            * 后续接入： 
            *
            * Newtonsoft.Json
            *
            * 或 Unity JsonUtility
            */
            return container;
        }
    }
}