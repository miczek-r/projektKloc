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
    public float currentExp = 0;
    public float nextLvlExp = 100;
    public float multiplie = 1.7f;
    public int Level = 1;
    EquipmentManager equipmentManager;

    public override void Die()
    {
        CancelInvoke();
        cameraMovement.enabled = false;
        anim.SetBool("Death", true);
        dead = true;
    }

    public void LevelUp()
    {
        if (currentExp >= nextLvlExp)
        {
            Level++;
            nextLvlExp *= multiplie;
            currentExp = 0;
        }
    }

    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += OnEquipmentChanged;
        mana = maxMana;
        stamina = maxStamina;
        InvokeRepeating(nameof(Regeneration), 2.0f, 1.0f);
        healthBarSlider.setMaxHealth(maxHealth);
        healthBarSlider.setHealth(currentHealth);
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

    public bool UseStamina(int staminaToUse)
    {
        if (stamina < staminaToUse)
            return false;
        stamina -= staminaToUse;
        return true;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBarSlider.setHealth(currentHealth);
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem is not null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem is not null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }
}
