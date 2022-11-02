using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private Vector3 _previousMovement;
    private float timeToEnd;

    public PlayerAttackState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
    {
        IsRootState = true;
    }



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
        Debug.Log("Started Attack");
        timeToEnd = Ctx._attackTime;
        Ctx.Melee.GetComponentInChildren<Collider>().enabled = true;
        Ctx.MovementLock = true;
        Ctx.Animator.SetBool("isAttacking", true);
        if(!Ctx.GetComponent<PlayerQuickActions>().hasBow)return;
        Ctx.SpawnArrow();
    }

    public override void ExitState()
    {
        Debug.Log("Ended Attack");
        Ctx.Melee.GetComponentInChildren<Collider>().enabled = false;
        Ctx.MovementLock = false;
        Ctx.IsAttacking = false;
        Ctx.Animator.SetBool("isAttacking", false);
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
