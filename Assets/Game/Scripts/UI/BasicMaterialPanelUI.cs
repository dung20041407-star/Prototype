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

    private void Start()
    {
        inventory.OnInventoryChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        chitinFragmentUI.SetItem(
            chitinFragment._icon,
            inventory.GetMaterialAmount(BasicMaterialType.ChitinFragment)
        );

        softTissueUI.SetItem(
            softTissue._icon,
            inventory.GetMaterialAmount(BasicMaterialType.SoftTissue)
        );

        proteinFiberUI.SetItem(
            proteinFiber._icon,
            inventory.GetMaterialAmount(BasicMaterialType.ProteinFiber)
        );

        hemolymphUI.SetItem(
            hemolymph._icon,
            inventory.GetMaterialAmount(BasicMaterialType.Hemolymph)
        );
    }
}
