using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForPlayerInFOVRange : Node
{
    private Transform _transform;

    public CheckForPlayerInFOVRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target is null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position,
                HostileEntityBT.fovRange
            );
            foreach (var collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    parent.parent.SetData("target", collider.transform);
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
