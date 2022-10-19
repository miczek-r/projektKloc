using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateMachine : MonoBehaviour
{
    private EntityBaseState _currentState;
    private EntityStateFactory _states;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private EntityStats _entityStats;
    [Header("Combat")]
    private bool _isAttacking = false;
    public float _attackTime;
    private bool _isDamaged = false;
    public float _damagedTime;
    private Queue<int> _damageTaken = new();


    public EntityBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _animator; } }
    public EntityStats EntityStats { get { return _entityStats; } }
    public Queue<int> DamageTaken { get { return _damageTaken; } }
    public bool IsDamaged { get { return _isDamaged; } set { _isDamaged = value; } }


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _entityStats = GetComponent<EntityStats>();
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _states = new EntityStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateStates();
    }

    Queue<int> _attackedBy = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && !_attackedBy.Contains(other.transform.root.GetInstanceID()))
        {
            _damageTaken.Enqueue(other.transform.root.GetComponent<EntityStats>().damage.GetValue());
            _attackedBy.Enqueue(other.transform.root.GetInstanceID());
            _isDamaged = true;
            StartCoroutine(nameof(DamagedCd));
        }

    }

    private IEnumerator DamagedCd()
    {
        yield return new WaitForSeconds(0.5f);
        _attackedBy.Dequeue();
    }

    public GameObject HitParticle;

    public void SpawnBlood()
    {
        Instantiate(HitParticle, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);

    }
}
