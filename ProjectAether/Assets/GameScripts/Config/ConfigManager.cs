using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public static class ConfigManager
    {
        private static readonly Dictionary<Type, Dictionary<int, IConfigRow>> _configTables = new ();
        public static void Initialize()
        {
            _configTables.Clear();
        }

        public static void Shutdown()
        {
            _configTables.Clear();
        }

        public static void Load<T>(List<T> configs) where T : class, IConfigRow
        {
            var table = new Dictionary<int, IConfigRow>();

            foreach (var row in configs)
            {
                table[row.Id] = row;
            }

            _configTables[typeof(T)] = table;
        }

        public static T Get<T>(int id) where T : class, IConfigRow
        {
            if (!_configTables.TryGetValue(typeof(T), out var table))
            {
                return null;
            }

            if (!table.TryGetValue(id, out var row))
            {
                return null;
            }

            return row as T;
        }
    }
}