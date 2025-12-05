using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    private Dictionary<Vector3Int,UpgradePage> upgradePages = new Dictionary<Vector3Int, UpgradePage>();
    private UpgradePage lastShownUpgradePage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeUpgradePagesData();
    }

    private void InitializeUpgradePagesData()
    {
        upgradePages.Add(new Vector3Int(8,-4,0),null);
        upgradePages.Add(new Vector3Int(7,-4,0),null);
        upgradePages.Add(new Vector3Int(6,-4,0),null);
        upgradePages.Add(new Vector3Int(5,-4,0),null);

        upgradePages.Add(new Vector3Int(7,-3,0),null);
        upgradePages.Add(new Vector3Int(6,-3,0),null);
        upgradePages.Add(new Vector3Int(5,-3,0),null);
        upgradePages.Add(new Vector3Int(4,-3,0),null);
        
        upgradePages.Add(new Vector3Int(8,-2,0),null);
        upgradePages.Add(new Vector3Int(7,-2,0),null);
        upgradePages.Add(new Vector3Int(6,-2,0),null);
        upgradePages.Add(new Vector3Int(5,-2,0),null);
        upgradePages.Add(new Vector3Int(4,-2,0),null);

        upgradePages.Add(new Vector3Int(5,-1,0),null);
        upgradePages.Add(new Vector3Int(4,-1,0),null);
        upgradePages.Add(new Vector3Int(3,-1,0),null);

        upgradePages.Add(new Vector3Int(5,0,0),null);
        upgradePages.Add(new Vector3Int(4,0,0),null);
        upgradePages.Add(new Vector3Int(3,0,0),null);

        upgradePages.Add(new Vector3Int(5,1,0),null);
        upgradePages.Add(new Vector3Int(4,1,0),null);
        upgradePages.Add(new Vector3Int(3,1,0),null);

        upgradePages.Add(new Vector3Int(8,2,0),null);
        upgradePages.Add(new Vector3Int(7,2,0),null);
        upgradePages.Add(new Vector3Int(6,2,0),null);
        upgradePages.Add(new Vector3Int(5,2,0),null);
        upgradePages.Add(new Vector3Int(4,2,0),null);

        upgradePages.Add(new Vector3Int(7,3,0),null);
        upgradePages.Add(new Vector3Int(6,3,0),null);
        upgradePages.Add(new Vector3Int(5,3,0),null);
        upgradePages.Add(new Vector3Int(4,3,0),null);

        upgradePages.Add(new Vector3Int(8,4,0),null);
        upgradePages.Add(new Vector3Int(7,4,0),null);
        upgradePages.Add(new Vector3Int(6,4,0),null);
        upgradePages.Add(new Vector3Int(5,4,0),null);

    }

    public void AddUpgradePage(Vector3Int pirateGridPosition, Pirate pirate)
    {
        upgradePages[pirateGridPosition] = Instantiate(pirate.upgradePagePrefab);
        upgradePages[pirateGridPosition].transform.SetParent(gameObject.transform, true);
        pirate.upgradePageInstance = upgradePages[pirateGridPosition];

        ShowUpgradePage(pirateGridPosition);
    }

    //micunealta pe care o vom folosi mai tarziu  >__<
    public void RemoveUpgradePage(Vector3Int pirateGridPosition)
    {
        Destroy(upgradePages[pirateGridPosition]);
        upgradePages[pirateGridPosition] = null;
    }

    public void ShowUpgradePage(Vector3Int pirateGridPosition)
    {
        if (lastShownUpgradePage != null)
            lastShownUpgradePage.gameObject.SetActive(false);
        
        upgradePages[pirateGridPosition].gameObject.SetActive(true);
        lastShownUpgradePage = upgradePages[pirateGridPosition];
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
