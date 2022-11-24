using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private List<GameObject> itemsInRange = new();
    public GameObject PickupsPanel;
    public GameObject PickupText;
    private Inventory _inventory;
    public bool canPickup = false;

    public void Start()
    {
        _inventory = Inventory.instance;
    }

    public void TryAdd(List<GameObject> itemPickups)
    {
        if (!Enumerable.SequenceEqual(itemPickups, itemsInRange))
        {
            itemsInRange = itemPickups;
            GeneratePickupList();
        }
    }

    private void GeneratePickupList()
    {
        if (itemsInRange.Count > 0)
        {
            canPickup = true;
            transform.GetChild(0).gameObject.SetActive(true);
            foreach (Transform child in PickupsPanel.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (var itemInRange in itemsInRange)
            {
                var textObject = Instantiate(PickupText);
                textObject.transform.SetParent(PickupsPanel.transform);
                textObject.GetComponent<TMP_Text>().text = itemInRange
                    .GetComponent<ItemPickup>()
                    .item.name;
                textObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            canPickup = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void PickupItems()
    {
        foreach (var item in itemsInRange)
        {
            item.GetComponent<ItemPickup>().PickUp();
        }
    }
}
