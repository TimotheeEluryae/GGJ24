using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoopManager : MonoBehaviour
{
    public List<GameObject> everyClientPrefabs = new List<GameObject>();
    List<GameObject> clients = new List<GameObject>();
    GameObject currentClient;

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
                clients.Add(tmp);
            }
        }

        StartCoroutine(StartClientInteraction());
    }

    IEnumerator StartClientInteraction()
    {
        currentClient = SelectClient();
        currentClient.SetActive(true);
        print("Client selected");

        canBake = false;

        Client clientSC = currentClient.GetComponent<Client>();
        clientSC.Enter();
        clientState = ClientState.Enter;

        yield return new WaitForSeconds(2.5f);

        DialogSystem.Instance.StartDialog(clientSC.clientName, clientSC.initialTxt);
        clientState = ClientState.FirstSpeak;
        isSpeaking = true;

        totalIngredients = 0;
        ingredientCountTxt.text = totalIngredients + "/10";
    }

    public GameObject SelectClient()
    {
        List<GameObject> clientSelected = new List<GameObject>();

        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].SetActive(false);

            if (clients[i].GetComponent<Client>().CanEnterTheShop()) clientSelected.Add(clients[i]);
        }

        return clientSelected[Random.Range(0, clientSelected.Count)];
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

        canBake = false;

        Debug.Log("Baking recipe");
        Client clientSC = currentClient.GetComponent<Client>();

        clientState = ClientState.LastSpeak;

        if (currentClient.GetComponent<Client>().IsClientHappy(eggCount, flourCount, butterCount, sugaryThingCount, sugarCount, yeastCount))
        {
            DialogSystem.Instance.StartDialog(clientSC.clientName, clientSC.exitTxtOK);
            isHappy = true;
            isSpeaking = true;
        }
        else
        {
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
                canBake = true;
                break;
            case ClientState.LastSpeak:
                clientState = ClientState.Leaving;

                Client clientSC = currentClient.GetComponent<Client>();

                if (isHappy)
                {
                    PlayerReputation.Instance.AddReputation(clientSC.reputationToGive);
                    clientSC.ExitHappy();
                }
                else
                {
                    PlayerReputation.Instance.RemoveReputation(clientSC.reputationToGive);
                    clientSC.ExitSad();
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