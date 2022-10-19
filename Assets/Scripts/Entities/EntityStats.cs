using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    private bool _isDead = false;
    public bool IsDead { get { return _isDead; } }
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public virtual void Die()
    {

    }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Regenerate(int health)
    {
        if (currentHealth == maxHealth) return;

        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
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
