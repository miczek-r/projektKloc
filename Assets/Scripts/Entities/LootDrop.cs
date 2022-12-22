using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemToDrop : System.Object
{
    public Item itemToDrop;
    public int probability;
}

public class LootDrop : MonoBehaviour
{
    [SerializeField]
    public ItemToDrop[] Loot;
    public GameObject bag;
    System.Random rand = new System.Random();

    public void GetDrop(Vector3 position)
    {
        foreach (ItemToDrop itemToDrop in Loot)
        {
            var aChance = rand.Next(1, 100);
            if (aChance <= itemToDrop.probability)
            {
                var drop = Instantiate(bag);
                drop.GetComponent<ItemPickup>().item = itemToDrop.itemToDrop;
                drop.transform.position = position;
            }
        }
    }
}
