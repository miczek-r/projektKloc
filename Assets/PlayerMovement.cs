using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float dashSpeed;

    public float groundDrag;
    public bool moveLocked = false;
    [Header("Gathering")]
    public float gatheringCooldown;
    public KeyCode gatheringKey = KeyCode.E;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    [Header("Attacking")]
    public float attackCooldown;
    [Header("Ground Check")]
    public LayerMask whatIsGround;
    public float playerHeight;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air,
        attacking,
        gathering,
        dashing
    }

    public bool dashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        playerHeight = GetComponentInChildren<CapsuleCollider>().height;
    }


    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();


        if (state == MovementState.walking)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void GroundCheck()
    {

    }

    private void MyInput()
    {
        if (moveLocked) return;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(gatheringKey) && grounded)
        {
            Gathering();

            Invoke(nameof(ResetGathering), gatheringCooldown);
        }

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetMouseButton(0) && grounded)
        {
            state = MovementState.attacking;
            Attack();
            Invoke(nameof(ResetAttack), attackCooldown);
        }

    }

    private void SetDashingSpeed()
    {
        moveSpeed = dashSpeed;
    }

    private void SetMovementSpeed()
    {
        moveSpeed = walkSpeed;
    }

    private void StateHandler()
    {
        if (moveLocked && (state == MovementState.attacking || state == MovementState.gathering)) return;
        if (dashing)
        {
            state = MovementState.dashing;
            anim.SetBool("Dashing", true);
            SetDashingSpeed();
        }
        else if (grounded)
        {
            state = MovementState.walking;
            SetMovementSpeed();
            anim.SetBool("Dashing", false);
            anim.SetBool("Jumping", false);
        }

        // Mode - Air
        else
        {
            anim.SetBool("Jumping", true);
            state = MovementState.air;
        }
    }
    private void FixedUpdate()
    {
        if (state == MovementState.attacking || state == MovementState.gathering) return;
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        anim.SetFloat("vertical", flatVel.magnitude);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Gathering()
    {
        state = MovementState.gathering;
        rb.velocity = Vector3.zero;
        anim.SetBool("Gathering", true);
        moveLocked = true;
    }

    private void ResetGathering()
    {
        state = MovementState.walking;
        anim.SetBool("Gathering", false);
        moveLocked = false;
    }

    private void Attack()
    {
        state = MovementState.attacking;
        rb.velocity = Vector3.zero;
        anim.SetBool("Attacking", true);
        moveLocked = true;
    }

    private void ResetAttack()
    {
        state = MovementState.walking;
        anim.SetBool("Attacking", false);
        moveLocked = false;
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        anim.SetBool("Jumping", true);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

}