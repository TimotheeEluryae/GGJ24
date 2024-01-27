using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.U2D;

public class Client : MonoBehaviour
{
    public SCO_Client clientParameters;

    [System.NonSerialized] public string clientName;

    [TextArea] List<Dialog> initialTxt;
    [TextArea] List<Dialog> exitTxtOK;
    [TextArea] List<Dialog> exitTxtNOK;

    Sprite spriteOK, spriteNOK;

    [System.NonSerialized] public Vector2 reputationMinMax;

    [System.NonSerialized] public List<SCO_Recipe> recipes;

    [SerializeField] Vector2 positionStart;
    [SerializeField] Vector2 positionIn;
    [SerializeField] Vector2 positionEnd;

    public void Awake()
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

    public void Exit(float moveTime)
    {
        transform.DOMove(positionEnd, moveTime);
        
        // WARNING ! Reinitialize the GameObject to it's first position.
    }

    public bool CanEnterTheShop()
    {
        return PlayerReputation.Instance.reputation >= reputationMinMax.x && PlayerReputation.Instance.reputation <= reputationMinMax.y;
    }

    private bool IsRecipeValid(SCO_Recipe recipe, int egg, int flour, int butter, int sugaryThing, int sugar, int yeast)
    {
        return egg >= recipe.egg.x
            && egg <= recipe.egg.y
            && flour <= recipe.flour.x
            && flour <= recipe.flour.y
            && butter <= recipe.butter.x
            && butter <= recipe.butter.y
            && sugaryThing <= recipe.sugaryThing.x
            && sugaryThing <= recipe.sugaryThing.y
            && sugar <= recipe.sugar.x
            && sugar <= recipe.sugar.y
            && yeast <= recipe.yeast.x
            && yeast <= recipe.yeast.y;
    }

    public bool IsClientHappy(int egg, int flour, int butter, int sugaryThing, int sugar, int yeast)
    {
        bool isHappy = true;

        for (int i = 0; i < recipes.Count; i++)
        {
            isHappy = isHappy && IsRecipeValid(recipes[i], egg, flour, butter, sugaryThing, sugar, yeast);
        }
        return isHappy;
    }

    public void ExitHappy(float moveTime)
    {
        //transform.GetComponent<SpriteRenderer>().sprite = spriteOK;
        Exit(moveTime);
    }

    public void ExitSad(float moveTime)
    {
        Exit(moveTime);
        //transform.GetComponent<SpriteRenderer>().sprite = spriteNOK;
    }
}
