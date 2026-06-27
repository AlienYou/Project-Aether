using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework
{
    public static class EventBus 
    {
        private static Dictionary<Type, Delegate> _eventTable = new ();

        public static void Subscribe<T>(System.Action<T> listener) where T : IEvent
        {
            Type eventType = typeof(T);
        
            if (_eventTable.TryGetValue(eventType, out Delegate existing))
            {
                _eventTable[eventType] = Delegate.Combine(existing, listener);
            }
            else
            {
                _eventTable.Add(eventType, listener);
            }
        }

        public static void Unsubscribe<T>(System.Action<T> listener) where T : IEvent
        {
            Type eventType = typeof(T);
        
            if (_eventTable.TryGetValue(eventType, out Delegate existing))
            {
                Delegate current = Delegate.Remove(existing, listener);
                if (current == null)
                {
                    _eventTable.Remove(eventType);
                }
                else
                {
                    _eventTable[eventType] = current;
                }
            }
        }

        public static void Publish<T>(T eventData) where T : IEvent
        {
            Type eventType = typeof(T);

            if (_eventTable.TryGetValue(eventType, out var callback))
            {
                (callback as System.Action<T>)?.Invoke(eventData);
            }
        }
    
        public static void Clear()
        {
            _eventTable.Clear();
        }
    }
}
