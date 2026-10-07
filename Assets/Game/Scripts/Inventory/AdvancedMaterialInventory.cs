using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
public class AdvancedMaterialInventory : ItemInventoryBase
{
    private List<AdvancedMaterialItem> items;
    [SerializeField] public int maxSlots = 10;
    public event Action OnInventoryChanged;
    private void Awake()
    {
        items = new List<AdvancedMaterialItem>();
    }

    private void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
            LogAllItems();
    }
    public bool Add(AdvancedMaterialItem item)
    {
        if (item == null)
            return false;

        foreach (AdvancedMaterialItem existingItem in items)
        {
            if (existingItem.data == item.data)
            {
                existingItem.amount += item.amount;
                OnInventoryChanged?.Invoke();

                Debug.Log($"Increased {item.data.name} to {existingItem.amount}");

                return true;
            }
        }

        if (IsFull())
        {
            Debug.Log("Material inventory is full.");
            return false;
        }

        items.Add(item);
        OnInventoryChanged?.Invoke();

        Debug.Log($"Added {item.data.name} to slot {items.Count - 1}");

        return true;
    }
    public AdvancedMaterialItem RemoveItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
            return null;

        AdvancedMaterialItem item = items[slotIndex];

        AdvancedMaterialItem removedItem = new AdvancedMaterialItem();
        removedItem.data = item.data;
        removedItem.amount = 1;

        item.amount--;
        OnInventoryChanged?.Invoke();

        if (item.amount == 0)
        {
            items.RemoveAt(slotIndex);
            OnInventoryChanged?.Invoke();
            Debug.Log($"Removed last {item.data.name}");
        }
        else
        {
            Debug.Log($"Decreased {item.data.name} to {item.amount}");
        }

        return removedItem;
    }
    public void LogAllItems()
    {
        Debug.Log("===ADVANCED MATERIAL INVENTORY CONTENTS ===");

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log($"Slot {i}: {items[i].data.name} x{items[i].amount}");
        }
    }
    public bool IsFull()
    {
        return items.Count >= maxSlots;
    }
    public AdvancedMaterialItem GetItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return null;
        }

        return items[slotIndex];
    }
}
