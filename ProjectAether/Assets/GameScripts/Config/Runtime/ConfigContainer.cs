using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public class ConfigContainer<T> where T : IConfigRow
    {
        private readonly Dictionary<int, T> _configs = new();

        public int Count => _configs.Count;

        public void Add(T row)
        {
            _configs[row.Id] = row;
        }

        public T Get(int id)
        {
            _configs.TryGetValue(id, out T row);
            return row;    
        }

        public bool Contains(int id)
        {
            return _configs.ContainsKey(id);
        }

        public IReadOnlyDictionary<int, T> GetAll()
        {
            return _configs;
        }

        public void Clear()
        {
            _configs.Clear();
        }
    }
}