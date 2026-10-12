using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private BackpackInventory backpackInventory;
    [SerializeField] private EquipmentInventory equipmentInventory;
    [SerializeField] private AdvancedMaterialInventory advancedMaterialInventory;
    [SerializeField] private HealingPotionStorage healingPotionStorage;
    [SerializeField] private SaveSystem saveSystem;
    public void TransferEquipmentFromBackpackInventoryToEquipmentInventory(BackpackInventory from, EquipmentInventory to, int slotIndex)
    {
        if (to.IsFull())
        {
            Debug.Log("Equipment inventory is full!");
            return;
        }

        InventoryItem item = from.GetItem(slotIndex);

        if (item == null)
            return;

        if (item is not EquipmentItem)
        {
            Debug.Log("This item is not equipment.");
            return;
        }

        EquipmentItem equipment = (EquipmentItem)from.Remove(slotIndex);

        if (equipment == null)
            return;

        to.Add(equipment);
        saveSystem.SaveHome();

    }
    public void TransferEquipmentFromEquipmentInventoryToBackpackInventory(EquipmentInventory from,BackpackInventory to,int slotIndex)
    {
        if (to.IsFull())
        {
            Debug.Log("Backpack is full!");
            return;
        }
        EquipmentItem item = from.Remove(slotIndex);

        if (item == null)
            return;
        to.Add(item);
        saveSystem.SaveHome();
    }
    public void TransferAdvancedMaterialFromBackpackInventoryToAdvancedMaterialInventory(BackpackInventory from, AdvancedMaterialInventory to, int slotIndex)
    {
        InventoryItem item = from.GetItem(slotIndex);

        if (item == null)
            return;

        if (item is not AdvancedMaterialItem)
        {
            Debug.Log("This item is not advanced material.");
            return;
        }

        AdvancedMaterialItem advancedMaterial = (AdvancedMaterialItem)from.Remove(slotIndex);

        if (advancedMaterial == null)
            return;

        if (!to.Add(advancedMaterial))
        {
            from.Add(advancedMaterial);
            Debug.Log("Transfer failed. Returned material to backpack.");
        }
        saveSystem.SaveHome();
    }
    public void TransferAdvancedMaterialFromAdvancedMaterialInventoryToBackpackInventory(AdvancedMaterialInventory from, BackpackInventory to, int slotIndex)
    {
        AdvancedMaterialItem item = from.RemoveItem(slotIndex);
        saveSystem.SaveHome();

        if (item == null)
            return;

        if (!to.Add(item))
        {
            from.Add(item);
            Debug.Log("Transfer failed. Returned material.");
        }
    }
    public void TransferHealingPotionFromHealingPotionStorageToBackpackInventory(HealingPotionStorage from, BackpackInventory to)
    {
        HealingPotionItem item = healingPotionStorage.RemovePotion(1);
        if (item == null)
            return;

        if (!backpackInventory.AddPotion(item))
        {
            healingPotionStorage.AddPotion(1);
        }
        saveSystem.SaveHome();
    }
    public void TransferHealingPotionFromBackpackInventoryToHealingPotionStorage(BackpackInventory from,HealingPotionStorage to,int slotIndex)
    {
        if (to.AtMaxCapacity())
        {
            Debug.Log("Healing Potion Storage is full.");
            return;
        }

        if (from.RemovePotion(slotIndex))
        {
            to.AddPotion(1);
            saveSystem.SaveHome();
        }
        else
        {
            Debug.Log("Invalid slot index!");
        }
    }

    public void TakeOutHealingPotion()
    {
        TransferHealingPotionFromHealingPotionStorageToBackpackInventory(
            healingPotionStorage,
            backpackInventory
        );
    }
}
