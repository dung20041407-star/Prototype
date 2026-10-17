using Unity.VisualScripting;
using UnityEngine;

public class AvatarButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject backpackPanel;
    [SerializeField] private GameObject playerStatsPanel;
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject equipmentInventoryPanel;
    [SerializeField] private GameObject advancedMaterialInventoryPanel;

    [SerializeField] private BasicMaterialPanelUI basicMaterialPanelUI;

    [SerializeField] private EquipmentButtonUI equipmentButtonUI;
    [SerializeField] private AdvancedMaterialButtonUI advancedMaterialButtonUI;

    [SerializeField] private bool isPanelOpen;

    [SerializeField] private BackpackUI backpackUI;

    [SerializeField] private CraftingPanelButtonUI craftingPanelButtonUI;

    public void Toggle()
    {
        if (isPanelOpen)
        {
            Close();
            return;
        }
        Open(); 
    }

    public void Close()
    {
        isPanelOpen = false;

        backpackPanel.SetActive(false);
        playerStatsPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        craftingPanel.SetActive(false);
        equipmentInventoryPanel.SetActive(false);
        advancedMaterialInventoryPanel.SetActive(false);
        basicMaterialPanelUI.Open();
    }
    public void Open()
    {
        if (craftingPanelButtonUI.IsPanelOpen || equipmentButtonUI.IsPanelOpen || advancedMaterialButtonUI.IsPanelOpen)
        {
            Close();
            return;
        }
        equipmentButtonUI.Close();
        advancedMaterialButtonUI.Close();
        craftingPanelButtonUI.Close();
        basicMaterialPanelUI.Close();
        isPanelOpen = true;

        backpackUI.SetMode(BackpackMode.Equipment);

        backpackPanel.SetActive(true);
        playerStatsPanel.SetActive(true);
        equipmentPanel.SetActive(true);
    }
}
