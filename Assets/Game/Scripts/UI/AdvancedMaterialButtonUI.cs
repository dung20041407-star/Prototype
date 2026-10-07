using UnityEngine;

public class AdvancedMaterialButtonUI : MonoBehaviour
{
    [SerializeField] private bool isPanelOpen;
    [SerializeField] private GameObject backpackPanel;
    [SerializeField] private GameObject advancedMaterialInventoryPanel;
    [SerializeField] private AvatarButtonUI avatarButtonUI;
    [SerializeField] private EquipmentButtonUI equipmentButtonUI;
    [SerializeField] private BackpackUI backpackUI;
    [SerializeField] private CraftingPanelButtonUI craftingPanelButtonUI;

    public void Toggle()
    {
        if (isPanelOpen)
        {
            Close();
            return;
        }

        avatarButtonUI.Close();
        equipmentButtonUI.Close();
        craftingPanelButtonUI.Close();

        isPanelOpen = true;

        backpackUI.SetMode(BackpackMode.TransferToAdvancedMaterial);

        backpackPanel.SetActive(true);
        advancedMaterialInventoryPanel.SetActive(true);
    }
    public void Close()
    {
        isPanelOpen = false;

        backpackPanel.SetActive(false);
        advancedMaterialInventoryPanel.SetActive(false);
    }
}
