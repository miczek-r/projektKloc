using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskTakeDamage : Node
{
    private Transform _transform;

    public TaskTakeDamage(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        int damageToBeDelt = (int)GetData("damageToBeDelt");
        _transform.GetComponent<EntityStats>().TakeDamage(damageToBeDelt);
        _transform.GetComponent<Animator>().SetTrigger("isDamaged");
        state = NodeState.RUNNING;
        return state;
    }
}
