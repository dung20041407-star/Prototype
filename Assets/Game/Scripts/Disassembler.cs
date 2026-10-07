using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Disassembler : MonoBehaviour
{
    public InventoryItem currentItem;
    public event Action OnCurrentItemChanged;

    [SerializeField] private BackpackInventory backpackInventory;
    [SerializeField] private BasicMaterialInventory basicMaterialInventory;

    public bool SetItem(int slotIndex)
    {
        if (currentItem != null)
            return false;

        InventoryItem item = backpackInventory.Remove(slotIndex);

        if (item == null)
            return false;

        currentItem = item;
        OnCurrentItemChanged?.Invoke();
        return true;
    }
    public void  TakeOutItem()
    {
        if (currentItem == null)
            return;

        backpackInventory.Add(currentItem);
        currentItem = null;
        OnCurrentItemChanged?.Invoke();
    }
    public void DisassembleItem()
    {
        if (currentItem == null)
        {
            Debug.Log("No item in slot.");
            return;
        }

        int chitin = 0;
        int softTissue = 0;
        int proteinFiber = 0;
        int hemolymph = 0;

        if (currentItem is EquipmentItem equipmentItem)
        {
            chitin = equipmentItem.data._cost.chitinCost / 2;
            softTissue = equipmentItem.data._cost.softTissueCost / 2;
            proteinFiber = equipmentItem.data._cost.proteinFiberCost / 2;
            hemolymph = equipmentItem.data._cost.hemolymphCost / 2;
        }
        else if (currentItem is AdvancedMaterialItem advancedMaterialItem)
        {
            chitin = advancedMaterialItem.data._cost.chitinCost / 2;
            softTissue = advancedMaterialItem.data._cost.softTissueCost / 2;
            proteinFiber = advancedMaterialItem.data._cost.proteinFiberCost / 2;
            hemolymph = advancedMaterialItem.data._cost.hemolymphCost / 2;
        }
        else if (currentItem is HealingPotionItem healingPotionItem)
        {
            chitin = healingPotionItem.data._cost.chitinCost / 2;
            softTissue = healingPotionItem.data._cost.softTissueCost / 2;
            proteinFiber = healingPotionItem.data._cost.proteinFiberCost / 2;
            hemolymph = healingPotionItem.data._cost.hemolymphCost / 2;
        }

        basicMaterialInventory.AddMaterial(BasicMaterialType.ChitinFragment, chitin);
        basicMaterialInventory.AddMaterial(BasicMaterialType.SoftTissue, softTissue);
        basicMaterialInventory.AddMaterial(BasicMaterialType.ProteinFiber, proteinFiber);
        basicMaterialInventory.AddMaterial(BasicMaterialType.Hemolymph, hemolymph);

        Debug.Log(
            $"+ Chitin Fragment: {chitin}\n" +
            $"+ Soft Tissue: {softTissue}\n" +
            $"+ Protein Fiber: {proteinFiber}\n" +
            $"+ Hemolymph: {hemolymph}"
        );

        currentItem = null;
        OnCurrentItemChanged?.Invoke();
    }
}
