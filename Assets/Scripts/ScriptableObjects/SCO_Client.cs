using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Client", menuName = "ScriptableObject/Client")]
public class SCO_Client : ScriptableObject
{
    public string name;
    public List<Dialog> initialTxt = new List<Dialog>();
    public List<Dialog> exitTxtOK = new List<Dialog>();
    public List<Dialog> exitTxtNOK = new List<Dialog>();

    public Sprite spriteOK, spriteNOK;
    public Vector2 reputationMinMax;

    public List<SCO_Recipe> recipes = new List<SCO_Recipe>();
}

[System.Serializable]
public class Dialog
{
    [TextArea] public string txt;
}