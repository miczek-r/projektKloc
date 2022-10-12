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

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

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
        Debug.DrawRay(transform.position, Vector3.down, Color.green);

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

    private Vector3 MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        return inputDir;
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
        Debug.Log(grounded);
        if (dashing)
        {
            state = MovementState.dashing;
            anim.SetBool("Dashing", true);
            Invoke(nameof(SetDashingSpeed), 0.25f);
        }
        else if (grounded)
        {
            if (state == MovementState.air)
            {
                anim.SetBool("Jumping", false);
            }
            state = MovementState.walking;
            Invoke(nameof(SetMovementSpeed), 0.25f);
            anim.SetBool("Dashing", false);
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }
    private void FixedUpdate()
    {

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