using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamagedState : EntityBaseState
{
    public EntityDamagedState(EntityStateMachine context, EntityStateFactory entityStateFactory) : base(context, entityStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.Animator.GetCurrentAnimatorStateInfo(0).IsName("Pushed"))
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void EnterState()
    {
        Ctx.Animator.SetTrigger("isDamaged");
        Ctx.IsDamaged = false;
        Ctx.SpawnBlood();
        while (Ctx.DamageTaken.Count > 0)
            Ctx.EntityStats.TakeDamage(Ctx.DamageTaken.Dequeue());
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
