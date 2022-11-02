using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskAttack : Node
{
    private Transform _lastTarget;
    private PlayerStats _playerStats;
    private Animator _animator;

    private float _attackCooldown = 1.0f;
    private float _waitingLeft = 0.0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _playerStats = target.GetComponent<PlayerStats>();
            _lastTarget = target;
        }

        _waitingLeft -= Time.deltaTime;
        if (_waitingLeft <= 0f)
        {
            _animator.SetTrigger("isAttacking");
            _playerStats.TakeDamage(10);
            _waitingLeft = _attackCooldown;

            if (_playerStats.IsDead)
            {
                Debug.Log("Player is Dead");
                target.tag = "Untagged";
                ClearData("target");
                state = NodeState.SUCCESS;
                return state;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
