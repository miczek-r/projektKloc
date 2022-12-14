using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

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
    public GameObject equipmentCamera;
    public GameObject aimReticle;
    public Rig rig;

    public CameraStyle currentStyle;

    float horizontalInput;
    float verticalInput;

    public enum CameraStyle
    {
        Basic,
        Aiming,
        Equipment
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
        CreateARay();

        Vector3 viewDir =
            transform.position
            - new Vector3(mainCamera.position.x, transform.position.y, mainCamera.position.z);
        orientation.forward = viewDir.normalized;
        if (currentStyle == CameraStyle.Basic)
        {
            Vector3 inputDir =
                orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(
                    playerObj.forward,
                    inputDir.normalized,
                    Time.deltaTime * rotationSpeed
                );
        }
        else if (currentStyle == CameraStyle.Aiming)
        {
            Vector3 dirToCombatLookAt =
                combatLookAt.position
                - new Vector3(
                    mainCamera.position.x,
                    combatLookAt.position.y,
                    mainCamera.position.z
                );
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    public LayerMask aimColiderMask = new LayerMask();
    public Transform debugTransform;
    public Vector3 mouseWorldPosition = Vector3.zero;

    private void CreateARay()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColiderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(true);
    }

    IEnumerator HideReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(false);
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
        if (currentStyle == CameraStyle.Equipment)
            return;
        if (!GetComponent<PlayerQuickActions>().hasBow)
            return;
        if (context.ReadValueAsButton())
        {
            rig.enabled = true;
            standardCamera.SetActive(false);
            aimCamera.SetActive(true);
            currentStyle = CameraStyle.Aiming;
            StartCoroutine(ShowReticle());
        }
        else
        {
            rig.enabled = false;
            currentStyle = CameraStyle.Basic;
            standardCamera.SetActive(true);
            aimCamera.SetActive(false);
            StartCoroutine(HideReticle());
        }
    }

    public void OnInventory(bool isInventoryOpen)
    {
        if (!isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            equipmentCamera.SetActive(false);
            standardCamera.SetActive(true);
            currentStyle = CameraStyle.Basic;
            Debug.Log("cursor hidden");
        }
        else
        {
            currentStyle = CameraStyle.Equipment;
            standardCamera.SetActive(false);
            aimCamera.SetActive(false);
            equipmentCamera.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("cursor visible");
        }
    }
}
