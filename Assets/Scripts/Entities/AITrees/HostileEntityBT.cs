using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class HostileEntityBT : BehaviourTree.Tree
{
    public Vector3 startingPosition;
    public static float iddleRadius = 2.0f;
    public static float fovRange = 5.0f;
    public static float attackRange = 2.0f;
    public bool isReturning = false;
    private Animator _animator;
    private KillableEntityStats _stats;
    private LootDrop _lootDropManager;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, fovRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, fovRange * 2f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
    }

    public void Awake()
    {
        _stats = GetComponent<KillableEntityStats>();
        _lootDropManager = GetComponent<LootDrop>();
    }

    protected override Node SetupTree()
    {
        _animator = GetComponent<Animator>();
        startingPosition = transform.position;
        Node root = new Selector(
            new List<Node>
            {
                new Sequence(
                    new List<Node>
                    {
                        new CheckForDeadStatus(transform),
                        new TaskDie(transform, _stats.ExpGiven, _lootDropManager)
                    }
                ),
                new Sequence(
                    new List<Node>
                    {
                        new CheckForOutOfRadius(transform, startingPosition),
                        new TaskReturnToSpawnPoint(transform, startingPosition)
                    }
                ),
                new Sequence(
                    new List<Node>
                    {
                        new CheckForDamageTaken(transform, _damageTaken),
                        new TaskTakeDamage(transform),
                    }
                ),
                new Sequence(
                    new List<Node>
                    {
                        new CheckForPlayerInAttackRange(transform, _animator),
                        new TaskAttack(transform, _stats.damage.GetValue())
                    }
                ),
                new Sequence(
                    new List<Node>
                    {
                        new CheckForPlayerInFOVRange(transform),
                        new TaskGoToTarget(transform)
                    }
                ),
                new TaskPatrol(transform, _animator, startingPosition)
            }
        );

        return root;
    }

    private Queue<int> _attackedBy = new();
    private Queue<Tuple<GameObject, int>> _damageTaken = new();

    private void OnTriggerEnter(Collider other)
    {
        if (
            (other.transform.root.CompareTag("Player"))
            && !_attackedBy.Contains(other.transform.root.GetInstanceID())
        )
        {
            Debug.Log("Damaged");
            _damageTaken.Enqueue(
                new Tuple<GameObject, int>(
                    other.transform.root.gameObject,
                    other.transform.root.GetComponent<EntityStats>().damage.GetValue()
                )
            );
            _attackedBy.Enqueue(other.transform.root.GetInstanceID());
            StartCoroutine(nameof(DamagedCd));
        }
        if (
            other.transform.root.CompareTag("Projectile")
            && !_attackedBy.Contains(other.transform.root.GetInstanceID())
        )
        {
            other.transform.root.GetComponent<Projectile>().enabled = false;
            other.transform.root.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.root.SetParent(gameObject.transform.GetChild(0).GetChild(0));
            _damageTaken.Enqueue(
                new Tuple<GameObject, int>(
                    other.transform.GetComponent<Projectile>().spawner,
                    other.transform.GetComponent<Projectile>().damage
                )
            );
        }
    }

    private IEnumerator DamagedCd()
    {
        yield return new WaitForSeconds(0.5f);
        _attackedBy.Dequeue();
    }
}
