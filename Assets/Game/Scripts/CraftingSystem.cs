using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField]private BasicMaterialInventory basicMaterialInventory;
    [SerializeField]private BackpackInventory backpackInventory;
    [SerializeField] private HealingPotionStorage healingPotionStorage;
    [SerializeField] private HealingPotionSO healingPotionSO;
    [SerializeField] private EquipmentSO currentRecipe;
    [SerializeField] private RepairStorage repairStorage;

    void CraftEquipment(EquipmentSO data)
    {
        if (!HasEnoughMaterials(data._cost))
        {
            Debug.Log("Not enough materials");
            return;
        }
        if (backpackInventory.IsFull())
        {
            Debug.Log("Backpack is full.");
            return;
        }
        basicMaterialInventory.SpendMaterial(BasicMaterialType.ChitinFragment, data._cost.chitinCost);
        basicMaterialInventory.SpendMaterial(BasicMaterialType.SoftTissue, data._cost.softTissueCost);
        basicMaterialInventory.SpendMaterial(BasicMaterialType.ProteinFiber, data._cost.proteinFiberCost);
        basicMaterialInventory.SpendMaterial(BasicMaterialType.Hemolymph, data._cost.hemolymphCost);
        Debug.Log($"Enough materials. Create 1 {data._itemName}.");
        backpackInventory.CreateEquipment(data);
    }
    bool HasEnoughMaterials(MaterialCost cost)
    {
        return basicMaterialInventory.GetMaterialAmount(
                   BasicMaterialType.ChitinFragment) >= cost.chitinCost

            && basicMaterialInventory.GetMaterialAmount(
                   BasicMaterialType.ProteinFiber) >= cost.proteinFiberCost

            && basicMaterialInventory.GetMaterialAmount(
                   BasicMaterialType.SoftTissue) >= cost.softTissueCost

            && basicMaterialInventory.GetMaterialAmount(
                   BasicMaterialType.Hemolymph) >= cost.hemolymphCost;
    }

    public void RepairEquipmentFromStorage()
    {
        EquipmentItem equipmentItem = repairStorage.currentItem;

        if (equipmentItem == null)
        {
            Debug.Log("No equipment to repair.");
            return;
        }

        if (equipmentItem.durability == equipmentItem.data.maxDurability)
        {
            Debug.Log("Equipment is at max durability");
            return;
        }

        float missingPercent = (float)(equipmentItem.data.maxDurability - equipmentItem.durability) / equipmentItem.data.maxDurability;

        MaterialCost repairCost = new MaterialCost();

        repairCost.chitinCost = Mathf.CeilToInt(equipmentItem.data._cost.chitinCost * missingPercent);

        repairCost.proteinFiberCost = Mathf.CeilToInt(equipmentItem.data._cost.proteinFiberCost * missingPercent);

        repairCost.softTissueCost = Mathf.CeilToInt(equipmentItem.data._cost.softTissueCost * missingPercent);

        repairCost.hemolymphCost = Mathf.CeilToInt(equipmentItem.data._cost.hemolymphCost * missingPercent);

        if (!HasEnoughMaterials(repairCost))
        {
            Debug.Log("Not enough materials.");
            return;
        }

        basicMaterialInventory.SpendMaterial(BasicMaterialType.ChitinFragment, repairCost.chitinCost);

        basicMaterialInventory.SpendMaterial(BasicMaterialType.SoftTissue, repairCost.softTissueCost);

        basicMaterialInventory.SpendMaterial(BasicMaterialType.ProteinFiber, repairCost.proteinFiberCost);

        basicMaterialInventory.SpendMaterial(BasicMaterialType.Hemolymph, repairCost.hemolymphCost);

        equipmentItem.durability = equipmentItem.data.maxDurability;

        repairStorage.NotifyCurrentItemChanged();

        Debug.Log($"{equipmentItem.data._itemName} repaired.");
    }

    void CraftHealingPotion(HealingPotionSO data)
    {
        if (!HasEnoughMaterials(data._cost))
        {
            Debug.Log("Not enough materials");
            return;
        }
        if (healingPotionStorage.AtMaxCapacity())
        {
            Debug.Log("Can not craft more Healing Potion!");
            return;
        }
        basicMaterialInventory.SpendMaterial(BasicMaterialType.ChitinFragment, data._cost.chitinCost);
        basicMaterialInventory.SpendMaterial(BasicMaterialType.SoftTissue, data._cost.softTissueCost);
        basicMaterialInventory.SpendMaterial(BasicMaterialType.ProteinFiber, data._cost.proteinFiberCost);
        basicMaterialInventory.SpendMaterial(BasicMaterialType.Hemolymph, data._cost.hemolymphCost);
        healingPotionStorage.AddPotion(1);
        Debug.Log($"Enough materials. Create 1 {data._itemName}. Current in storage: {healingPotionStorage.healingPotionItem.amount}");
    }
    public void CraftHealingPotionButton()
    {
        CraftHealingPotion(healingPotionSO);
    }

    public void SelectRecipe(EquipmentSO recipe)
    {
        currentRecipe = recipe;

        Debug.Log($"Selected recipe: {currentRecipe._itemName}");
    }

    public void CraftSelectedRecipe()
    {
        if (currentRecipe == null)
        {
            Debug.Log("No recipe selected.");
            return;
        }
        CraftEquipment(currentRecipe);
    }
    public void ClearSelectedRecipe()
    {
        if (currentRecipe == null)
            return;
        currentRecipe = null;
        RecipeButtonUI.ClearSelection();
        Debug.Log("Recipe selection cleared.");
    }
}
