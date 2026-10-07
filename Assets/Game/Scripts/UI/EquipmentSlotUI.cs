using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField] private Image equipmentIcon;

    private int equipmentSlotIndex;

    public event Action<int> OnSlotClicked;

    public void SetSlotIndex(int index)
    {
        equipmentSlotIndex = index;
    }

    public void OnClick()
    {
        OnSlotClicked?.Invoke(equipmentSlotIndex);
    }

    public void SetItem(EquipmentItem item)
    {
        equipmentIcon.enabled = true;
        equipmentIcon.sprite = item.data._icon;
    }

    public void Clear()
    {
        equipmentIcon.sprite = null;
        equipmentIcon.enabled = false;
    }
}
