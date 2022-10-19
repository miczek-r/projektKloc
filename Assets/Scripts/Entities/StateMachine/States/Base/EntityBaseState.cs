using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBaseState
{
    private bool _isRootState = false;
    private EntityStateMachine _ctx;
    private EntityStateFactory _factory;
    private EntityBaseState _currentSuperState;
    private EntityBaseState _currentSubState;

    protected bool IsRootState { set { _isRootState = value; } }
    protected EntityStateMachine Ctx { get { return _ctx; } }
    protected EntityStateFactory Factory { get { return _factory; } }

    public EntityBaseState(EntityStateMachine context, EntityStateFactory EntityStateFactory)
    {
        this._ctx = context;
        this._factory = EntityStateFactory;
    }
    public abstract void EnterState();
    public abstract void ExitState();

    public abstract void UpdateState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();
    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState is not null)
        {
            _currentSubState.UpdateState();
        }
    }
    protected void SwitchState(EntityBaseState newState)
    {
        ExitState();
        newState.EnterState();
        if (_isRootState)
            _ctx.CurrentState = newState;
        else if (_currentSuperState is not null)
            _currentSuperState.SetSubState(newState);
    }
    protected void SetSuperState(EntityBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(EntityBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    
}
