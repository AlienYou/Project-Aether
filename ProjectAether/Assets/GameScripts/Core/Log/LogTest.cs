using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

public class LogTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Log.Info("Game Start");
        Log.Warning("COnfig Missing");
        Log.Error("Load Failed");
        Log.Exception(new System.Exception("Test Exception"));
    }
}
