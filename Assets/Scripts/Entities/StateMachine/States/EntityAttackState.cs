using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackState : EntityBaseState
{
    public EntityAttackState(EntityStateMachine context, EntityStateFactory entityStateFactory) : base(context, entityStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
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
