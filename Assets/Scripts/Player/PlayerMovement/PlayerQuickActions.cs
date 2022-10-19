using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerQuickActions : MonoBehaviour
{

    private Animator anim;
    public GameObject melee;


    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void OnQuickSwap(InputValue value)
    {
        if (anim.GetInteger("WeaponType") == 0)
        {
            melee.transform.Find("Fist").gameObject.SetActive(false);
            melee.transform.Find("Sword").gameObject.SetActive(true);
            anim.SetInteger("WeaponType", 1);
        }
        else
        {
            melee.transform.Find("Fist").gameObject.SetActive(true);
            melee.transform.Find("Sword").gameObject.SetActive(false);
            anim.SetInteger("WeaponType", 0);
        }
    }

    public void OnQuick1(InputValue value)
    {
        Debug.Log("Sword");
        melee.transform.Find("Fist").gameObject.SetActive(false);
        melee.transform.Find("Sword").gameObject.SetActive(true);
        anim.SetInteger("WeaponType", 1);
    }
    public void OnQuick2(InputValue value)
    {
        Debug.Log("Fist");
        melee.transform.Find("Fist").gameObject.SetActive(true);
        melee.transform.Find("Sword").gameObject.SetActive(false);
        anim.SetInteger("WeaponType", 0);
    }

}
