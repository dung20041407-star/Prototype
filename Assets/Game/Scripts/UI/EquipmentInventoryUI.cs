using UnityEngine;

public class EquipmentInventoryUI : MonoBehaviour
{
    [SerializeField] private EquipmentInventory equipmentInventory;
    [SerializeField] private BackpackInventory backpackInventory;
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private InventorySlotUI[] slots;
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotParent;

    private void Start()
    {
        CreateSlots();

        equipmentInventory.OnInventoryChanged += Refresh;

        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            EquipmentItem item = equipmentInventory.GetItem(i);

            if (item == null)
                slots[i].Clear();
            else
                slots[i].SetItem(item);
        }
    }

    private void CreateSlots()
    {
        int slotCount = equipmentInventory.maxSlot;

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
        inventoryManager.TransferEquipmentFromEquipmentInventoryToBackpackInventory(
            equipmentInventory,
            backpackInventory,
            slotIndex
        );
    }
}
