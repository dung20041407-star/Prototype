using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] EquipmentManager equipmentManager;
    int baseHp = 1000;
    int baseHpDamage = 100;
    int baseStabilityDamage = 100;
    int baseDefense = 10;
    float baseAttackSpeed = 1;
    float baseMoveSpeed = 1;

    public int maxHp;
    public int hpDamage;
    public int stabilityDamage;
    public int defense;
    public float attackSpeed;
    public float moveSpeed;

    public int currentHp;

    public event Action OnHPChanged;

    [Header("Testing Apply Damage(< or > to apply)")]
    [SerializeField] private int damage;
    private void Awake()
    {
        RefreshStats(equipmentManager.GetEquippedItems());
        currentHp = maxHp;
    }
    // Update is called once per frame
    void Update()
    {
        RefreshStats(equipmentManager.GetEquippedItems());

        if (Keyboard.current.commaKey.wasPressedThisFrame)
        {
            TakeDamage(damage);
        }
        if (Keyboard.current.periodKey.wasPressedThisFrame)
        {
            Heal(damage);
        }
    }
    void RefreshStats(List<EquipmentItem> equipments)
    {
        float hpPercent = 1f;

        if (maxHp > 0)
        {
            hpPercent = (float)currentHp / maxHp;
        }

        ResetStatsToBaseStats();

        foreach (EquipmentItem item in equipments)
        {
            ApplyEquipment(item);
        }

        currentHp = Mathf.RoundToInt(maxHp * hpPercent);
    }
    void ResetStatsToBaseStats()
    {
        maxHp = baseHp;
        hpDamage = baseHpDamage;
        stabilityDamage = baseStabilityDamage;
        defense = baseDefense;
        attackSpeed = baseAttackSpeed;
        moveSpeed = baseMoveSpeed;
    }
    void ApplyEquipment(EquipmentItem item)
    {
        if (item == null || item.data == null)
            return;
        if (item.data.type == EquipmentType.Spear)
        {
            if (equipmentManager.currentWeaponState != WeaponHandState.Sharp)
                return;
        }

        if (item.data.type == EquipmentType.Club)
        {
            if (equipmentManager.currentWeaponState != WeaponHandState.Blunt)
                return;
        }
        maxHp += Mathf.RoundToInt(baseHp * item.data.HpPercent / 100f);

        hpDamage += Mathf.RoundToInt(baseHpDamage * item.data.DamagePercent / 100f);

        stabilityDamage += Mathf.RoundToInt(baseStabilityDamage * item.data.StabilityDamagePercent / 100f);

        defense += Mathf.RoundToInt(baseDefense * item.data.DefensePercent / 100f);

        attackSpeed += baseAttackSpeed * item.data.AttackSpeedPercent / 100f;

        moveSpeed += baseMoveSpeed * item.data.MoveSpeedPercent / 100f;
    }
    public void IncreaseMaxHp(int amount)
    {
        baseHp += amount;
    }


    public void IncreaseDamage(int amount)
    {
        baseHpDamage += amount;
        baseStabilityDamage += amount;
    }
    public void TakeDamage(int amount)
    {
        if(amount <= 0) return;
        currentHp -= amount;
        currentHp = Mathf.Max(currentHp, 0);

        OnHPChanged?.Invoke();
    }
    public void Heal(int amount)
    {
        if(amount <= 0) return;
        currentHp += amount;
        currentHp = Mathf.Min(currentHp, maxHp);

        OnHPChanged?.Invoke();
    }

}
