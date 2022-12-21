using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForOutOfRadius : Node
{
    private Transform _transform;
    Vector3 _startingPosition;
    bool isReturning = false;

    public CheckForOutOfRadius(Transform transform, Vector3 startingPosition)
    {
        _transform = transform;
        _startingPosition = startingPosition;
    }

    public override NodeState Evaluate()
    {
        Vector3 targetGoTo = new Vector3(
            _startingPosition.x,
            _transform.position.y,
            _startingPosition.z
        );
        if (Vector3.Distance(_transform.position, targetGoTo) >= HostileEntityBT.fovRange * 5f)
        {
            isReturning = true;
            state = NodeState.SUCCESS;
            return state;
        }
        if (
            Vector3.Distance(_transform.position, targetGoTo) >= HostileEntityBT.fovRange
            && isReturning
        )
        {
            state = NodeState.SUCCESS;
            return state;
        }
        if (isReturning)
            parent.parent.ClearData("target");
        isReturning = false;

        state = NodeState.FAILURE;
        return state;
    }
}
