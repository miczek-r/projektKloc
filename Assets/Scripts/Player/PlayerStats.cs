using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    [Header("References")]
    public CameraController cameraMovement;
    public Animator anim;
    public bool dead = false;
    [Header("Statistics")]
    public int mana;
    public int maxMana = 100;
    public int stamina;
    public int maxStamina = 100;

    [Header("Bars")]
    public HealthBarSlider healthBarSlider;
    public StaminaBar staminaBar;
    public override void Die()
    {
        CancelInvoke();
        cameraMovement.enabled = false;
        anim.SetBool("Death", true);
        dead = true;
    }

    void Start()
    {
        mana = maxMana;
        stamina = maxStamina;
        InvokeRepeating(nameof(Regeneration), 2.0f, 1.0f);
        healthBarSlider.setMaxHealth(maxHealth);
        healthBarSlider.setHealth(currentHealth/5);
        staminaBar.setMaxStamina(stamina);
    }

    void Regeneration()
    {
        Regenerate(1);
        healthBarSlider.setHealth(currentHealth);
        stamina += 5;
        staminaBar.setStamina(stamina);
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

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem is not null)
        {
            armor.AddModifier(newItem.armorModifier);
            armor.AddModifier(newItem.damageModifier);
        }

        if (oldItem is not null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            armor.RemoveModifier(oldItem.damageModifier);
        }
    }
}
