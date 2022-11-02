using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForPlayerInAttackRange : Node
{
    private Transform _transform;
    private Animator _animator;

    public CheckForPlayerInAttackRange(Transform transform, Animator animator)
    {
        _transform = transform;
        _animator = animator;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target is null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform targetT = (Transform)target;
        if (Vector3.Distance(_transform.position, targetT.position) <= HostileEntityBT.attackRange)
        {
            _animator.SetBool("isMoving", false);

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
