using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework
{
    public abstract class FSMOwner
    {
        protected readonly StateMachine stateMachine = new();
    
        public void Update()
        {
            stateMachine.Update();
        }
    }
}