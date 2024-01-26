using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public List<SCO_Client> clients = new List<SCO_Client>();
    SCO_Client currentClient;

    public SCO_Client SelectClient()
    {
        List<SCO_Client> clientSelected = new List<SCO_Client>();

        for (int i = 0; i < clients.Count; i++)
        {
            if (PlayerReputation.Instance.HasValideReputation(clients[i].reputationMinMax)) clientSelected.Add(clients[i]);
        }

        return clientSelected[Random.Range(0, clientSelected.Count)];
    }
    //clientEnter();
    //gameplayphase()
    //{
    //    // le joueur doit faire une recette à partir des indications du client
    //    // le joueur fais ses mélanges, puis clic sur le bouton "Mélanger" pour valider la recette, et on passe à la suite
    //    // Verification si la recette du joueur correspond à la recette du client
    //}

    //exitphase()
    //{
    //    // sélection du sprite correspond à la réussite de la recette ou pas
    //}
}