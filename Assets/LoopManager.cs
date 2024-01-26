using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public List<GameObject> clients = new List<GameObject>();
    public List<SCO_Recipe> recipes = new List<SCO_Recipe>();
    GameObject currentClient;

    int maxIngredients = 10;

    public int ingredient1Count = 0, ingredient2Count = 0, ingredient3Count = 0, ingredient4Count = 0, ingredient5Count = 0, ingredient6Count = 0;

    public int totalIngredients = 0;

    private void Awake()
    {
        SetClient();
    }

    public GameObject SelectClient()
    {
        GameObject clientSelected = clients[Random.Range(0, clients.Count)];

        while (!clientSelected.GetComponent<Client>().CanEnterTheShop())
        {
            clientSelected = clients[Random.Range(0, clients.Count)];
        }

        return clientSelected;
    }

    public void SetClient()
    {
        currentClient = SelectClient();
    }

    public void AddIngredient(int value)
    {
        if (totalIngredients < maxIngredients)
        {
            switch (value)
            {
                case 1: ingredient1Count += 1; break;
                case 2: ingredient2Count += 1; break;
                case 3: ingredient3Count += 1; break;
                case 4: ingredient4Count += 1; break;
                case 5: ingredient5Count += 1; break;
            }
            totalIngredients += 1;
        }
    }

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