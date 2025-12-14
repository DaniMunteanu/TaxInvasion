using UnityEngine;

public class TreasurePile : MonoBehaviour
{
    float currentTreasureHitpoints;
    public MainUI mainUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTreasureHitpoints = mainUI.treasureHealth.currentHitpoints ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
