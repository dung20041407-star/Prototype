using System;
using System.Collections.Generic;

[Serializable]
public class HomeSaveData
{
    public PlayerSaveData playerSaveData;
    public BasicMaterialSaveData basicMaterialSaveData;
    public BackpackSaveData backpackSaveData;
    public EquipmentManagerSaveData equipmentManagerSaveData;
}

[Serializable]
public class PlayerSaveData
{
    public int level;
    public int currentExp;
    public int expToNextLevel;

    public int baseHp;
    public float currentHpRatio;

    public int baseHpDamage;
    public int baseStabilityDamage;
    public int baseDefense;
    public float baseAttackSpeed;
    public float baseMoveSpeed;
}
[Serializable]
public class BasicMaterialSaveData
{
    public int chitinFragment;
    public int proteinFiber;
    public int softTissue;
    public int hemolymph;
}
[Serializable]
public class BackpackSaveData
{
    public List<BackpackItemSaveData> items;
}

[Serializable]
public class BackpackItemSaveData
{
    public BackpackItemType itemType;
    public int id;

    public int durability;
    public int amount;
}
public enum BackpackItemType
{
    Equipment,
    AdvancedMaterial,
    HealingPotion
}
[Serializable]
public class EquipmentManagerSaveData
{
    public int clubBackpackSlotIndex;
    public int spearBackpackSlotIndex;
    public int armorBackpackSlotIndex;
    public int greavesBackpackSlotIndex;
    public int gauntletsBackpackSlotIndex;
    public int bootsBackpackSlotIndex;
}
