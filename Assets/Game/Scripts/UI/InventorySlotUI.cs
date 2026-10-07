using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private TMP_Text durabilityText;
    [SerializeField] private GameObject equipIcon;

    public event Action<int> OnSlotClicked;

    private int slotIndex;

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }
    public int GetSlotIndex()
    {
        return slotIndex;
    }
    public void SetItem(InventoryItem item)
    {
        icon.enabled = true;

        if (item is EquipmentItem equipment)
        {
            equipIcon.SetActive(equipment.isEquipped);
            icon.sprite = equipment.data._icon;
            amountText.text = "";
            durabilityText.text = equipment.durability.ToString();
        }
        else if (item is AdvancedMaterialItem material)
        {
            equipIcon.SetActive(false);
            icon.sprite = material.data._icon;
            amountText.text = material.amount.ToString();
            durabilityText.text = "";
        }
        else if (item is HealingPotionItem potion)
        {
            equipIcon.SetActive(false);
            icon.sprite = potion.data._icon;
            amountText.text = potion.amount.ToString();
            durabilityText.text = "";
        }
    }

    public void Clear()
    {
        icon.sprite = null;
        icon.enabled = false;
        equipIcon.SetActive(false );
        amountText.text = "";
        durabilityText.text = "";
    }
    public void OnClick()
    {
        OnSlotClicked?.Invoke(slotIndex);
    }
}
