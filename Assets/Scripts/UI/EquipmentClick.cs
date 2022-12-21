using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        EquipmentManager.instance.UnEquip(
            (int)System.Enum.Parse(typeof(EquipmentSlot), gameObject.name)
        );
    }
}
