using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("References")]
    public CameraMovement cameraMovement;
    public Animator anim;
    public bool dead = false;
    [Header("Statistics")]
    public int health;
    public int maxHealth = 100;
    public int mana;
    public int maxMana = 100;
    public int stamina;
    public int maxStamina = 100;
    void Start()
    {
        health = maxHealth;
        mana = maxMana;
        stamina = maxStamina;
        InvokeRepeating(nameof(Dying), 2.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            return;
        }
        if (health < 0)
        {
            health = 0;
        }
        if (health == 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            cameraMovement.enabled = false;
            anim.SetBool("Death", true);
            dead = true;
        }
    }

    void Dying()
    {

        health -= 10;
    }
}
