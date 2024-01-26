using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    GameObject clientPrefab;

    public static ClientManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void CreateClient(SCO_Client clientParameters)
    {
        GameObject currentClient = Instantiate(clientPrefab);
        
    }

    public void EnterClient(GameObject currentClient)
    {

    }

    public void ExitClient(GameObject currentClient)
    {

    }
}