using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Core
{
    public class Log
    {
        public static void Info(string message)
        {
            if (!LogConfig.EnableLog)
            {
                return;
            }
            if (LogConfig.Level > LogLevelEnum.Info)
            {
                return;
            }
            Debug.Log($"[INFO] {message}");
        }

        public static void Warning(string message)
        {
            if (!LogConfig.EnableLog)
            {
                return;
            }
            if (LogConfig.Level > LogLevelEnum.Warning)
            {
                return;
            }
            Debug.LogWarning($"[WARNING] {message}");
        }

        public static void Error(string message)
        {
            if (!LogConfig.EnableLog)
            {
                return;
            }
            if (LogConfig.Level > LogLevelEnum.Error)
            {
                return;
            }
            Debug.LogError($"[ERROR] {message}");
        }

        public static void Exception(System.Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}
