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
    public float currentExp = 0;
    public float nextLvlExp = 100;
    public float multiplie = 1.7f;
    public int Level = 1;
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

        mana = maxMana;
        stamina = maxStamina;
        InvokeRepeating(nameof(Regeneration), 2.0f, 1.0f);
    }

    void Regeneration()
    {
        Regenerate(1);
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
