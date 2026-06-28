using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<System.Type, IService> services = new();

        public static void Register<T>(T service) where T : class, IService
        {
            var type = typeof(T);
            if (services.ContainsKey(type))
            {
                throw new System.Exception($"Service Already Registered : {type.Name}");
            }
            services.Add(type, service);
        }

        public static T Get<T>() where T : class, IService
        {
            var type = typeof(T);
            if (services.TryGetValue(type, out var service))
            {
                return (T)service;
            }
            throw new System.Exception($"Service Not Found : {type.Name}");
        }

        public static bool TryGet<T>(out T service) where T : class, IService
        {
            var type = typeof(T);
            if (services.TryGetValue(type, out var s))
            {
                service = (T)s;
                return true;
            }
            service = null;
            return false;
        }

        public static void Clear()
        {
            services.Clear();
        }

        public static void Unregister<T>() where T : class, IService
        {
            services.Remove(typeof(T));
        }
    }
}