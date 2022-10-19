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
    public GameObject melee;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    public float playerHeight;
    private bool _isGrounded;

    [Header("Jumping")]
    private int _isJumpingHash;
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
    public float _attackTime;
    [Header("Dodging")]
    private bool _isDodging = false;
    public float _dodgeTime;


    public CameraController cameraController;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public Transform Transform { get { return transform; } }
    public Animator Animator { get { return _animator; } }
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
    public bool IsDodging { get { return _isDodging; } }
    public bool MovementLock { set { _movementLock = value; } }

    void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponentInChildren<Animator>();
        cameraController = GetComponent<CameraController>();
        _isJumpingHash = Animator.StringToHash("isJumping");
        _isMovingHash = Animator.StringToHash("isMoving");

        _playerInput.Player.Jump.started += OnJump;
        _playerInput.Player.Jump.canceled += OnJump;
        _playerInput.Player.Move.started += OnMove;
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;
        _playerInput.Player.Attack.started += OnAttack;
        _playerInput.Player.Attack.canceled += OnAttack;
        _playerInput.Player.Dodge.started += OnDodge;
        _playerInput.Player.Dodge.canceled += OnDodge;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.2f, whatIsGround);
        _currentState.UpdateStates();
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

    private bool _isInvoking = false;
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

    void OnGather(InputAction.CallbackContext context)
    {

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
