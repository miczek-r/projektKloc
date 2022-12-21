using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform equipmentParent;

    Inventory inventory;
    EquipmentManager equipmentManager;

    InventorySlot[] slots;
    private GameObject _inventoryLayout;
    public GameObject player;

    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateEquipmentUI;
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateBackpackUI;
        _inventoryLayout = transform.GetChild(0).gameObject;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventoryLayout.SetActive(!_inventoryLayout.active);
            UpdateBackpackUI();
            player.GetComponent<CameraController>().OnInventory(_inventoryLayout.active);
            player.GetComponent<PlayerStateMachine>().enabled = !_inventoryLayout.active;
        }
        ;
    }

    void UpdateBackpackUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void UpdateEquipmentUI(Equipment newItem, Equipment oldItem)
    {
        if (newItem is not null)
        {
            equipmentParent
                .Find(newItem.equipSlot.ToString())
                .GetComponent<InventorySlot>()
                .AddItem(newItem);
        }
        else if (oldItem is not null)
        {
            equipmentParent
                .Find(oldItem.equipSlot.ToString())
                .GetComponent<InventorySlot>()
                .ClearSlot();
        }
    }
}
