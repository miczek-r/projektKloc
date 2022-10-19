using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitDeadState : EntityBaseState
{
    public EntitDeadState(EntityStateMachine context, EntityStateFactory entityStateFactory) : base(context, entityStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
    }

    public override void EnterState()
    {
        Debug.Log("asd");
        Ctx.Animator.SetBool("isDead", true);
        Ctx.transform.Rotate(20.0f, 0.0f, 0.0f, Space.Self);
        Ctx.transform.Translate(0.0f, 0.3f, 0.0f, Space.Self);
        Ctx.GetComponent<Rigidbody>().useGravity = true;
        Ctx.GetComponent<CapsuleCollider>().radius = 0.01f;

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
