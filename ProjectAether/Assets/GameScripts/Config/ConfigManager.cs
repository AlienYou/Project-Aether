using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public static class ConfigManager
    {
        private static readonly Dictionary<Type, object> _containers = new ();
        public static void Initialize()
        {
            _containers.Clear();
        }

        public static void Shutdown()
        {
            _containers.Clear();
        }

        public static void RegisterTable<T>(ConfigContainer<T> table) where T : class, IConfigRow
        {
            _containers[typeof(T)] = table;
        }

        public static ConfigContainer<T> GetTable<T>() where T : class, IConfigRow
        {
            if (_containers.TryGetValue(typeof(T), out var table))
            {
                return table as ConfigContainer<T>;
            }

            return null;
        }

        public static T Get<T>(int id) where T : class, IConfigRow
        {
            var table = GetTable<T>();
            if (table == null)
            {
                return default;
            }

            return table.Get(id);
        }
    }
}