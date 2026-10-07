using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BasicMaterialUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;
    
    public void SetItem(Sprite icon, int amount)
    {
        this.icon.sprite = icon;
        amountText.text = amount.ToString();
    }
}
