using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityIdleState : EntityBaseState
{
    public EntityIdleState(EntityStateMachine context, EntityStateFactory entityStateFactory) : base(context, entityStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.EntityStats.IsDead)
        {
            SwitchState(Factory.Dead());
        }
        if (Ctx.IsDamaged)
        {
            SwitchState(Factory.Damaged());
        }
    }

    public override void EnterState()
    {
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
