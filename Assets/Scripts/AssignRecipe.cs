using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssignRecipe : MonoBehaviour
{
    public Image recipeImage;
    public TextMeshProUGUI recipeDescription;
    public TextMeshProUGUI recipeName;

    public SCO_Recipe recipe;

    private void Awake()
    {
        recipeDescription.text = recipe.description;
        recipeName.text = recipe.recipeName;
    }
}
