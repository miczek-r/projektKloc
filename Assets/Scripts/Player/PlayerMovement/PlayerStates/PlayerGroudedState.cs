using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroudedState : PlayerBaseState
{
    public PlayerGroudedState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchState(Factory.Jump());
        }
        if (Ctx.IsAttacking)
        {
            SwitchState(Factory.Attack());
        }
        if (Ctx.IsDodging && Ctx.IsMoving)
        {
            SwitchState(Factory.Dodge());
        }
        if (!Ctx.IsGrounded)
        {
            SwitchState(Factory.Fall());
        }
        if (Ctx.PlayerStats.IsDead)
        {
            SwitchState(Factory.Dead());
        }
        if (Ctx.IsDamaged)
        {
            SwitchState(Factory.Damaged());
        }
        if (Ctx.IsGathering)
        {
            SwitchState(Factory.Gather());
        }
        if (Ctx.IsBlocking)
        {
            SwitchState(Factory.Block());
        }
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void ExitState()
    {
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
