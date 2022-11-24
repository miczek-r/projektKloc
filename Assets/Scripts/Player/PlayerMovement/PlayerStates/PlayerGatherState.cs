using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGatherState : PlayerBaseState
{
    public PlayerGatherState(PlayerStateMachine context, PlayerStateFactory playerStateFactory)
        : base(context, playerStateFactory)
    {
        IsRootState = true;
    }

    float timeToEnd;

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
        timeToEnd = 2.0f;
        Ctx.MovementLock = true;
        Ctx.Animator.SetTrigger("isGathering");
    }

    public override void ExitState()
    {
        Ctx.pickupManager.PickupItems();
        Ctx.MovementLock = false;
        Ctx.IsGathering = false;
    }

    public override void InitializeSubState()
    {
        SetSubState(Factory.Idle());
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
