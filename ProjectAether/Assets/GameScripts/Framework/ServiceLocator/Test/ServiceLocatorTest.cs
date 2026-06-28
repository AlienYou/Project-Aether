using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using ProjectAether.Framework;
using ProjectAether.Test;
using UnityEngine;

public class ServiceLocatorTest : MonoBehaviour
{
    void Start()
    {
        TestService testService = new TestService();
        ServiceLocator.Register(testService);
    
        TestService result = ServiceLocator.Get<TestService>();
        Log.Info($"ServiceLocatorTest: {result.Name}");
    }
}
