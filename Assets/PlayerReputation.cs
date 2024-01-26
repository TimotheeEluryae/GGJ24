using UnityEngine;

public class PlayerReputation : MonoBehaviour
{
    public int reputation = 5;

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
        if (reputation >= minMaxReputationRequire.x && reputation <= minMaxReputationRequire.y)
        {
            print("HasValidReputation");
            return true;
        }
        else return false;
    }
}
