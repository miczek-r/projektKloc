using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerQuickActions : MonoBehaviour
{

    public GameObject melee;
    public GameObject offHand;
    private Animator _anim;
    private PlayerInput _playerInput;
    public bool hasBow = false;


    void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _playerInput = new PlayerInput();

        _playerInput.Player.Quick1.started += OnQuick1;
        _playerInput.Player.Quick2.started += OnQuick2;
        _playerInput.Player.Quick3.started += OnQuick3;
        _playerInput.Player.Quick4.started += OnQuick4;
        _playerInput.Player.Quick5.started += OnQuick5;
    }
    public void OnQuick1(InputAction.CallbackContext context)
    {
        SetWeapon(0);
    }
    public void OnQuick2(InputAction.CallbackContext context)
    {
        SetWeapon(1);
    }
    public void OnQuick3(InputAction.CallbackContext context)
    {
        SetWeapon(2);
    }
    public void OnQuick4(InputAction.CallbackContext context)
    {
        SetWeapon(3);
    }
    public void OnQuick5(InputAction.CallbackContext context)
    {
        SetWeapon(4);
    }

    private void SetWeapon(int weaponNumber)
    {
        hasBow = false;
        HideAllWeapons();
        switch (weaponNumber)
        {
            case 0:
                melee.transform.Find("Fist").gameObject.SetActive(true);
                _anim.SetFloat("WeaponType", 0);
                break;
            case 1:
                offHand.transform.Find("Shield").gameObject.SetActive(true);
                melee.transform.Find("OneHanded").gameObject.SetActive(true);
                _anim.SetFloat("WeaponType", 1);
                break;
            case 2:
                melee.transform.Find("TwoHanded").gameObject.SetActive(true);
                _anim.SetFloat("WeaponType", 2);
                break;
            case 3:
                melee.transform.Find("Spear").gameObject.SetActive(true);
                _anim.SetFloat("WeaponType", 3);
                break;
            case 4:
                melee.transform.Find("Bow").gameObject.SetActive(true);
                _anim.SetFloat("WeaponType", 4);
                hasBow = true;
                break;
        }
    }

    private void HideAllWeapons()
    {
        offHand.transform.Find("Shield").gameObject.SetActive(false);
        foreach (Transform child in melee.transform)
        {
            child.gameObject.SetActive(false);
        }
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
