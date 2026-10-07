using UnityEngine;

public enum BasicMaterialType
{
    ChitinFragment,
    SoftTissue,
    ProteinFiber,
    Hemolymph
}

[CreateAssetMenu(menuName = "Items/Basic Material")] 
public class BasicMaterialSO : ItemBaseSO
{
    public BasicMaterialType type;
}
