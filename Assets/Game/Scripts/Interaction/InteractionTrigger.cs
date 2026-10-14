using Unity.VisualScripting;
using UnityEngine;
public enum InteractionType
{
    Crafting,
    EquipmentInventory,
    AdvancedMaterialInventory
}
public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] private bool isDetected;
    [SerializeField] private InteractionType interactionType;

    public bool IsDetected => isDetected;

    public void SetDetected(bool value)
    {
        isDetected = value;

        if (PersistentCanvas.Instance == null)
            return;

        if (isDetected)
        {
            OpenInteraction();
        }
        else
        {
            CloseInteraction();
        }
    }

    private void OpenInteraction()
    {
        switch (interactionType)
        {
            case InteractionType.Crafting:
                PersistentCanvas.Instance.CraftingPanelButtonUI.Open();
                break;

            case InteractionType.EquipmentInventory:
                PersistentCanvas.Instance.EquipmentButtonUI.Open();
                break;

            case InteractionType.AdvancedMaterialInventory:
                PersistentCanvas.Instance.AdvancedMaterialButtonUI.Open();
                break;
        }
    }

    private void CloseInteraction()
    {
        switch (interactionType)
        {
            case InteractionType.Crafting:
                PersistentCanvas.Instance.CraftingPanelButtonUI.Close();
                break;

            case InteractionType.EquipmentInventory:
                PersistentCanvas.Instance.EquipmentButtonUI.Close();
                break;

            case InteractionType.AdvancedMaterialInventory:
                PersistentCanvas.Instance.AdvancedMaterialButtonUI.Close();
                break;
        }
    }
}
