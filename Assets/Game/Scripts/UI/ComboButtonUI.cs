using UnityEngine;
using UnityEngine.UI;

public class ComboButtonUI : MonoBehaviour
{
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private Image buttonImage;

    [Header("Combo Icons")]
    [SerializeField] private Sprite bluntIcon;
    [SerializeField] private Sprite sharpIcon;

    private void OnEnable()
    {
        equipmentManager.OnEquipmentChanged += UpdateIcon;
        UpdateIcon();
    }

    private void OnDisable()
    {
        equipmentManager.OnEquipmentChanged -= UpdateIcon;
    }

    public void UpdateIcon()
    {
        buttonImage.sprite =
            equipmentManager.currentWeaponState == WeaponHandState.Blunt
            ? bluntIcon
            : sharpIcon;
    }
}
