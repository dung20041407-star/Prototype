using UnityEngine;

public enum AdvancedMaterialType
{
    Weapon,
    Armor
}

[CreateAssetMenu(menuName = "Items/Advanced Material")]
public class AdvancedMaterialSO : ItemBaseSO
{
    public AdvancedMaterialType type;
}
