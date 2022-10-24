using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    private float timeToEnd;

    public PlayerBlockState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
    {
        IsRootState = true;
    }



    public override void CheckSwitchStates()
    {
        if (!Ctx.IsBlocking)
            SwitchState(Factory.Grounded());
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.Animator.SetBool("isBlocking", true);
    }

    public override void ExitState()
    {
        Ctx.Animator.SetBool("isBlocking", false);
    }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMoving)
        {
            SetSubState(Factory.Idle());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

}
