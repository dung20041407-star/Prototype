using UnityEngine;

public class EquipmentItem : InventoryItem
{
    public EquipmentSO data;

    public int durability;

    public bool isEquipped;

    public EquipmentItem(EquipmentSO data)
    {
        this.data = data;
        this.durability = data.maxDurability;
    }
}
