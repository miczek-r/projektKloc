using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.Rigidbody.drag = 0f;
        Jump();
    }

    public override void ExitState()
    {
        if (Ctx.IsJumpPressed)
            Ctx.RequireNewJumpPress = true;
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }


    private void Jump()
    {

        Ctx.Rigidbody.velocity = new Vector3(Ctx.Rigidbody.velocity.x, 0f, Ctx.Rigidbody.velocity.z);

        Ctx.Rigidbody.AddForce(Ctx.transform.up * Ctx.JumpForce, ForceMode.Impulse);

        Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
    }
}
