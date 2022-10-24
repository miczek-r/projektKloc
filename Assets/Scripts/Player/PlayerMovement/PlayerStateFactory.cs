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
    dodge,
    fall,
    damaged,
    dead,
    gather
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
        _states[PlayerStates.fall] = new PlayerFallState(_context, this);
        _states[PlayerStates.damaged] = new PlayerDamagedState(_context, this);
        _states[PlayerStates.dead] = new PlayerDeadState(_context, this);
        _states[PlayerStates.gather] = new PlayerGatherState(_context, this);
    }

    public PlayerBaseState Idle() => _states[PlayerStates.idle];
    public PlayerBaseState Run() => _states[PlayerStates.run];
    public PlayerBaseState Grounded() => _states[PlayerStates.groudned];
    public PlayerBaseState Jump() => _states[PlayerStates.jump];
    public PlayerBaseState Attack() => _states[PlayerStates.attack];
    public PlayerBaseState Dodge() => _states[PlayerStates.dodge];
    public PlayerBaseState Fall() => _states[PlayerStates.fall];
    public PlayerBaseState Damaged() => _states[PlayerStates.damaged];
    public PlayerBaseState Dead() => _states[PlayerStates.dead];
    public PlayerBaseState Gather() => _states[PlayerStates.gather];

}
