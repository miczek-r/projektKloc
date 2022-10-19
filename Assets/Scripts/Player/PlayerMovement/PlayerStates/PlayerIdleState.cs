using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsMoving)
        {
            SwitchState(Factory.Run());
        }
    }

    public override void EnterState()
    {
        Ctx.Rigidbody.drag = 5.0f;
        Ctx.Animator.SetBool(Ctx.IsMovingHash, false);
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
