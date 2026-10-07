using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;

public class EquipmentInventory : MonoBehaviour
{
    private List<EquipmentItem> equipments;
    [SerializeField] public int maxSlot = 10;
    public event Action OnInventoryChanged;
    private void Awake()
    {
        equipments = new List<EquipmentItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
            LogAllItems();

    }
    public void Add(EquipmentItem item)
    {
        if (item == null) return;

        if (IsFull())
        {
            Debug.Log("Equipment inventory is full.");
            return;
        }

        equipments.Add(item);

        OnInventoryChanged?.Invoke();

        Debug.Log($"Added {item.data.name} to slot {equipments.Count - 1}");
    }
    public EquipmentItem Remove(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= equipments.Count)
            return null;

        EquipmentItem item = equipments[slotIndex];

        equipments.RemoveAt(slotIndex);

        OnInventoryChanged?.Invoke();

        Debug.Log($"Removed {item.data.name} from slot {slotIndex}");

        return item;
    }
    public void LogAllItems()
    {
        Debug.Log("===EQUIPMENT INVENTORY CONTENTS ===");

        for (int i = 0; i < equipments.Count; i++)
        {
            Debug.Log($"Slot {i}: {equipments[i].data.name} : durability {equipments[i].durability}");
        }
    }

    public bool IsFull()
    {
        return equipments.Count >= maxSlot;
    }
    public EquipmentItem GetItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= equipments.Count)
        {
            return null;
        }

        return equipments[slotIndex];
    }
}
