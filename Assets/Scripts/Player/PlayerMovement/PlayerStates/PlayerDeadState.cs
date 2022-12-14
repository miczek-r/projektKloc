using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
    }

    public override void EnterState()
    {   
        SceneManager.LoadScene(2);
        Ctx.Animator.SetBool("isDead", true);
        //Ctx.cameraController.enabled = false;
        Ctx.enabled = false;
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
    }
}
