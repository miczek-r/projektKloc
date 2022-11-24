using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskReturnToSpawnPoint : Node
{
    private Transform _transform;
    private Animator _animator;

    public TaskReturnToSpawnPoint(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        Vector3 targetGoTo = new Vector3(
            HostileEntityBT.startingPosition.x,
            _transform.position.y,
            HostileEntityBT.startingPosition.z
        );
        if (Vector3.Distance(_transform.position, targetGoTo) < 0.01f)
        {
            parent.parent.ClearData("target");

            _transform.position = targetGoTo;
            HostileEntityBT.isReturning = false;
            _animator.SetBool("isMoving", false);
        }
        else
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position,
                targetGoTo,
                Time.deltaTime * 10.0f
            );
            _transform.LookAt(targetGoTo);
        }
        return state;
    }
}
