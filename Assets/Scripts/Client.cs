using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Client : MonoBehaviour
{
    public SCO_Client clientParameters;

    private string clientName;

    [TextArea] public List<Dialog> initialTxt;
    [TextArea] public List<Dialog> exitTxtOK;
    [TextArea] public List<Dialog> exitTxtNOK;

    Sprite spriteOK, spriteNOK;
    public Vector2 reputationMinMax;

    public List<SCO_Recipe> recipes;

    public Vector2 positionStart;
    public Vector2 positionIn;
    public Vector2 positionEnd;

    public void Start()
    {
        clientName = clientParameters.clientName;

        initialTxt = clientParameters.initialTxt;
        exitTxtOK = clientParameters.exitTxtOK;
        exitTxtNOK = clientParameters.exitTxtNOK;

        spriteOK = clientParameters.spriteOK;
        spriteNOK = clientParameters.spriteNOK;

        reputationMinMax = clientParameters.reputationMinMax;

        recipes = clientParameters.recipes;

        transform.position = positionStart;
    }

    public void Enter(float moveTime)
    {
        transform.DOMove(positionIn, moveTime);
    }

    // Appelé par le bouton Bake!
    public void Exit(float moveTime)
    {
        transform.DOMove(positionEnd, moveTime);        
    }

}
