using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Client", menuName = "ScriptableObject/Client")]
public class SCO_Client : ScriptableObject
{
    public string clientName;
    [TextArea]public List<Dialog> initialTxt = new List<Dialog>();
    [TextArea]public List<Dialog> exitTxtOK = new List<Dialog>();
    [TextArea]public List<Dialog> exitTxtNOK = new List<Dialog>();

    public Sprite spritOK, spriteNOK;
    public Vector2 reputationMinMax;

    public List<SCO_Recipe> recipes = new List<SCO_Recipe>();
}

[System.Serializable]
public class Dialog
{
    public string txt;
}