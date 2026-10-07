using UnityEngine;
using UnityEngine.UI;
public class DisassembleSlotUI : MonoBehaviour
{
    [SerializeField] private Image disassembledItemIcon;
    [SerializeField] private Disassembler disassembler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        disassembler.OnCurrentItemChanged += UpdateIcon;
        UpdateIcon();
    }

    private void OnDestroy()
    {
        disassembler.OnCurrentItemChanged -= UpdateIcon;
    }
    public void UpdateIcon()
    {

        if (disassembler.currentItem is EquipmentItem equipment)
        {
            disassembledItemIcon.enabled = true;
            disassembledItemIcon.sprite = equipment.data._icon;
        }
        else if (disassembler.currentItem is AdvancedMaterialItem material)
        {
            disassembledItemIcon.enabled = true;
            disassembledItemIcon.sprite = material.data._icon;
        }
        else if (disassembler.currentItem is HealingPotionItem potion)
        {
            disassembledItemIcon.enabled = true;
            disassembledItemIcon.sprite = potion.data._icon;
        }
        else
        {
            disassembledItemIcon.sprite = null;
            disassembledItemIcon.enabled = false;
        }
    }
}
