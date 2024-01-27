using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public List<GameObject> clientPrefabs = new List<GameObject>();
    List<GameObject> clients = new List<GameObject>();

    //public List<SCO_Recipe> recipes = new List<SCO_Recipe>();

    GameObject currentClient;

    int maxIngredients = 10;

    int eggCount = 0, flourCount = 0, butterCount = 0, sugaryThingCount = 0, sugarCount = 0, yeastCount = 0;

    int totalIngredients = 0;

    private void Start()
    {
        foreach(GameObject obj in clientPrefabs)
        {
            if (obj != null)
            {
                clients.Add(Instantiate(obj));
            }
        }

        currentClient = SelectClient();

        currentClient.GetComponent<Client>().Enter(2);
    }

    public GameObject SelectClient()
    {
        GameObject clientSelected = clients[Random.Range(0, clients.Count)];

        while (!clientSelected.GetComponent<Client>().CanEnterTheShop())
        {
            Debug.Log("Searching for a client...");
            clientSelected = clients[Random.Range(0, clients.Count)];
        }

        return clientSelected;
    }

    public void AddIngredient(int value)
    {
        Debug.Log("trying to add ingredients");
        if (totalIngredients <= maxIngredients)
        {
            switch (value)
            {
                case 1: eggCount += 1; break;
                case 2: flourCount += 1; break;
                case 3: butterCount += 1; break;
                case 4: sugaryThingCount += 1; break;
                case 5: sugarCount += 1; break;
                case 6: yeastCount += 1; break;
            }
            totalIngredients += 1;
            Debug.Log("Ingredient added");
        }
    }

    public void AddEgg()
    {
        Debug.Log("trying to add eggs");
        AddIngredient(1);
    }

    public void AddFlour()
    {
        AddIngredient(2);
    }

    public void AddButter()
    {
        AddIngredient(3);
    }

    public void AddSugaryThing()
    {
        AddIngredient(4);
    }

    public void AddSugar()
    {
        AddIngredient(5);
    }

    public void AddYeast()
    {
        AddIngredient(6);
    }



    // Simplified this logic by moving verifications to the Client class
    /*
    public void CraftRecipe()
    {
        for(int i = 0; i < recipes.Count; i++)
        {
            if (IsValidIngredient(recipes[i].ingredient1, ingredient1Count) 
                && IsValidIngredient(recipes[i].ingredient2, ingredient2Count)
                && IsValidIngredient(recipes[i].ingredient3, ingredient3Count)
                && IsValidIngredient(recipes[i].ingredient4, ingredient4Count)
                && IsValidIngredient(recipes[i].ingredient5, ingredient5Count)
                && IsValidIngredient(recipes[i].ingredient6, ingredient6Count))
            {
                print("Recipe Work");
                for(int n = 0; n < currentClient.GetComponent<Client>().recipes.Count; n++)
                {
                    if (currentClient.GetComponent<Client>().recipes[n] == recipes[i])
                    {
                        print("Client Work");
                    }
                    else
                    {
                        print("Client Fail");
                    }
                }
            }
            else
            {
                print("Recipe Fail");
            }
        }
    }*/

    public void CraftRecipe()
    {
        Debug.Log("Baking recipe");

        if (currentClient.GetComponent<Client>().IsClientHappy(eggCount, flourCount, butterCount, sugaryThingCount, sugarCount, yeastCount))
        {
            currentClient.GetComponent<Client>().ExitHappy(2);
        }
        else
        {
            currentClient.GetComponent<Client>().ExitSad(2);
        }
    }

    public bool IsValidIngredient(Vector2 recipeIngredient,int currentIngredient)
    {
        if (recipeIngredient.x <= currentIngredient && recipeIngredient.y >= currentIngredient)
        {
            return true;
        }
        return false;
    }
}