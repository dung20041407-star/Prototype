using UnityEngine;

public class HealingPotionItem : InventoryItem
{
    public HealingPotionSO data;
    public int amount;
    public HealingPotionItem(HealingPotionSO data, int amount)
    {
        this.data = data;
        this.amount = amount;
    }
}
