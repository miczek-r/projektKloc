using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More EquipmentManagers found");
            return;
        }
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;

    PlayerQuickActions equipmentModelManager;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start()
    {
        equipmentModelManager = GetComponent<PlayerQuickActions>();
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] is not null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged is not null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        if (oldItem is not null)
            equipmentModelManager.UnEquipModel(oldItem);
        equipmentModelManager.EquipModel(newItem);
        currentEquipment[slotIndex] = newItem;
    }

    public void UnEquip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged is not null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            equipmentModelManager.UnEquipModel(oldItem);
        }
    }
}
