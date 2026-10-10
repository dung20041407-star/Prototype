using System;
using System.Collections.Generic;

[Serializable]
public class HomeSaveData
{
    public PlayerSaveData playerSaveData;
    public BasicMaterialSaveData basicMaterialSaveData;
    public BackpackSaveData backpackSaveData;
    public EquipmentManagerSaveData equipmentManagerSaveData;
    public AdvancedMaterialInventorySaveData advancedMaterialInventorySaveData;
    public EquipmentInventorySaveData equipmentInventorySaveData;
    public RepairStorageSaveData repairStorageSaveData;
    public HealingPotionStorageSaveData healingPotionStorageSaveData;
    public DisassemblerSaveData disassemblerSaveData;
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
[Serializable]
public class AdvancedMaterialInventorySaveData
{
    public List<AdvancedMaterialItemSaveData> items;
}

[Serializable]
public class AdvancedMaterialItemSaveData
{
    public int id;
    public int amount;
}
[Serializable]
public class EquipmentInventorySaveData
{
    public List<EquipmentItemSaveData> items;
}

[Serializable]
public class EquipmentItemSaveData
{
    public int id;
    public int durability;
}
[Serializable]
public class RepairStorageSaveData
{
    public int id;
    public int durability;
}
[Serializable]
public class HealingPotionStorageSaveData
{
    public int amount;
}
[Serializable]
public class DisassemblerSaveData
{
    public BackpackItemType itemType;
    public int id;
    public int durability;
}
