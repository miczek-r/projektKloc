using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    private Animator _animator;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _animator.SetBool("isMoving", true);
            _transform.position = Vector3.MoveTowards(
                _transform.position,
                target.position,
                Time.deltaTime * 6.0f
            );
            _transform.LookAt(target.position);
        }
        state = NodeState.RUNNING;
        return state;
    }
}
