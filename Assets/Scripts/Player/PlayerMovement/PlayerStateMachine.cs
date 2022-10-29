using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private Animator _animator;
    private PlayerStats _playerStats;
    public GameObject melee;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    private bool _isGrounded;

    [Header("Jumping")]
    private int _isJumpingHash;
    private int _isFallingHash;
    public float _jumpForce;
    private bool _isJumpPressed;
    private bool _requireNewJumpPress = false;
    [Header("Running")]
    public bool _movementLock = false;
    public float _groundDrag;
    public int _movementSpeed;
    private int _isMovingHash;
    private bool _isMoving;
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    public Transform orientation;
    [Header("Combat")]
    private bool _isAttacking = false;
    private bool _isBlocking = false;
    public float _attackTime;
    private bool _isDamaged = false;
    private Queue<int> _damageTaken = new();
    [Header("Dodging")]
    private bool _isDodging = false;
    public float _dodgeTime;
    [Header("Gather")]
    private bool _isGathering = false;


    public CameraController cameraController;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public Animator Animator { get { return _animator; } }
    public PlayerStats PlayerStats { get { return _playerStats; } }
    public Vector3 CurrentMovement { get { return _currentMovement; } set { _currentMovement = value; } }
    public GameObject Melee { get { return melee; } }
    public bool IsGrounded { get { return _isGrounded; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public int IsJumpingHash { get { return _isJumpingHash; } }
    public float JumpForce { get { return _jumpForce; } set { _jumpForce = value; } }
    public int IsMovingHash { get { return _isMovingHash; } }
    public bool IsMoving { get { return _isMoving; } }
    public int MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }
    public float GroundDrag { get { return _groundDrag; } set { _groundDrag = value; } }
    public bool IsAttacking { get { return _isAttacking; } }
    public bool IsBlocking { get { return _isBlocking; } }
    public bool IsDodging { get { return _isDodging; } }
    public bool MovementLock { set { _movementLock = value; } }
    public int IsFallingHash { get { return _isFallingHash; } }
    public Queue<int> DamageTaken { get { return _damageTaken; } }
    public bool IsDamaged { get { return _isDamaged; } set { _isDamaged = value; } }
    public bool IsGathering { get { return _isGathering; } set { _isGathering = value; } }

    void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponentInChildren<Animator>();
        cameraController = GetComponent<CameraController>();
        _isJumpingHash = Animator.StringToHash("isJumping");
        _isMovingHash = Animator.StringToHash("isMoving");
        _isFallingHash = Animator.StringToHash("isFalling");

        _playerInput.Player.Jump.started += OnJump;
        _playerInput.Player.Jump.canceled += OnJump;
        _playerInput.Player.Move.started += OnMove;
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;
        _playerInput.Player.Attack.started += OnAttack;
        _playerInput.Player.Attack.canceled += OnAttack;
        _playerInput.Player.Dodge.started += OnDodge;
        _playerInput.Player.Dodge.canceled += OnDodge;
        _playerInput.Player.Debug.started += OnDebug;
        _playerInput.Player.Gather.started += OnGather;
        _playerInput.Player.Aim.started += OnBlock;
        _playerInput.Player.Aim.canceled += OnBlock;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerStats = GetComponent<PlayerStats>();
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.2f, whatIsGround);
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

    //TODO: Remove later
    private void OnDebug(InputAction.CallbackContext context)
    {
        _damageTaken.Enqueue(10);
        _attackedBy.Enqueue(transform.root.GetInstanceID());
        _isDamaged = true;
        StartCoroutine(nameof(DamagedCd));
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

    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requireNewJumpPress = false;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.y = _currentMovementInput.y;
        _isMoving = (!_movementLock) ? _currentMovementInput.x != 0 || _currentMovementInput.y != 0 : false;
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        _isMoving = false;
        _isAttacking = context.ReadValueAsButton();
    }

    void OnDodge(InputAction.CallbackContext context)
    {
        _isDodging = context.ReadValueAsButton();
        //cameraController.enabled = !context.ReadValueAsButton();
    }

    void OnBlock(InputAction.CallbackContext context)
    {
        if (GetComponent<PlayerQuickActions>().hasBow) return;
        _isBlocking = context.ReadValueAsButton();
    }

    void OnGather(InputAction.CallbackContext context)
    {
        _isMoving = false;
        _isGathering = true;
    }

    void OnEnable()
    {
        _playerInput.Player.Enable();
    }
    void OnDisable()
    {
        _playerInput.Player.Disable();
    }
}
