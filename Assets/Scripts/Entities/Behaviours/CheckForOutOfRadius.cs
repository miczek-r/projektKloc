using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForOutOfRadius : Node
{
    private Transform _transform;

    public CheckForOutOfRadius(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Vector3 targetGoTo = new Vector3(
            HostileEntityBT.startingPosition.x,
            _transform.position.y,
            HostileEntityBT.startingPosition.z
        );
        if (HostileEntityBT.isReturning)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        if (Vector3.Distance(_transform.position, targetGoTo) >= HostileEntityBT.fovRange * 5f)
        {
            HostileEntityBT.isReturning = true;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
