using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMaterialInventory : MonoBehaviour
{
    [SerializeField] SaveSystem saveSystem;
    [Header("Use arrow keys to add materials")]
    [Header("Add Amount")]
    [SerializeField] private int addAmount = 1;

    public event Action OnInventoryChanged;

    private Dictionary<BasicMaterialType, int> materials;

    private void Awake()
    {
        materials = new Dictionary<BasicMaterialType, int>();
        materials.Add(BasicMaterialType.ChitinFragment, 0);
        materials.Add(BasicMaterialType.ProteinFiber, 0);
        materials.Add(BasicMaterialType.SoftTissue, 0);
        materials.Add(BasicMaterialType.Hemolymph, 0);
    }

    private void Update()
    {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                AddMaterial(BasicMaterialType.ChitinFragment, addAmount);

            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                AddMaterial(BasicMaterialType.SoftTissue, addAmount);

            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                AddMaterial(BasicMaterialType.ProteinFiber, addAmount);

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                AddMaterial(BasicMaterialType.Hemolymph, addAmount);  
    }

    public void AddMaterial(BasicMaterialType materialType, int amount)
    {
        materials[materialType] += amount;
        OnInventoryChanged?.Invoke();
        saveSystem.SaveHome();
    }

    public bool SpendMaterial(BasicMaterialType materialType, int amount)
    {
        if (materials[materialType] < amount)
            return false;

        materials[materialType] -= amount;
        OnInventoryChanged?.Invoke();
        return true;
    }

    public int GetMaterialAmount(BasicMaterialType materialType)
    {
        return materials[materialType];
    }

    public void SetMaterialAmount(BasicMaterialType materialType, int amount)
    {
        materials[materialType] = amount;
        OnInventoryChanged?.Invoke();
    }
}
