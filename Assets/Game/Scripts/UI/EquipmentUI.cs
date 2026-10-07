using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private EquipmentSlotUI[] slots;
    [SerializeField] private EquipmentManager equipmentManager;

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlotIndex(i);
            slots[i].OnSlotClicked += OnSlotClicked;
        }
        equipmentManager.OnEquipmentChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            EquipmentItem item = equipmentManager.GetEquippedItem(i);
            if (item == null)
                slots[i].Clear();
            else
                slots[i].SetItem(item);
        }
    }
    public void OnSlotClicked(int equipmentSlotIndex)
    {
        equipmentManager.UnequipItem(equipmentSlotIndex);
    }
}
