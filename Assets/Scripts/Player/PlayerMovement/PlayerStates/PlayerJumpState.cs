using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine context, PlayerStateFactory playerStateFactory)
        : base(context, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
        if (Ctx.Animator.GetCurrentAnimatorStateInfo(0).IsName("isJumping"))
        {
            SwitchState(Factory.Fall());
        }
    }

    public override void EnterState()
    {
        InitializeSubState();
        Jump();
        Ctx.QuestSupervisor.Achievments.Increment("jump");
    }

    public override void ExitState()
    {
        if (Ctx.IsJumpPressed)
            Ctx.RequireNewJumpPress = true;
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public float Gravity = -15.0f;
    public float JumpHeight = 2.0f;

    private void Jump()
    {
        Ctx.VerticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

        Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
    }
}
