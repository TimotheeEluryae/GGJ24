using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Client : MonoBehaviour
{
    public SCO_Client clientParameters;

    private string name;

    [TextArea] public List<string> initialTxt;
    [TextArea] public List<string> exitTxtOK;
    [TextArea] public List<string> exitTxtNOK;

    Sprite spritOK, spriteNOK;
    Vector2 reputationMinMax;

    List<SCO_Recipe> recipes;

    public Vector3 positionStart;
    public Vector3 positionIn;
    public Vector3 positionEnd;

    public void Start()
    {
        name = clientParameters.name;

        initialTxt = clientParameters.initialTxt;
        exitTxtOK = clientParameters.exitTxtOK;
        exitTxtNOK = clientParameters.exitTxtNOK;

        spriteOK = clientParameters.spriteOK;
        spriteNOK = clientParameters.spriteNOK;

        reputationMinMax = clientParameters.reputationMinMax;

        recipes = clientParameters.recipes;

        transform.position = positionStart;
    }

    public void Enter()
    {
        transform.DOMove(positionIn, 2);
    }

    public void Exit()
    {
        transform.DOMove(positionEnd, 2);
    }

}
