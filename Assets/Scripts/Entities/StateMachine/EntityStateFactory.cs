using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum EntityStates
{
    idle,
    chase,
    reset,
    attack,
    damaged,
    dead
}

public class EntityStateFactory : MonoBehaviour
{

    EntityStateMachine _context;
    Dictionary<EntityStates, EntityBaseState> _states = new Dictionary<EntityStates, EntityBaseState>();

    public EntityStateFactory(EntityStateMachine currentContext)
    {
        _context = currentContext;
        _states[EntityStates.idle] = new EntityIdleState(_context, this);
        _states[EntityStates.chase] = new EntityChaseState(_context, this);
        _states[EntityStates.reset] = new EntityReturnState(_context, this);
        _states[EntityStates.attack] = new EntityAttackState(_context, this);
        _states[EntityStates.damaged] = new EntityDamagedState(_context, this);
        _states[EntityStates.dead] = new EntitDeadState(_context, this);
    }

    public EntityBaseState Idle() => _states[EntityStates.idle];
    public EntityBaseState Chase() => _states[EntityStates.chase];
    public EntityBaseState Reset() => _states[EntityStates.reset];
    public EntityBaseState Attack() => _states[EntityStates.attack];
    public EntityBaseState Damaged() => _states[EntityStates.damaged];
    public EntityBaseState Dead() => _states[EntityStates.dead];

}