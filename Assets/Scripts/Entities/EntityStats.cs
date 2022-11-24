using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    private bool _isDead = false;
    public bool IsDead
    {
        get { return _isDead; }
    }
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public virtual void Die() { }

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void Regenerate(int health)
    {
        if (currentHealth == maxHealth)
            return;

        currentHealth += health;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            _isDead = true;
        }
    }
}
