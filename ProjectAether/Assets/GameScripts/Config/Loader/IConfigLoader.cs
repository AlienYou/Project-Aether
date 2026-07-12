using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public interface IConfigLoader
    {
        ConfigContainer<T> LoadConfig<T>(string path) where T : IConfigRow;
    }
}
