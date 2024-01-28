using TMPro;
using UnityEngine;

public class AssignRecipe : MonoBehaviour
{
    public TextMeshProUGUI recipeDescription;
    public TextMeshProUGUI recipeName;

    public SCO_Recipe recipe;

    private void Awake()
    {
        recipeDescription.text = recipe.description;
        recipeName.text = recipe.recipeName;
    }
}
