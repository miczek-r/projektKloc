using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
    {
        IsRootState = true;
    }
    private float timeToEnd;

    public override void CheckSwitchStates()
    {
        timeToEnd -= Time.deltaTime;
        if (timeToEnd < 0)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void EnterState()
    {
        timeToEnd = Ctx._dodgeTime;
        //Ctx.MovementLock = true;
        Ctx.Animator.SetBool("isDodging", true);
        InitializeSubState();
    }

    public override void ExitState()
    {
        // Ctx.MovementLock = false;
        Ctx.Animator.SetBool("isDodging", false);
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
