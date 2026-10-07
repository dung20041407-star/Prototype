using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionStorageSlotUI : MonoBehaviour
{
    [SerializeField] private Image healingPotionItemIcon;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private HealingPotionStorage healingPotionStorage;

    private void Start()
    {
        healingPotionItemIcon.sprite = healingPotionStorage.healingPotionItem.data._icon;

        healingPotionStorage.OnPotionChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        healingPotionStorage.OnPotionChanged -= Refresh;
    }

    private void Refresh()
    {
        int amount = healingPotionStorage.healingPotionItem.amount;

        amountText.text = amount.ToString();

        if (amount == 0)
        {
            Color iconColor = healingPotionItemIcon.color;
            iconColor.a = 0.3f;           // 30% opacity
            healingPotionItemIcon.color = iconColor;

            Color textColor = amountText.color;
            textColor.a = 0.3f;
            amountText.color = textColor;
        }
        else
        {
            Color iconColor = healingPotionItemIcon.color;
            iconColor.a = 1f;
            healingPotionItemIcon.color = iconColor;

            Color textColor = amountText.color;
            textColor.a = 1f;
            amountText.color = textColor;
        }
    }
}
