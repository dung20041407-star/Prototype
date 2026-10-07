using UnityEngine;
public enum BackpackMode
{
    Equipment,
    TransferToEquipment,
    TransferToAdvancedMaterial,
    TransferToHealingPotion,
    TransferToDisassembler,
    TransferToRepair,
    None
}
public class BackpackUI : MonoBehaviour
{
    [SerializeField] private BackpackInventory backpackInventory;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private InventorySlotUI[] slots;
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private EquipmentInventory equipmentInventory;
    [SerializeField] private AdvancedMaterialInventory advancedMaterialInventory;
    [SerializeField] private HealingPotionStorage healingPotionStorage;
    [SerializeField] private Disassembler disassembler;
    [SerializeField] private RepairStorage repairStorage;

    private BackpackMode currentMode;
    public void SetMode(BackpackMode mode)
    {
        currentMode = mode;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        CreateSlots();
    }

    private void Start()
    {
        backpackInventory.OnInventoryChanged += Refresh;
        equipmentManager.OnEquipmentChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            InventoryItem item = backpackInventory.GetItem(i);

            if (item == null)
                slots[i].Clear();
            else
                slots[i].SetItem(item);
        }
    }
    private void CreateSlots()
    {
        int slotCount = backpackInventory.maxSlot;
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
        InventoryItem item = backpackInventory.GetItem(slotIndex);

        if (item == null)
            return;

        switch (currentMode)
        {
            case BackpackMode.Equipment:

                if (item is EquipmentItem equipment)
                {
                    equipmentManager.EquipItem(equipment);
                }
                else
                {
                    Debug.Log("item is not equipment!");
                }

                    break;

            case BackpackMode.TransferToEquipment:

                inventoryManager.TransferEquipmentFromBackpackInventoryToEquipmentInventory(
                    backpackInventory,
                    equipmentInventory,
                    slotIndex
                );

                break;

            case BackpackMode.TransferToAdvancedMaterial:

                inventoryManager.TransferAdvancedMaterialFromBackpackInventoryToAdvancedMaterialInventory(
                    backpackInventory,
                    advancedMaterialInventory,
                    slotIndex
                );

                break;

            case BackpackMode.TransferToHealingPotion:

                inventoryManager.TransferHealingPotionFromBackpackInventoryToHealingPotionStorage(
                    backpackInventory,
                    healingPotionStorage,
                    slotIndex
                );

                break;

            case BackpackMode.TransferToDisassembler:

                disassembler.SetItem(slotIndex);
                break;

            case BackpackMode.TransferToRepair:
                repairStorage.SetItem(slotIndex);
                break;

            case BackpackMode.None:
                break;
        }
    }
}
