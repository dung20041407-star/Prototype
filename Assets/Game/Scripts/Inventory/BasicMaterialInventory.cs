using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMaterialInventory : MonoBehaviour
{
    [SerializeField] SaveSystem saveSystem;

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
    public void AddChitinFragment()
    {
        AddMaterial(BasicMaterialType.ChitinFragment, addAmount);
    }
    public void AddProteinFiber()
    {
        AddMaterial(BasicMaterialType.ProteinFiber, addAmount);
    }
    public void AddSoftTissue()
    {
        AddMaterial(BasicMaterialType.SoftTissue, addAmount);
    }
    public void AddHemoLymph()
    {
        AddMaterial(BasicMaterialType.Hemolymph, addAmount);
    }
}
