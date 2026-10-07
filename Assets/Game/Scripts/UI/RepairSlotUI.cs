using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepairSlotUI : MonoBehaviour
{
    [SerializeField] private Image repairedItemIcon;
    [SerializeField] private TMP_Text durabilityText;
    [SerializeField] private RepairStorage repairStorage;

    private void Start()
    {
        repairStorage.OnCurrentItemChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        repairStorage.OnCurrentItemChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        EquipmentItem equipment = repairStorage.currentItem;

        if (equipment != null)
        {
            repairedItemIcon.enabled = true;
            repairedItemIcon.sprite = equipment.data._icon;

            durabilityText.enabled = true;
            durabilityText.text = equipment.durability.ToString();
        }
        else
        {
            repairedItemIcon.sprite = null;
            repairedItemIcon.enabled = false;

            durabilityText.text = "";
            durabilityText.enabled = false;
        }
    }
}
