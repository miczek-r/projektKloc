using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using BehaviourTree;
using UnityEngine;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Animator _animator;
    private Vector3 nextWaypoint;

    private float _waitTime = 1.0f;
    private float _waitLeft = 0f;
    private bool _isWaiting = false;

    public TaskPatrol(Transform transform, Animator animator)
    {
        _transform = transform;
        _animator = animator;
        nextWaypoint = GetNextPatroPosition();
    }

    public override NodeState Evaluate()
    {
        if (_isWaiting)
        {
            _waitLeft -= Time.deltaTime;
            if (_waitLeft < 0.0f)
            {
                _isWaiting = false;
                _animator.SetBool("isMoving",true);
            }
        }
        else
        {
            if (Vector3.Distance(_transform.position, nextWaypoint) < 0.01f)
            {
                _transform.position = nextWaypoint;
                _waitLeft = _waitTime;
                _isWaiting = true;
                nextWaypoint = GetNextPatroPosition();
                _animator.SetBool("isMoving",false);
            }
            else
            {
                nextWaypoint.y = _transform.position.y;
                _transform.position = Vector3.MoveTowards(
                    _transform.position,
                    nextWaypoint,
                    Time.deltaTime * 1.0f
                );
                _transform.LookAt(nextWaypoint);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

    private Vector3 GetNextPatroPosition()
    {
        return HostileEntityBT.startingPosition + Random.insideUnitSphere * Random.Range(1f, 10f);
    }
}
