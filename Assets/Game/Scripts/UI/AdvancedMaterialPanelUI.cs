using UnityEngine;

public class AdvancedMaterialPanelUI : MonoBehaviour
{
    [SerializeField] private AdvancedMaterialInventory advancedMaterialInventory;
    [SerializeField] private BackpackInventory backpackInventory;

    [SerializeField] private InventorySlotUI[] slots;
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private InventoryManager inventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateSlots();
        advancedMaterialInventory.OnInventoryChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            AdvancedMaterialItem item = advancedMaterialInventory.GetItem(i);

            if (item == null)
                slots[i].Clear();
            else
                slots[i].SetItem(item);
        }
    }
    private void CreateSlots()
    {
        int slotCount = advancedMaterialInventory.maxSlots;

        slots = new InventorySlotUI[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            slots[i] = Instantiate(slotPrefab, slotParent);

            slots[i].SetSlotIndex(i);

            slots[i].OnSlotClicked += OnSlotClicked;

        }
    }
    public void OnSlotClicked(int slotIndex)
    {
        inventoryManager.TransferAdvancedMaterialFromAdvancedMaterialInventoryToBackpackInventory(
            advancedMaterialInventory,
            backpackInventory,
            slotIndex
        );
    }
}
