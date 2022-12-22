using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillableEntityStats : EntityStats
{
    public Slider healthSlider;
    public GameObject healthSliderUI;
    public GameObject statusBar;
    public GameObject damageTakenText;
    public int ExpGiven;

    public override void Awake()
    {
        base.Awake();
        healthSlider.value = currentHealth / maxHealth;
    }

    public override void Regenerate(int health)
    {
        base.Regenerate(health);
        healthSlider.value = currentHealth / maxHealth;
        if (currentHealth == maxHealth)
        {
            healthSliderUI.SetActive(false);
        }
    }

    public override int TakeDamage(int damage)
    {
        var finalDamage = base.TakeDamage(damage);
        var damageTakenGameObject = Instantiate(damageTakenText);
        var obj = damageTakenGameObject.GetComponent<TMP_Text>();
        obj.SetText(finalDamage.ToString());
        damageTakenGameObject.transform.parent = statusBar.transform;
        damageTakenGameObject.transform.localPosition = Vector3.zero;
        damageTakenGameObject.transform.localRotation = Quaternion.identity;

        healthSliderUI.SetActive(true);
        healthSlider.value = currentHealth * 1.0f / maxHealth;
        if (currentHealth <= 0)
        {
            healthSliderUI.SetActive(false);
        }
        return finalDamage;
    }
}
