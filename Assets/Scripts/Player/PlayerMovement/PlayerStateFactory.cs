using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerStates
{
    idle,
    run,
    groudned,
    jump,
    attack,
    dodge
}

public class PlayerStateFactory : MonoBehaviour
{

    PlayerStateMachine _context;
    Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
        _states[PlayerStates.idle] = new PlayerIdleState(_context, this);
        _states[PlayerStates.run] = new PlayerRunState(_context, this);
        _states[PlayerStates.groudned] = new PlayerGroudedState(_context, this);
        _states[PlayerStates.jump] = new PlayerJumpState(_context, this);
        _states[PlayerStates.attack] = new PlayerAttackState(_context, this);
        _states[PlayerStates.dodge] = new PlayerDodgeState(_context, this);
    }

    public PlayerBaseState Idle() => _states[PlayerStates.idle];
    public PlayerBaseState Run() => _states[PlayerStates.run];
    public PlayerBaseState Grounded() => _states[PlayerStates.groudned];
    public PlayerBaseState Jump() => _states[PlayerStates.jump];
    public PlayerBaseState Attack() => _states[PlayerStates.attack];
    public PlayerBaseState Dodge() => _states[PlayerStates.dodge];

}
