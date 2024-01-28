using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour
{
    public List<GameObject> everyClientPrefabs = new List<GameObject>();
    List<GameObject> clientsInGame = new List<GameObject>();
    GameObject currentClient;

    public AudioClip buttonSound;

    public TMP_Text ingredientCountTxt;

    public Transform clientContent;

    bool isSpeaking = false;
    bool canBake = false;

    int eggCount = 0, flourCount = 0, butterCount = 0, sugaryThingCount = 0, sugarCount = 0, yeastCount = 0;
    int maxIngredients = 10;
    int totalIngredients = 0;

    ClientState clientState;
    bool isHappy;

    public static LoopManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < everyClientPrefabs.Count; i++)
        {
            if (everyClientPrefabs[i] != null)
            {
                GameObject tmp = Instantiate(everyClientPrefabs[i], clientContent);
                clientsInGame.Add(tmp);
            }
        }

        StartCoroutine(StartClientInteraction());
    }

    IEnumerator StartClientInteraction()
    {
        currentClient = SelectClient();
        if (currentClient != null)
        {
            currentClient.SetActive(true);

            canBake = false;

            Client clientSC = currentClient.GetComponent<Client>();
            clientSC.Enter();
            clientState = ClientState.Enter;

            if (clientSC.staticPNJ)
            {
                yield return new WaitForSeconds(4);

                if (clientSC.staticSoundPNJ != null) AudioManager2.instance.PlayClipAt(clientSC.staticSoundPNJ);
                clientSC.Exit();
                StartCoroutine(WaitForNewClient(.8f));
            }
            else
            {
                yield return new WaitForSeconds(2.5f);

                DialogSystem.Instance.StartDialog("", clientSC.initialTxt);
                clientState = ClientState.FirstSpeak;
                isSpeaking = true;

                totalIngredients = 0;
                ingredientCountTxt.text = totalIngredients + "/10";
            }
        }
        else
        {
            print("You lost");
        }
    }

    public GameObject SelectClient()
    {
        if(PlayerReputation.Instance.reputation <= 0)
        {
            print("Reputation less to 0");
            return null;
        }


        List<GameObject> clientSelected = new List<GameObject>();

        for (int i = 0; i < clientsInGame.Count; i++)
        {
            clientsInGame[i].SetActive(false);
            Client tmp = clientsInGame[i].GetComponent<Client>();

            if (tmp.CanEnterTheShop() && !tmp.wasAlreadySelected) clientSelected.Add(clientsInGame[i]);
        }

        if(clientSelected.Count <= 0 )
        {
            print("reset list");

            for (int i = 0; i < clientsInGame.Count; i++)
            {
                clientsInGame[i].GetComponent<Client>().wasAlreadySelected = false;
            }

            return SelectClient();
        }

        GameObject clientSelect = clientSelected[Random.Range(0, clientSelected.Count)];
        clientSelect.GetComponent<Client>().wasAlreadySelected = true;

        print(clientSelect.name);
        return clientSelect;
    }

    public void AddEgg()=> AddIngredient(Ingredient.Egg);
    public void AddFlour()=> AddIngredient(Ingredient.Flour);
    public void AddButter()=> AddIngredient(Ingredient.Butter);
    public void AddSugaryThing()=> AddIngredient(Ingredient.SugaryThing);
    public void AddSugar()=> AddIngredient(Ingredient.Sugar);
    public void AddYeast()=> AddIngredient(Ingredient.Yeast);
    public void AddIngredient(Ingredient ingredient)
    {
        if (isSpeaking) return;

        AudioManager2.instance.PlayClipAt(buttonSound);

        Debug.Log("trying to add ingredients");
        if (totalIngredients < maxIngredients)
        {
            switch (ingredient)
            {
                case Ingredient.Egg: eggCount += 1; break;
                case Ingredient.Flour: flourCount += 1; break;
                case Ingredient.Butter: butterCount += 1; break;
                case Ingredient.SugaryThing: sugaryThingCount += 1; break;
                case Ingredient.Sugar: sugarCount += 1; break;
                case Ingredient.Yeast: yeastCount += 1; break;
            }
            totalIngredients += 1;
            ingredientCountTxt.text = totalIngredients + "/10";
            Debug.Log("Ingredient added");
        }
    }

    public void CraftRecipe()
    {
        if(isSpeaking || !canBake)
            return;

        AudioManager2.instance.PlayClipAt(buttonSound);

        canBake = false;

        Debug.Log("Baking recipe");
        Client clientSC = currentClient.GetComponent<Client>();

        clientState = ClientState.LastSpeak;

        if (currentClient.GetComponent<Client>().IsClientHappy(eggCount, flourCount, butterCount, sugaryThingCount, sugarCount, yeastCount))
        {
            if (clientSC.spriteOK != null) clientSC.graphics.sprite = clientSC.spriteOK;
            if (clientSC.soundOK != null) AudioManager2.instance.PlayClipAt(clientSC.soundOK);
            DialogSystem.Instance.StartDialog(clientSC.clientName, clientSC.exitTxtOK);
            isHappy = true;
            isSpeaking = true;
        }
        else
        {
            if (clientSC.spriteNOK != null) clientSC.graphics.sprite = clientSC.spriteNOK;
            if (clientSC.soundNOK != null) AudioManager2.instance.PlayClipAt(clientSC.soundNOK);
            DialogSystem.Instance.StartDialog(clientSC.clientName, clientSC.exitTxtNOK);
            isHappy = false;
            isSpeaking = true;
        }
    }

    IEnumerator WaitForNewClient(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(StartClientInteraction());
    }

    public bool IsValidIngredient(Vector2 recipeIngredient,int currentIngredient)
    {
        if (recipeIngredient.x <= currentIngredient && recipeIngredient.y >= currentIngredient)
        {
            return true;
        }
        return false;
    }

    public void EndSpeak()
    {
        switch (clientState)
        {
            case ClientState.FirstSpeak:
                clientState = ClientState.Waiting;
                eggCount = 0;
                flourCount = 0;
                butterCount = 0;
                sugaryThingCount = 0;
                sugarCount = 0;
                yeastCount = 0;
                canBake = true;
                break;

            case ClientState.LastSpeak:
                clientState = ClientState.Leaving;

                Client clientSC = currentClient.GetComponent<Client>();

                if (isHappy)
                {
                    PlayerReputation.Instance.AddReputation(clientSC.reputationToGive);
                    
                    clientSC.Exit();
                }
                else
                {
                    PlayerReputation.Instance.RemoveReputation(clientSC.reputationToGive);
                    clientSC.Exit();
                }

                StartCoroutine(WaitForNewClient(0.8f));
                break;

        }

        isSpeaking = false;
    }
}

public enum Ingredient
{
    Egg,
    Flour,
    Butter,
    SugaryThing,
    Sugar,
    Yeast
}