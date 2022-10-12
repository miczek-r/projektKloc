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
        InvokeRepeating(nameof(Dying), 2.0f, 2.3f);
        InvokeRepeating(nameof(Regeneration), 2.0f, 1.0f);
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
            CancelInvoke();
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

    void Regeneration()
    {
        health += 1;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        stamina += 5;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        mana += 1;
        if (mana > maxMana)
        {
            mana = maxMana;
        }
    }
}
