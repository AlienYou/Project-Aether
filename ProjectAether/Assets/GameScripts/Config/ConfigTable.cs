using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public class ConfigTable<T> where T : IConfigRow
    {
        private readonly Dictionary<int, T> _rows = new();

        public void Add(T row)
        {
            _rows[row.Id] = row;
        }

        public T Get(int id)
        {
            _rows.TryGetValue(id, out T row);
            return row;
        }

        public IReadOnlyDictionary<int, T> Rows => _rows;
    }
}