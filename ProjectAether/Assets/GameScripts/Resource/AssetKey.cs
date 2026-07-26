using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Resource
{
    public readonly struct AssetKey : IEquatable<AssetKey>
    {
        private readonly string _value;
        public string Value => _value;

        public AssetKey(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("AssetKey value cannot be null or empty");
            }
            _value = value;
        }

        public override string ToString()
        {
            return Value;    
        }

        public bool Equals(AssetKey other)
        {
            return string.Equals(_value, other.Value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is AssetKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value != null ? _value.GetHashCode() : 0;
        }
    
        public static bool operator ==(AssetKey left, AssetKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AssetKey left, AssetKey right)
        {
            return left.Equals(right);
        }
    }
}