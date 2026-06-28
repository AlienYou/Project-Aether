using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Framework.Test
{
    public class IdleState : IState
    {
        public void Enter()
        {
            Log.Info("IdleState: Enter");
        }

        public void Exit()
        {
            Log.Info("IdleState: Exit");
        }

        public void Update()
        {
            Log.Info("IdleState: Update");
        }
    }
}