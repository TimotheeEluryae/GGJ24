using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObject/Recipe")]
public class SCO_Client : ScriptableObject
{
    public string name;
    [TextArea]public List<string> initialTxt = new List<string>();
    [TextArea]public List<string> exitTxtOK = new List<string>();
    [TextArea]public List<string> exitTxtNOK = new List<string>();

    public Sprite spritOK, spriteNOK;
    public Vector2 reputationMinMax;

    public List<SCO_Recipe> recipes = new List<SCO_Recipe>();
}