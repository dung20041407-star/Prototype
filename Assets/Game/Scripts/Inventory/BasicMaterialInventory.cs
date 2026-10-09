using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMaterialInventory : MonoBehaviour
{
    [Header("Use arrow keys to add/remove materials")]
    [SerializeField] private bool addOrRemove = false;

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
