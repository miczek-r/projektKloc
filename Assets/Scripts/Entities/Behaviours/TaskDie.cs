using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskDie : Node
{
    private Transform _transform;
    private Animator _animator;
    private Collider _collider;
    private float _decompositionTime = 5.0f;
    private bool isDecomposing = false;

    public TaskDie(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _collider = transform.GetComponent<Collider>();
    }

    public override NodeState Evaluate()
    {
        if (isDecomposing)
        {
            _decompositionTime -= Time.deltaTime;
            if (_decompositionTime <= 1.0f)
            {
                _collider.enabled = false;
            }
            if (_decompositionTime <= 0f)
            {
                Object.Destroy(_transform.gameObject);
            }
        }
        else
        {
            _animator.SetBool("isDead", true);
            isDecomposing = true;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
