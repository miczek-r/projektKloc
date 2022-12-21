using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskReturnToSpawnPoint : Node
{
    private Transform _transform;
    private Animator _animator;
    private Vector3 _startingPosition;

    public TaskReturnToSpawnPoint(Transform transform, Vector3 startingPosition)
    {
        _transform = transform;
        _startingPosition = startingPosition;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        Vector3 targetGoTo = new Vector3(
            _startingPosition.x,
            _transform.position.y,
            _startingPosition.z
        );
        if (Vector3.Distance(_transform.position, targetGoTo) < 0.01f)
        {

            _transform.position = targetGoTo;
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
