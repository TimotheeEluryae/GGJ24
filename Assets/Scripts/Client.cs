using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Client : MonoBehaviour
{
    public string clientName;
    public List<Dialog> initialTxt = new List<Dialog>();
    public List<Dialog> exitTxtOK = new List<Dialog>();
    public List<Dialog> exitTxtNOK = new List<Dialog>();

    public Sprite spriteOK, spriteNOK;

    public Vector2 reputationMinMax;

    public List<SCO_Recipe> recipes;

    public Vector2 positionStart;
    public Vector2 positionIn;
    public Vector2 positionEnd;

    public SpriteRenderer graphics;

    private void Start()
    {
        transform.position = positionStart;
    }

    public void Enter(float moveTime)
    {
        transform.DOMove(positionIn, moveTime);
    }

    public void Exit(float moveTime)
    {
        transform.DOMove(positionEnd, moveTime).OnComplete(()=> { transform.position = positionStart; });
        
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
        //graphics.sprite = spriteOK;
        Exit(moveTime);
    }
    public void ExitSad(float moveTime)
    {
        Exit(moveTime);
        //graphics.sprite = spriteNOK;
    }
}
[System.Serializable]
public class Dialog
{
    [TextArea] public string txt;
}

public enum ClientState
{
    Enter,
    FirstSpeak,
    Waiting,
    LastSpeak,
    Leaving
}