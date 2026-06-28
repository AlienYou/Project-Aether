using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Framework.Test
{
    public class MoveState : IState
    {
        public void Enter()
        {
            Log.Info("MoveState: Enter");
        }

        public void Exit()
        {
            Log.Info("MoveState: Exit");
        }

        public void Update()
        {
            Log.Info("MoveState: Update");
        }
    }
}