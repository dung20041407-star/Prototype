using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveSystem : MonoBehaviour
{
    private string homeSavePath;
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private BasicMaterialInventory basicMaterialInventory;
    [SerializeField] private BackpackInventory backpackInventory;
    [SerializeField] private EquipmentManager equipmentManager;

    [SerializeField] private List<EquipmentSO> equipmentSOs;
    [SerializeField] private List<AdvancedMaterialSO> advancedMaterialSOs;
    [SerializeField] private List<HealingPotionSO> healingPotionSOs;

    private void Awake()
    {
        homeSavePath = Path.Combine(
            Application.persistentDataPath,
            "home_save.json"
        );
    }

    private void Start()
    {
        LoadHome();
    }

    private void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SaveHome();
        }
    }

    public void SaveHome()
    {
        HomeSaveData data = new HomeSaveData();

        data.playerSaveData = new PlayerSaveData();
        data.basicMaterialSaveData = new BasicMaterialSaveData();
        data.backpackSaveData = SaveBackpack();
        data.equipmentManagerSaveData = SaveEquipmentManager();

        // =========================
        // LevelSystem
        // =========================

        data.playerSaveData.level = levelSystem.Level;
        data.playerSaveData.currentExp = levelSystem.CurrentExp;
        data.playerSaveData.expToNextLevel = levelSystem.ExpToNextLevel;


        // =========================
        // PlayerStats
        // =========================

        data.playerSaveData.baseHp = playerStats.BaseHp;
        data.playerSaveData.baseHpDamage = playerStats.BaseHpDamage;
        data.playerSaveData.baseStabilityDamage = playerStats.BaseStabilityDamage;
        data.playerSaveData.baseDefense = playerStats.BaseDefense;
        data.playerSaveData.baseAttackSpeed = playerStats.BaseAttackSpeed;
        data.playerSaveData.baseMoveSpeed = playerStats.BaseMoveSpeed;
        data.playerSaveData.currentHpRatio = playerStats.CurrentHpRatio;


        // =========================
        // BasicMaterialInventory
        // =========================

        data.basicMaterialSaveData.chitinFragment =
            basicMaterialInventory.GetMaterialAmount(
                BasicMaterialType.ChitinFragment
            );

        data.basicMaterialSaveData.softTissue =
            basicMaterialInventory.GetMaterialAmount(
                BasicMaterialType.SoftTissue
            );

        data.basicMaterialSaveData.proteinFiber =
            basicMaterialInventory.GetMaterialAmount(
                BasicMaterialType.ProteinFiber
            );

        data.basicMaterialSaveData.hemolymph =
            basicMaterialInventory.GetMaterialAmount(
                BasicMaterialType.Hemolymph
            );


        // =========================
        // Convert to JSON
        // =========================

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(homeSavePath, json);
    }

    public void LoadHome()
    {
        if (!File.Exists(homeSavePath))
        {
            Debug.Log("No Home Save found.");
            return;
        }

        string json = File.ReadAllText(homeSavePath);
        HomeSaveData data =
            JsonUtility.FromJson<HomeSaveData>(json);

        // 1. Player base stats
        playerStats.LoadFromSave(data.playerSaveData);

        // 2. Level
        levelSystem.LoadFromSave(data.playerSaveData);

        // 3. Basic materials
        basicMaterialInventory.SetMaterialAmount(
            BasicMaterialType.ChitinFragment,
            data.basicMaterialSaveData.chitinFragment
        );

        basicMaterialInventory.SetMaterialAmount(
            BasicMaterialType.SoftTissue,
            data.basicMaterialSaveData.softTissue
        );

        basicMaterialInventory.SetMaterialAmount(
            BasicMaterialType.ProteinFiber,
            data.basicMaterialSaveData.proteinFiber
        );

        basicMaterialInventory.SetMaterialAmount(
            BasicMaterialType.Hemolymph,
            data.basicMaterialSaveData.hemolymph
        );

        // 4. Backpack
        LoadBackpack(data.backpackSaveData);

        // 5. Equipment
        LoadEquipmentManager(data.equipmentManagerSaveData);

        // 6. Restore HP
        playerStats.RestoreHpRatio(
            data.playerSaveData.currentHpRatio
        );
    }
    public BackpackSaveData SaveBackpack()
    {
        BackpackSaveData data = new BackpackSaveData();
        data.items = new List<BackpackItemSaveData>();

        foreach (InventoryItem item in backpackInventory.GetItems())
        {
            BackpackItemSaveData itemData = new BackpackItemSaveData();

            if (item is EquipmentItem equipment)
            {
                itemData.itemType = BackpackItemType.Equipment;
                itemData.id = equipment.data._ID;
                itemData.durability = equipment.durability;
            }
            else if (item is AdvancedMaterialItem material)
            {
                itemData.itemType = BackpackItemType.AdvancedMaterial;
                itemData.id = material.data._ID;
                itemData.amount = material.amount;
            }
            else if (item is HealingPotionItem potion)
            {
                itemData.itemType = BackpackItemType.HealingPotion;
                itemData.id = potion.data._ID;
                itemData.amount = potion.amount;
            }

            data.items.Add(itemData);
        }

        return data;
    }

    private EquipmentSO FindEquipmentSO(int id)
    {
        foreach (EquipmentSO data in equipmentSOs)
        {
            if (data._ID == id)
                return data;
        }

        return null;
    }

    private AdvancedMaterialSO FindAdvancedMaterialSO(int id)
    {
        foreach (AdvancedMaterialSO data in advancedMaterialSOs)
        {
            if (data._ID == id)
                return data;
        }

        return null;
    }

    private HealingPotionSO FindHealingPotionSO(int id)
    {
        foreach (HealingPotionSO data in healingPotionSOs)
        {
            if (data._ID == id)
                return data;
        }

        return null;
    }
    public void LoadBackpack(BackpackSaveData data)
    {
        if (data == null)
            return;

        backpackInventory.ClearItems();

        foreach (BackpackItemSaveData itemData in data.items)
        {
            switch (itemData.itemType)
            {
                case BackpackItemType.Equipment:
                    {
                        EquipmentSO equipmentSO = FindEquipmentSO(itemData.id);

                        if (equipmentSO == null)
                        {
                            Debug.LogWarning(
                                $"Could not find EquipmentSO with ID {itemData.id}"
                            );
                            continue;
                        }

                        EquipmentItem equipmentItem = new EquipmentItem(equipmentSO);
                        equipmentItem.durability = itemData.durability;

                        if (!backpackInventory.Add(equipmentItem))
                        {
                            Debug.LogWarning(
                                $"Failed to add Equipment ID {itemData.id} to backpack."
                            );
                        }

                        break;
                    }

                case BackpackItemType.AdvancedMaterial:
                    {
                        AdvancedMaterialSO materialSO =
                            FindAdvancedMaterialSO(itemData.id);

                        if (materialSO == null)
                        {
                            Debug.LogWarning(
                                $"Could not find AdvancedMaterialSO with ID {itemData.id}"
                            );
                            continue;
                        }

                        AdvancedMaterialItem materialItem =
                            new AdvancedMaterialItem();

                        materialItem.data = materialSO;
                        materialItem.amount = itemData.amount;

                        if (!backpackInventory.Add(materialItem))
                        {
                            Debug.LogWarning(
                                $"Failed to add Advanced Material ID {itemData.id} to backpack."
                            );
                        }

                        break;
                    }

                case BackpackItemType.HealingPotion:
                    {
                        HealingPotionSO potionSO =
                            FindHealingPotionSO(itemData.id);

                        if (potionSO == null)
                        {
                            Debug.LogWarning(
                                $"Could not find HealingPotionSO with ID {itemData.id}"
                            );
                            continue;
                        }

                        HealingPotionItem potionItem =
                            new HealingPotionItem(
                                potionSO,
                                itemData.amount
                            );

                        if (!backpackInventory.AddPotion(potionItem))
                        {
                            Debug.LogWarning(
                                $"Failed to add Healing Potion ID {itemData.id} to backpack."
                            );
                        }

                        break;
                    }
            }
        }
    }

    private EquipmentManagerSaveData SaveEquipmentManager()
    {
        EquipmentManagerSaveData data = new EquipmentManagerSaveData();

        data.clubBackpackSlotIndex =
            FindBackpackSlotIndex(equipmentManager.GetEquippedItem(0));

        data.spearBackpackSlotIndex =
            FindBackpackSlotIndex(equipmentManager.GetEquippedItem(1));

        data.armorBackpackSlotIndex =
            FindBackpackSlotIndex(equipmentManager.GetEquippedItem(2));

        data.greavesBackpackSlotIndex =
            FindBackpackSlotIndex(equipmentManager.GetEquippedItem(3));

        data.gauntletsBackpackSlotIndex =
            FindBackpackSlotIndex(equipmentManager.GetEquippedItem(4));

        data.bootsBackpackSlotIndex =
            FindBackpackSlotIndex(equipmentManager.GetEquippedItem(5));

        return data;
    }
    private int FindBackpackSlotIndex(EquipmentItem equipment)
    {
        if (equipment == null)
            return -1;

        IReadOnlyList<InventoryItem> items =
            backpackInventory.GetItems();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == equipment)
                return i;
        }

        return -1;
    }
    private void LoadEquipmentToSlot(int backpackSlotIndex)
    {
        if (backpackSlotIndex < 0)
            return;

        InventoryItem item =
            backpackInventory.GetItem(backpackSlotIndex);

        if (item is not EquipmentItem equipment)
        {
            Debug.LogWarning(
                $"Could not load equipment from Backpack slot {backpackSlotIndex}."
            );

            return;
        }

        equipmentManager.EquipItem(equipment);
    }
    public void LoadEquipmentManager(EquipmentManagerSaveData data)
    {
        if (data == null)
            return;

        LoadEquipmentToSlot(data.clubBackpackSlotIndex);
        LoadEquipmentToSlot(data.spearBackpackSlotIndex);
        LoadEquipmentToSlot(data.armorBackpackSlotIndex);
        LoadEquipmentToSlot(data.greavesBackpackSlotIndex);
        LoadEquipmentToSlot(data.gauntletsBackpackSlotIndex);
        LoadEquipmentToSlot(data.bootsBackpackSlotIndex);
    }
}
