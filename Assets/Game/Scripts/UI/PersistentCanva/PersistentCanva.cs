using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    public static PersistentCanvas Instance { get; private set; }

    public CraftingPanelButtonUI CraftingPanelButtonUI { get; private set; }
    public EquipmentButtonUI EquipmentButtonUI { get; private set; }

    public AdvancedMaterialButtonUI AdvancedMaterialButtonUI { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        CraftingPanelButtonUI = GetComponentInChildren<CraftingPanelButtonUI>(true);

        EquipmentButtonUI = GetComponentInChildren<EquipmentButtonUI>(true);

        AdvancedMaterialButtonUI = GetComponentInChildren<AdvancedMaterialButtonUI>(true);

        DontDestroyOnLoad(gameObject);
    }
}
