using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityStats))]
public class EntityCombat : MonoBehaviour
{

    private EntityStats thisStats;

    // Start is called before the first frame update
    void Start()
    {
        thisStats = GetComponent<EntityStats>();
    }

    public void Attack(EntityStats targetStats)
    {
        targetStats.TakeDamage(thisStats.damage.GetValue());
    }
}
