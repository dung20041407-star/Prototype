using System.Collections.Generic;
using UnityEngine;
public enum EquipmentTier
{
    Basic,
    Advanced
}
public enum EquipmentType
{
    Club,
    Spear,
    Armor,
    Greaves,
    Gauntlets,
    Boots
}
[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentSO : ItemBaseSO
{
    public EquipmentTier tier;
    public EquipmentType type;
    public List<EquipmentEffectSO> effects;

    public int maxDurability;

    public int HpPercent;
    public int DamagePercent;
    public int StabilityDamagePercent;
    public int DefensePercent;
    public float AttackSpeedPercent;
    public float MoveSpeedPercent;
}
