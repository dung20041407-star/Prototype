using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class HealingPotionStorage : MonoBehaviour
{
    [SerializeField] private HealingPotionSO data;
    public HealingPotionItem healingPotionItem;

    public event Action OnPotionChanged;

    private void Awake()
    {
        healingPotionItem = new HealingPotionItem(data, 0);
    }

    public void AddPotion(int amount)
    {
        healingPotionItem.amount += amount;
        OnPotionChanged?.Invoke();
    }
    public HealingPotionItem RemovePotion(int amount)
    {
        if (healingPotionItem.amount < amount)
            return null;

        healingPotionItem.amount -= amount;
        OnPotionChanged?.Invoke();

        return new HealingPotionItem(data, amount);
    }
    public bool AtMaxCapacity()
    {
        return healingPotionItem.amount >= 99;
    }
    public void LoadFromSave(HealingPotionStorageSaveData data)
    {
        healingPotionItem.amount = data.amount;
        OnPotionChanged?.Invoke();
    }
}
