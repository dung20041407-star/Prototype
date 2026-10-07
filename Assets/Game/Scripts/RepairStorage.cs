using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class RepairStorage : MonoBehaviour
{
    public EquipmentItem currentItem;

    public event Action OnCurrentItemChanged;

    [SerializeField] private BackpackInventory backpackInventory;

    [SerializeField] private int slotIndex;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SetItem(slotIndex);
        }
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            TakeOutItem();
        }
    }

    public bool SetItem(int slotIndex)
    {
        if (currentItem != null)
        {
            Debug.Log("Repair storage is already occupied.");
            return false;
        }

        InventoryItem item = backpackInventory.GetItem(slotIndex);

        if (item == null)
        {
            Debug.Log("No item in this backpack slot.");
            return false;
        }

        if (item is not EquipmentItem)
        {
            Debug.Log("Selected item is not equipment.");
            return false;
        }

        EquipmentItem equipmentItem = (EquipmentItem)backpackInventory.Remove(slotIndex);

        if (equipmentItem == null)
            return false;

        currentItem = equipmentItem;
        OnCurrentItemChanged?.Invoke();

        Debug.Log($"Moved {equipmentItem.data._itemName} to Repair Storage.");

        return true;
    }

    public void TakeOutItem()
    {
        if (currentItem == null)
            return;

        if (!backpackInventory.Add(currentItem))
            return;

        currentItem = null;
        OnCurrentItemChanged?.Invoke();
    }

    public bool HasItem()
    {
        return currentItem != null;
    }
    public void NotifyCurrentItemChanged()
    {
        OnCurrentItemChanged?.Invoke();
    }
}
