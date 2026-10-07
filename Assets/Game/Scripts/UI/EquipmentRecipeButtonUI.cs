using UnityEngine;
using UnityEngine.UI;

public class RecipeButtonUI : MonoBehaviour
{
    [SerializeField] private EquipmentSO recipe;
    [SerializeField] private CraftingSystem craftingSystem;
    [SerializeField] private Image background;

    private static RecipeButtonUI currentSelected;
    public void OnClick()
    {
        Debug.Log($"Recipe Button Clicked: {recipe._itemName}");

        if (currentSelected != null)
            currentSelected.SetTransparency(0.1f);

        currentSelected = this;
        SetTransparency(1f);

        craftingSystem.SelectRecipe(recipe);
    }

    private void SetTransparency(float alpha)
    {
        Color color = background.color;
        color.a = alpha;
        background.color = color;
    }

    public static void ClearSelection()
    {
        if (currentSelected != null)
        {
            currentSelected.SetTransparency(0.1f);
            currentSelected = null;
        }
    }
}
