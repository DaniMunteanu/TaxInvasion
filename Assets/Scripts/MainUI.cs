using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    CharacterStatsSO characterStats;
    [SerializeField]
    EconomySystem economySystem;

    public HealthBar treasureHealthbar;
    public Health treasureHealth;
    public TMP_Text treasureHPText;
    public TMP_Text creditsSpentText;
    public TMP_Text creditsTotalText;
    public TMP_Text roundText;

    private Dictionary<Vector3Int,PirateProfile> pirateProfiles = new Dictionary<Vector3Int, PirateProfile>();
    private PirateProfile lastShownPirateProfile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePirateProfilesData();
        OnTreasureHealthChanged();
        UpdateTotalCreditsText();
        treasureHealth.healthChanged.AddListener(OnTreasureHealthChanged);
        economySystem.creditsModified.AddListener(UpdateTotalCreditsText);
    }

    void OnTreasureHealthChanged()
    {
        treasureHPText.text = treasureHealth.currentHitpoints + "/" + treasureHealth.maxHitpoints;
    }

    void UpdateTotalCreditsText()
    {
        creditsTotalText.text = economySystem.currentCredits.ToString();
    }

    private void InitializePirateProfilesData()
    {
        pirateProfiles.Add(new Vector3Int(8,-4,0),null);
        pirateProfiles.Add(new Vector3Int(7,-4,0),null);
        pirateProfiles.Add(new Vector3Int(6,-4,0),null);
        pirateProfiles.Add(new Vector3Int(5,-4,0),null);

        pirateProfiles.Add(new Vector3Int(7,-3,0),null);
        pirateProfiles.Add(new Vector3Int(6,-3,0),null);
        pirateProfiles.Add(new Vector3Int(5,-3,0),null);
        pirateProfiles.Add(new Vector3Int(4,-3,0),null);
        
        pirateProfiles.Add(new Vector3Int(8,-2,0),null);
        pirateProfiles.Add(new Vector3Int(7,-2,0),null);
        pirateProfiles.Add(new Vector3Int(6,-2,0),null);
        pirateProfiles.Add(new Vector3Int(5,-2,0),null);
        pirateProfiles.Add(new Vector3Int(4,-2,0),null);

        pirateProfiles.Add(new Vector3Int(5,-1,0),null);
        pirateProfiles.Add(new Vector3Int(4,-1,0),null);
        pirateProfiles.Add(new Vector3Int(3,-1,0),null);

        pirateProfiles.Add(new Vector3Int(5,0,0),null);
        pirateProfiles.Add(new Vector3Int(4,0,0),null);
        pirateProfiles.Add(new Vector3Int(3,0,0),null);

        pirateProfiles.Add(new Vector3Int(5,1,0),null);
        pirateProfiles.Add(new Vector3Int(4,1,0),null);
        pirateProfiles.Add(new Vector3Int(3,1,0),null);

        pirateProfiles.Add(new Vector3Int(8,2,0),null);
        pirateProfiles.Add(new Vector3Int(7,2,0),null);
        pirateProfiles.Add(new Vector3Int(6,2,0),null);
        pirateProfiles.Add(new Vector3Int(5,2,0),null);
        pirateProfiles.Add(new Vector3Int(4,2,0),null);

        pirateProfiles.Add(new Vector3Int(7,3,0),null);
        pirateProfiles.Add(new Vector3Int(6,3,0),null);
        pirateProfiles.Add(new Vector3Int(5,3,0),null);
        pirateProfiles.Add(new Vector3Int(4,3,0),null);

        pirateProfiles.Add(new Vector3Int(8,4,0),null);
        pirateProfiles.Add(new Vector3Int(7,4,0),null);
        pirateProfiles.Add(new Vector3Int(6,4,0),null);
        pirateProfiles.Add(new Vector3Int(5,4,0),null);

    }

    public void AddPirateProfile(Vector3Int pirateGridPosition, Pirate pirate)
    {
        pirateProfiles[pirateGridPosition] = Instantiate(pirate.pirateProfilePrefab);
        pirateProfiles[pirateGridPosition].destroyProfilePage.AddListener(RemovePirateProfile);
        pirateProfiles[pirateGridPosition].upgradePage.economySystem = economySystem;
        pirateProfiles[pirateGridPosition].upgradePage.characterStats = characterStats;
        pirateProfiles[pirateGridPosition].transform.SetParent(gameObject.transform, true);
        pirate.pirateProfileInstance = pirateProfiles[pirateGridPosition];

        ShowPirateProfile(pirateGridPosition);
    }

    //micunealta pe care o vom folosi mai tarziu  >__<
    public void RemovePirateProfile(Vector3Int pirateGridPosition)
    {
        Destroy(pirateProfiles[pirateGridPosition].gameObject);
        pirateProfiles[pirateGridPosition] = null;
    }

    public void ShowPirateProfile(Vector3Int pirateGridPosition)
    {
        if (lastShownPirateProfile != null)
            lastShownPirateProfile.gameObject.SetActive(false);
        
        pirateProfiles[pirateGridPosition].gameObject.SetActive(true);
        lastShownPirateProfile = pirateProfiles[pirateGridPosition];
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
