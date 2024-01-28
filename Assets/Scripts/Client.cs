using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.U2D;
using System.Collections;

public class Client : MonoBehaviour
{
    public string clientName;
    public List<Dialog> initialTxt = new List<Dialog>();
    public List<Dialog> exitTxtOK = new List<Dialog>();
    public List<Dialog> exitTxtNOK = new List<Dialog>();

    Sprite initialSprite;
    public Sprite spriteOK, spriteNOK;

    public int reputationToGive;

    public bool staticPNJ;
    public AudioClip staticSoundPNJ;

    public Vector2 reputationMinMax;

    public AudioClip soundOK, soundNOK;
    public AudioClip[] walkSound;

    public List<SCO_Recipe> recipes;

    Animator animator;
    [HideInInspector] public Image graphics;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        graphics = GetComponent<Image>();

        initialSprite = graphics.sprite;
    }

    public void WalkSound()
    {
        if(walkSound.Length != 0) AudioManager.instance.PlayClipAt(walkSound[Random.Range(0, walkSound.Length)]);
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

    public void Enter()
    {
        graphics.sprite = initialSprite;
        animator.SetBool("IsEnter", true);
    }
    public void Exit()
    {
        animator.SetBool("IsEnter", false);
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