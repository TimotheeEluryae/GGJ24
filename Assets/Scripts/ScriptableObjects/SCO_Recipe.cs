using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObject/Recipe")]
public class SCO_Recipe : ScriptableObject
{
    public string recipeName;
    [TextArea]public string description;

    public Vector2 egg, flour, butter, sugaryThing, sugar, yeast;
}