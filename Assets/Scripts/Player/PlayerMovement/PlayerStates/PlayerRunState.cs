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
        Move();
        CheckSwitchStates();
    }


        private float _targetRotation = 0.0f;
        private float targetSpeed = 10.0f;
        private float _speed = 0.0f;
[Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;
        private float _rotationVelocity;
    private void Move()
        {
            float currentHorizontalSpeed = new Vector3(Ctx.CharacterController.velocity.x, 0.0f, Ctx.CharacterController.velocity.z).magnitude;

            float speedOffset = 0.1f;

            float inputMagnitude = Ctx.CurrentMovement.magnitude;
            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }


            // normalise input direction
            Vector3 inputDirection = new Vector3(Ctx.CurrentMovement.x, 0.0f, Ctx.CurrentMovement.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (Ctx.CurrentMovement != Vector3.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  Ctx.mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(Ctx.transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                Ctx.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Ctx.TargetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;


        }

}
