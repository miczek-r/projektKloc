using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckForDamageTaken : Node
{
    private Transform _transform;
    private Queue<Tuple<GameObject, int>> _damageTaken;

    public CheckForDamageTaken(Transform transform, Queue<Tuple<GameObject, int>> damageTaken)
    {
        _transform = transform;
        _damageTaken = damageTaken;
    }

    public override NodeState Evaluate()
    {
        if (_damageTaken.Count > 0)
        {
            parent.parent.SetData("damageToBeDelt", _damageTaken.Dequeue());
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
