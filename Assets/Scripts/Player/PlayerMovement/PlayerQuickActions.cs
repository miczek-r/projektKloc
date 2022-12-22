using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerQuickActions : MonoBehaviour
{
    public GameObject melee;
    public GameObject offHand;
    public GameObject armorParent;
    private Animator _anim;
    private PlayerInput _playerInput;
    public GameObject enemy;
    public GameObject enemyParent;
    public bool hasBow = false;

    void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _playerInput = new PlayerInput();

        _playerInput.Player.Quick1.started += OnQuick1;
    }

    public void OnQuick1(InputAction.CallbackContext context)
    {
        var enemyObj = Instantiate(enemy);
        enemyObj.transform.SetParent(enemyParent.transform);
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

    public void EquipModel(Equipment itemToEquip)
    {
        if (itemToEquip.equipSlot == EquipmentSlot.Weapon)
        {
            var weapon = Instantiate(itemToEquip.gameObject);
            weapon.transform.SetParent(melee.transform);
            weapon.SetActive(true);
            _anim.SetFloat(
                "WeaponType",
                (itemToEquip.gameObject.name.Contains("OneHanded")) ? 1 : 2
            );
            weapon.transform.localPosition = new Vector3(-0.13f, 0, -0.05f);
            weapon.transform.localRotation = Quaternion.identity;
            melee.transform.Find("Fist").gameObject.SetActive(false);
            if (weapon.name.Contains("Bow"))
            {
                hasBow = true;
                _anim.SetFloat("WeaponType", 4);
                weapon.transform.localRotation = Quaternion.Euler(-20, 100, 0);
            }
        }
        else if (itemToEquip.gameObject != null)
            armorParent.transform.Find(itemToEquip.gameObject.name).gameObject.SetActive(true);
    }

    public void UnEquipModel(Equipment oldItem)
    {
        hasBow = false;
        if (oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            foreach (Transform child in melee.transform)
            {
                if (child.name != "Fist")
                    Destroy(child.gameObject);
            }
            _anim.SetFloat("WeaponType", 0);
            melee.transform.Find("Fist").gameObject.SetActive(true);
        }
        else
        {
            armorParent.transform.Find(oldItem.gameObject.name).gameObject.SetActive(false);
        }
    }

    private void SetWeapon(int weaponNumber)
    {
        hasBow = false;
        HideAllWeapons();
        switch (weaponNumber)
        {
            case 0:

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
