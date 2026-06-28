using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Framework.Test
{
    public class FSMTest : MonoBehaviour
    {
        private StateMachine _fsm;

        // Start is called before the first frame update
        void Start()
        {
            _fsm = new StateMachine();
            _fsm.AddState(new IdleState());
            _fsm.AddState(new MoveState());
            _fsm.ChangeState<IdleState>();
            _fsm.ChangeState<MoveState>();
        }

        // Update is called once per frame
        void Update()
        {
            _fsm?.Update();
        }
    }
}
