using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class PeacefulEntityBT : BehaviourTree.Tree
{
    public static Vector3 startingPosition;
    public static float iddleRadius = 2.0f;
    private Animator _animator;

    protected override Node SetupTree()
    {
        _animator = GetComponent<Animator>();
        startingPosition = transform.position;
        Node root = new Selector(
            new List<Node>
            {
                new Sequence(
                    new List<Node> { new CheckForDeadStatus(transform), new TaskDie(transform) }
                ),
                new Sequence(
                    new List<Node>
                    {
                        new CheckForDamageTaken(transform, _damageTaken),
                        new TaskTakeDamage(transform),
                    }
                ),
                new TaskPatrol(transform, _animator, startingPosition)
            }
        );

        return root;
    }

    private Queue<int> _attackedBy = new();
    private Queue<int> _damageTaken = new();

    private void OnTriggerEnter(Collider other)
    {
        if (
            other.transform.root.CompareTag("Player")
            && !_attackedBy.Contains(other.transform.root.GetInstanceID())
        )
        {
            Debug.Log("Damaged");
            _damageTaken.Enqueue(
                other.transform.root.GetComponent<EntityStats>().damage.GetValue()
            );
            _attackedBy.Enqueue(other.transform.root.GetInstanceID());
            StartCoroutine(nameof(DamagedCd));
        }
    }

    private IEnumerator DamagedCd()
    {
        yield return new WaitForSeconds(0.5f);
        _attackedBy.Dequeue();
    }
}
