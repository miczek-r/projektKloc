using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform mainCamera;
    public Transform orientation;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject standardCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;

    public CameraStyle currentStyle;

    float horizontalInput;
    float verticalInput;
    public enum CameraStyle
    {
        Basic,
        Aiming
    }

    public PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput.Player.Move.started += OnMove;
        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;
        playerInput.Player.Aim.started += OnAim;
        playerInput.Player.Aim.canceled += OnAim;

    }

    private void Update()
    {

        Vector3 viewDir = transform.position - new Vector3(mainCamera.position.x, transform.position.y, mainCamera.position.z);
        orientation.forward = viewDir.normalized;
        if (currentStyle == CameraStyle.Basic)
        {
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        else if (currentStyle == CameraStyle.Aiming)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(mainCamera.position.x, combatLookAt.position.y, mainCamera.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }

    void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;

    }

    void OnEnable()
    {
        playerInput.Player.Enable();
    }
    void OnDisable()
    {
        playerInput.Player.Disable();
    }

    void OnAim(InputAction.CallbackContext context)
    {
        Debug.Log("Aiming");
        if (context.ReadValueAsButton())
        {
            standardCamera.SetActive(false);
            aimCamera.SetActive(true);

            //Allow time for the camera to blend before enabling the UI
            StartCoroutine(ShowReticle());
        }
        else
        {
            standardCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
        }
    }
}
