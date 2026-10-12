using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackpackInventory : MonoBehaviour
{
    private List<InventoryItem> items = new();

    [Header("Test")]
    [SerializeField] private AdvancedMaterialSO testMaterial;
    [SerializeField] private int slotIndex;
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] public int maxSlot = 10;
    public event Action OnInventoryChanged;

    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
            CreateAdvancedMaterial(testMaterial);

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            DecreaseDurability(slotIndex, 10);
        }
    }

    public void CreateEquipment(EquipmentSO data)
    {
        if (data == null) return;
        EquipmentItem newItem = new EquipmentItem(data);
        items.Add(newItem);
        Debug.Log($"Added {newItem.data.name} to slot {items.Count - 1}");
        OnInventoryChanged?.Invoke();
    }

    public void CreateAdvancedMaterial(AdvancedMaterialSO material)
    {
        if (material == null) return;

        foreach (InventoryItem inventoryItem in items)
        {
            if (inventoryItem is AdvancedMaterialItem item)
            {
                if (item.data == material)
                {
                    item.amount++;
                    Debug.Log($"Increased {material.name} to {item.amount}");
                    OnInventoryChanged?.Invoke();
                    saveSystem.SaveHome();
                    return;
                }
            }
        }

        AdvancedMaterialItem newItem = new AdvancedMaterialItem();
        newItem.data = material;
        newItem.amount = 1;
        if (IsFull())
        {
            Debug.Log("Backpack is full.");
            return;
        }
        items.Add(newItem);
        saveSystem.SaveHome();
        Debug.Log($"Added {material.name} to slot {items.Count - 1}");
        OnInventoryChanged?.Invoke();
    }

    public bool Add(InventoryItem item)
    {
        if (item == null)
            return false;

        if (item is AdvancedMaterialItem newMaterial)
        {
            foreach (InventoryItem inventoryItem in items)
            {
                if (inventoryItem is AdvancedMaterialItem existingMaterial &&
                    existingMaterial.data == newMaterial.data)
                {
                    existingMaterial.amount += newMaterial.amount;

                    Debug.Log(
                        $"Added {newMaterial.amount} {newMaterial.data.name}. " +
                        $"Total: {existingMaterial.amount}"
                    );

                    OnInventoryChanged?.Invoke();
                    saveSystem.SaveHome();
                    return true;
                }
            }
        }

        if (IsFull())
        {
            Debug.Log("Backpack is full.");
            return false;
        }

        items.Add(item);

        OnInventoryChanged?.Invoke();
        saveSystem.SaveHome();
        return true;
    }
    public bool AddPotion(HealingPotionItem newPotion)
    {
        if (newPotion == null)
            return false;

        foreach (InventoryItem inventoryItem in items)
        {
            if (inventoryItem is HealingPotionItem potion)
            {
                if (potion.amount >= 5)
                {
                    Debug.Log("Healing Potion slot is full.");
                    return false;
                }

                int canAdd = Mathf.Min(newPotion.amount, 5 - potion.amount);

                potion.amount += canAdd;

                Debug.Log($"Added {canAdd} Healing Potion. Current: {potion.amount}");
                OnInventoryChanged?.Invoke();
                saveSystem.SaveHome();
                return true;
            }
        }

        if (newPotion.amount > 5)
            newPotion.amount = 5;
        if (IsFull())
        {
            Debug.Log("Backpack is full.");
            return false;
        }
        items.Add(newPotion);

        Debug.Log($"Created Healing Potion stack x{newPotion.amount}");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public InventoryItem Remove(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            Debug.Log("Remove failed: Invalid slot index.");
            return null;
        }

        InventoryItem item = items[slotIndex];


        if (item is EquipmentItem equipment)
        {
            if (equipment.isEquipped)
            {
                Debug.Log("Cannot remove equipped equipment.");
                return null;
            }
            items.RemoveAt(slotIndex);
            OnInventoryChanged?.Invoke();

            Debug.Log($"Removed equipment: {equipment.data.name}");
            saveSystem.SaveHome();
            return equipment;
        }


        if (item is AdvancedMaterialItem material)
        {
            material.amount--;

            AdvancedMaterialItem removedItem = new AdvancedMaterialItem();
            removedItem.data = material.data;
            removedItem.amount = 1;


            if (material.amount == 0)
            {
                items.RemoveAt(slotIndex);
                Debug.Log($"Removed last {material.data.name}");
            }
            else
            {
                Debug.Log($"Removed 1 {material.data.name}. Remaining: {material.amount}");
            }
            OnInventoryChanged?.Invoke();
            saveSystem.SaveHome();
            return removedItem;
        }
        Debug.Log("Remove failed: Unsupported item type.");
        return null;
    }
    public bool RemovePotion(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
            return false;

        if (items[slotIndex] is HealingPotionItem potion)
        {
            potion.amount--;

            if (potion.amount == 0)
                items.RemoveAt(slotIndex);
            OnInventoryChanged?.Invoke();
            saveSystem.SaveHome();
            return true;
        }
        return false;
    }

    public void DecreaseDurability(int slotIndex, int amount)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
            return;

        if (items[slotIndex] is EquipmentItem equipment)
        {
            equipment.durability -= amount;

            if (equipment.durability < 0)
                equipment.durability = 0;
            OnInventoryChanged?.Invoke();
            Debug.Log(
                $"Decreased {equipment.data.name} durability. Current: {equipment.durability}"
            );
        }
        saveSystem.SaveHome();
    }
    public InventoryItem GetItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return null;
        }

        return items[slotIndex];
    }

    public IReadOnlyList<InventoryItem> GetItems()
    {
        return items;
    }

    public bool IsFull()
    {
        return items.Count >= maxSlot;
    }

    public void NotifyInventoryChanged()
    {
        OnInventoryChanged?.Invoke();
    }
    public void ClearItems()
    {
        items.Clear();
        OnInventoryChanged?.Invoke();
    }
}
