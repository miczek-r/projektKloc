using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private Vector3 _moveDirection;
    public PlayerRunState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMoving)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void EnterState()
    {
        Ctx.Rigidbody.drag = Ctx.GroundDrag;
        Ctx.Animator.SetBool(Ctx.IsMovingHash, true);
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        Movement();
        SpeedControl();
        CheckSwitchStates();
    }

    private void Movement()
    {

        _moveDirection = Ctx.orientation.forward * Ctx.CurrentMovement.y + Ctx.orientation.right * Ctx.CurrentMovement.x;
        Ctx.Rigidbody.AddForce(_moveDirection.normalized * Ctx.MovementSpeed * 10f, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(Ctx.Rigidbody.velocity.x, 0f, Ctx.Rigidbody.velocity.z);

        Ctx.Animator.SetFloat("MovementX", Ctx.Rigidbody.velocity.x);
        Ctx.Animator.SetFloat("MovementY", Ctx.Rigidbody.velocity.z);

        if (flatVel.magnitude > Ctx.MovementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * Ctx.MovementSpeed;
            Ctx.Rigidbody.velocity = new Vector3(limitedVel.x, Ctx.Rigidbody.velocity.y, limitedVel.z);
        }
    }
}
