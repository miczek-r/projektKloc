using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : PlayerBaseState
{
    public PlayerDamagedState(PlayerStateMachine context, PlayerStateFactory playerStateFactory)
        : base(context, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.Animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void EnterState()
    {
        Ctx.Animator.SetTrigger("isDamaged");
        Ctx.IsDamaged = false;
        Ctx.SpawnBlood();
        while (Ctx.DamageTaken.Count > 0)
            Ctx.PlayerStats.TakeDamage(Ctx.DamageTaken.Dequeue());
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
