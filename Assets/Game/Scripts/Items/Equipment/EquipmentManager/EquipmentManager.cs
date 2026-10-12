using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum WeaponHandState
{
    None,
    Blunt,
    Sharp
}
public class EquipmentManager : MonoBehaviour
{
    [SerializeField]private List<EquipmentSlot> slots;
    [SerializeField] private BackpackInventory backpackInventory;
    [SerializeField] private SaveSystem saveSystem;
    public WeaponHandState currentWeaponState;

    public event Action OnEquipmentChanged;
    private void Awake()
    {
        slots = new List<EquipmentSlot>();

        EquipmentSlot clubSlot = new EquipmentSlot();
        clubSlot.type = EquipmentType.Club;
        slots.Add(clubSlot);

        EquipmentSlot spearSlot = new EquipmentSlot();
        spearSlot.type = EquipmentType.Spear;
        slots.Add(spearSlot);

        EquipmentSlot armorSlot = new EquipmentSlot();
        armorSlot.type = EquipmentType.Armor;
        slots.Add(armorSlot);

        EquipmentSlot greavesSlot = new EquipmentSlot();
        greavesSlot.type = EquipmentType.Greaves;
        slots.Add(greavesSlot);

        EquipmentSlot gauntletsSlot = new EquipmentSlot();
        gauntletsSlot.type = EquipmentType.Gauntlets;
        slots.Add(gauntletsSlot);

        EquipmentSlot bootsSlot = new EquipmentSlot();
        bootsSlot.type = EquipmentType.Boots;
        slots.Add(bootsSlot);
    }

    public void EquipItem(EquipmentItem item)
    {
        if (item == null)
        {
            return;
        }
        EquipmentSlot targetSlot = null;
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.type == item.data.type)
            {
                targetSlot = slot;
                break;
            }
        }
        if (targetSlot == null)
        {
            Debug.Log("No matching equipment slot.");
            return;
        }
        if(item == targetSlot.equippedItem)
        {
            Debug.Log($"{item.data.name} is already equipped.");
            return ;
        }
        if (targetSlot.equippedItem != null)
        {
            targetSlot.equippedItem.isEquipped = false;
            Debug.Log($"Unequipped {targetSlot.equippedItem.data.name}");
        }
        targetSlot.equippedItem = item;
        item.isEquipped = true;
        OnEquipmentChanged?.Invoke();
        Debug.Log($"Equipped {item.data.name}");
        saveSystem.SaveHome();
    }
    public void UnequipItem(int equipmentSlotIndex)
    {
        if (equipmentSlotIndex < 0 || equipmentSlotIndex >= slots.Count)
        {
            Debug.Log("Invalid equipment slot.");
            return;
        }

        EquipmentSlot slot = slots[equipmentSlotIndex];

        if (slot.equippedItem == null)
        {
            Debug.Log("No equipment in this slot.");
            return;
        }

        slot.equippedItem.isEquipped = false;
        Debug.Log($"Unequipped {slot.equippedItem.data.name}");

        slot.equippedItem = null;
        OnEquipmentChanged?.Invoke();
        saveSystem.SaveHome();
    }

    public List<EquipmentItem> GetEquippedItems()
    {
        List<EquipmentItem> equippedItems = new List<EquipmentItem>();

        foreach (EquipmentSlot slot in slots)
        {
            if (slot.equippedItem != null)
            {
                equippedItems.Add(slot.equippedItem);
            }
        }

        return equippedItems;
    }
    public EquipmentItem GetEquippedItem(int equipmentSlotIndex)
    {
        if (equipmentSlotIndex < 0 || equipmentSlotIndex >= slots.Count)
            return null;

        return slots[equipmentSlotIndex].equippedItem;
    }
}
