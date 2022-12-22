using System;
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
        Tuple<GameObject, int> damageToBeDelt = (Tuple<GameObject, int>)GetData("damageToBeDelt");
        _transform.GetComponent<EntityStats>().TakeDamage(damageToBeDelt.Item2);
        parent.parent.SetData("lastDamageDealer", damageToBeDelt.Item1);
        _transform.GetComponent<Animator>().SetTrigger("isDamaged");
        state = NodeState.RUNNING;
        return state;
    }
}
