using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReputation : MonoBehaviour
{
    public int reputation;

    public static PlayerReputation Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddReputation(int reputationToAdd)
    {
        reputation += reputationToAdd;
    }
    public void RemoveReputation(int reputationToRemove)
    {
        reputation -= reputationToRemove;
    }
    
    public bool HasValideReputation(Vector2 minMaxReputationRequire)
    {
        if(reputation >= minMaxReputationRequire.x && reputation <= minMaxReputationRequire.y) return true;
        else return false;
    }
}
