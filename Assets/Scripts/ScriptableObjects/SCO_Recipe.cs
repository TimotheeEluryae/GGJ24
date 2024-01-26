using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObject/Recipe")]
public class SCO_Recipe : ScriptableObject
{
    public string recipeName;
    [TextArea]public string description;

    public Vector2 ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6;
}