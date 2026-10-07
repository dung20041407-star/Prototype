using UnityEngine;

public class BasicMaterialPanelUI : MonoBehaviour
{
    [SerializeField] private BasicMaterialInventory inventory;

    [SerializeField] private BasicMaterialSO chitinFragment;
    [SerializeField] private BasicMaterialSO softTissue;
    [SerializeField] private BasicMaterialSO proteinFiber;
    [SerializeField] private BasicMaterialSO hemolymph;

    [SerializeField] private BasicMaterialUI chitinFragmentUI;
    [SerializeField] private BasicMaterialUI softTissueUI;
    [SerializeField] private BasicMaterialUI proteinFiberUI;
    [SerializeField] private BasicMaterialUI hemolymphUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory.OnInventoryChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        chitinFragmentUI.SetItem(chitinFragment._icon, inventory.materials[BasicMaterialType.ChitinFragment]);

        softTissueUI.SetItem(softTissue._icon, inventory.materials[BasicMaterialType.SoftTissue]);

        proteinFiberUI.SetItem(proteinFiber._icon, inventory.materials[BasicMaterialType.ProteinFiber]);

        hemolymphUI.SetItem(hemolymph._icon, inventory.materials[BasicMaterialType.Hemolymph]);
    }
}
