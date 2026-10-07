using UnityEngine;

public class CraftingPanelButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject EquipmentCraftingPanel;
    [SerializeField] private GameObject EquipmentRepairingPanel;
    [SerializeField] private GameObject PotionCraftingPanel;
    [SerializeField] private GameObject DisassemblerPanel;
    [SerializeField] private GameObject backpackPanel;
    [SerializeField] private BackpackUI backpackUI;
    [SerializeField] private CraftingSystem craftingSystem;

    [SerializeField] private GameObject CraftingPanel;
    [SerializeField] private AdvancedMaterialButtonUI advancedMaterialButtonUI;
    [SerializeField] private EquipmentButtonUI equipmentButtonUI;
    [SerializeField] private AvatarButtonUI avatarButtonUI;
    public void ToggleEquipmentCraftingButtonPanel()
    {
        craftingSystem.ClearSelectedRecipe();
        EquipmentCraftingPanel.SetActive(true);
        EquipmentRepairingPanel.SetActive(false);
        PotionCraftingPanel.SetActive(false);
        DisassemblerPanel.SetActive(false);
        backpackPanel.SetActive(true);
        backpackUI.SetMode(BackpackMode.None);
    }

    public void ToggleEquipmentRepairingPanel()
    {
        craftingSystem.ClearSelectedRecipe();
        EquipmentRepairingPanel.SetActive(true);
        EquipmentCraftingPanel.SetActive(false);
        PotionCraftingPanel.SetActive(false);
        DisassemblerPanel.SetActive(false);
        backpackPanel.SetActive(true);
        backpackUI.SetMode(BackpackMode.TransferToRepair);
    }

    public void TogglePotionCraftingButtonPanel()
    {
        craftingSystem.ClearSelectedRecipe();
        EquipmentCraftingPanel.SetActive(false);
        EquipmentRepairingPanel.SetActive(false);
        PotionCraftingPanel.SetActive(true);
        DisassemblerPanel.SetActive(false);
        backpackPanel.SetActive(true);
        backpackUI.SetMode(BackpackMode.TransferToHealingPotion);
    }

    public void ToggleDisassemblerPanel()
    {
        craftingSystem.ClearSelectedRecipe();
        EquipmentCraftingPanel.SetActive(false);
        EquipmentRepairingPanel.SetActive(false);
        PotionCraftingPanel.SetActive(false);
        DisassemblerPanel.SetActive(true);
        backpackPanel.SetActive(true);
        backpackUI.SetMode(BackpackMode.TransferToDisassembler);
    }
    public void ToggleSelf()
    {
        if (CraftingPanel.activeSelf == false)
        {
            backpackUI.SetMode(BackpackMode.None);
            advancedMaterialButtonUI.Close();
            equipmentButtonUI.Close();
            avatarButtonUI.Close();

            CraftingPanel.SetActive(true);
            ToggleEquipmentCraftingButtonPanel();
        }
        else
        {
            Close();
        }
    }
    public void Close()
    {
        craftingSystem.ClearSelectedRecipe();
        CraftingPanel.SetActive(false);
        backpackPanel.SetActive(false);
    }
}
