using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inventory.instance.items.Count > int.Parse(gameObject.name) - 1)
            (Inventory.instance.items[int.Parse(gameObject.name) - 1] as Equipment).Use();
    }
}
