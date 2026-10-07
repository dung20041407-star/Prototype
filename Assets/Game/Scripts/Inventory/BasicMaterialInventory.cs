using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMaterialInventory : MonoBehaviour
{
    [Header("use arrrow key to add materials respectively")]
    [SerializeField] private bool addOrRemove = false;
    [Header("add = true, remove = false")]
    [Header("Add Amount")]
    [SerializeField] private int addAmount = 1;

    public event Action OnInventoryChanged;

    public Dictionary<BasicMaterialType, int> materials;
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
        if (addOrRemove)
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
        else
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                if (SpendMaterial(BasicMaterialType.ChitinFragment, addAmount))
                    Debug.Log("Spend Success");
                else
                    Debug.Log("Not Enough Material");
            }
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                if (SpendMaterial(BasicMaterialType.SoftTissue, addAmount))
                    Debug.Log("Spend Success");
                else
                    Debug.Log("Not Enough Material");
            }
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                if (SpendMaterial(BasicMaterialType.ProteinFiber, addAmount))
                    Debug.Log("Spend Success");
                else
                    Debug.Log("Not Enough Material");
            }
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                if (SpendMaterial(BasicMaterialType.Hemolymph, addAmount))
                    Debug.Log("Spend Success");
                else
                    Debug.Log("Not Enough Material");
            }
        }
    }
    public void AddMaterial(BasicMaterialType materialType, int amount)
    {
        materials[materialType] += amount;
        OnInventoryChanged?.Invoke();
    }
    public bool SpendMaterial(BasicMaterialType materialType, int amount)
    {
        if (materials[materialType] < amount)
            return false;

        materials[materialType] -= amount;
        OnInventoryChanged?.Invoke();

        return true;
    }
    public void LogAllMaterials()
    {
        Debug.Log("=== INVENTORY CONTENTS ===");
        foreach (KeyValuePair<BasicMaterialType, int> pair in materials)
        {
            Debug.Log($"{pair.Key}: {pair.Value}");
        }
    }
}
