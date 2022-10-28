using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForDeadStatus : Node
{
    private EntityStats _entityStats;

    public CheckForDeadStatus(Transform transform)
    {
        _entityStats = transform.GetComponent<EntityStats>();
    }

    public override NodeState Evaluate()
    {
        state = (_entityStats.IsDead) ? NodeState.SUCCESS : NodeState.FAILURE;
        return state;
    }
}
