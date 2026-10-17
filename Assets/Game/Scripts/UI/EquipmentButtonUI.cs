using UnityEngine;

public class EquipmentButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject equipmentInventoryPanel;
    [SerializeField] private GameObject backpackPanel;

    [SerializeField] private AvatarButtonUI avatarButtonUI;
    [SerializeField] private AdvancedMaterialButtonUI advancedMaterialButtonUI;

    [SerializeField] private bool isPanelOpen;
    public bool IsPanelOpen => isPanelOpen;

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
        equipmentInventoryPanel.SetActive(false);
    }
    public void Open()
    {
        avatarButtonUI.Close();
        advancedMaterialButtonUI.Close();
        craftingPanelButtonUI.Close();

        isPanelOpen = true;

        backpackUI.SetMode(BackpackMode.TransferToEquipment);

        backpackPanel.SetActive(true);
        equipmentInventoryPanel.SetActive(true);
    }
}
