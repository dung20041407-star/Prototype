using Unity.VisualScripting;
using UnityEngine;

public class AvatarButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject backpackPanel;
    [SerializeField] private GameObject playerStatsPanel;
    [SerializeField] private GameObject equipmentPanel;

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

        equipmentButtonUI.Close();
        advancedMaterialButtonUI.Close();
        craftingPanelButtonUI.Close();

        isPanelOpen = true;

        backpackUI.SetMode(BackpackMode.Equipment);

        backpackPanel.SetActive(true);
        playerStatsPanel.SetActive(true);
        equipmentPanel.SetActive(true);
    }

    public void Close()
    {
        isPanelOpen = false;

        backpackPanel.SetActive(false);
        playerStatsPanel.SetActive(false);
        equipmentPanel.SetActive(false);
    }
}
