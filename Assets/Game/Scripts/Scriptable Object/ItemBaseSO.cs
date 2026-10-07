using UnityEngine;
[System.Serializable]
public struct MaterialCost
{
    public int chitinCost;
    public int softTissueCost;
    public int proteinFiberCost;
    public int hemolymphCost;
}

public abstract class ItemBaseSO : ScriptableObject
{
    public int _ID;
    public string _itemName;
    public Sprite _icon;
    [TextArea] public string _description;
    public MaterialCost _cost;
}
