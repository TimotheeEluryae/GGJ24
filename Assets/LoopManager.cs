using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public List<SCO_Client> clients = new List<SCO_Client>();
    public List<SCO_Recipe> recipes = new List<SCO_Recipe>();
    SCO_Client currentClient;

    int maxIngredients = 10;

    public int ingredient1Count = 0, ingredient2Count = 0, ingredient3Count = 0, ingredient4Count = 0, ingredient5Count = 0, ingredient6Count = 0;

    public int totalIngredients = 0;

    private void Awake()
    {
        SetClient();
        print(currentClient.clientName);
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
                for(int n = 0; n < currentClient.recipes.Count; n++)
                {
                    if (currentClient.recipes[n] == recipes[i])
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
    //gameplayphase()
    //{
    //    // le joueur doit faire une recette à partir des indications du client
    //    // le joueur fais ses mélanges, puis clic sur le bouton "Mélanger" pour valider la recette, et on passe à la suite
    //    // Verification si la recette du joueur correspond à la recette du client
    //}
    public SCO_Client SelectClient()
    {
        List<SCO_Client> clientSelected = new List<SCO_Client>();

        for (int i = 0; i < clients.Count; i++)
        {
            if (PlayerReputation.Instance.HasValideReputation(clients[i].reputationMinMax))
            {
                clientSelected.Add(clients[i]);
            }
        }

        return clientSelected[Random.Range(0, clientSelected.Count)];
    }
}