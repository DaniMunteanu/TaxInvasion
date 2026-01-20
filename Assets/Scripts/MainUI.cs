using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    CharacterStatsSO characterStats;
    [SerializeField]
    EconomySystem economySystem;
    [SerializeField]
    WaveSpawner waveSpawner;

    public HealthBar treasureHealthbar;
    public Health treasureHealth;
    public TMP_Text treasureHPText;
    public TMP_Text creditsSpentText;
    public TMP_Text creditsTotalText;
    public TMP_Text roundText;

    [SerializeField]
    Button settingsButton;
    [SerializeField]
    Button newRoundButton;
    [SerializeField]
    Button normalSpeedButton;
    [SerializeField]
    Button doubleSpeedButton;

    [SerializeField]
    PauseScreen settingsScreen;
    [SerializeField]
    PauseScreen victoryScreen;
    [SerializeField]
    PauseScreen gameOverScreen;
    private float previousTimeScale = 1f;
    private bool paused = false;
    private bool pausable = false;

    private Dictionary<Vector3Int,PirateProfile> pirateProfiles = new Dictionary<Vector3Int, PirateProfile>();
    private PirateProfile lastShownPirateProfile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePirateProfilesData();
        OnTreasureHealthChanged();
        UpdateTotalCreditsText();
        UpdateRoundText();
        treasureHealth.healthChanged.AddListener(OnTreasureHealthChanged);
        economySystem.creditsModified.AddListener(UpdateTotalCreditsText);
        waveSpawner.newRoundStarted.AddListener(UpdateRoundText);

        newRoundButton.onClick.AddListener(OnNewRoundButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        normalSpeedButton.onClick.AddListener(OnNormalSpeedButtonPressed);
        doubleSpeedButton.onClick.AddListener(OnDoubleSpeedButtonPressed);
        waveSpawner.roundOver.AddListener(OnRoundOver);

        pausable = true;
    }

    void OnTreasureHealthChanged()
    {
        treasureHPText.text = treasureHealth.currentHitpoints + "/" + treasureHealth.maxHitpoints;
    }

    void UpdateTotalCreditsText()
    {
        creditsTotalText.text = economySystem.currentCredits.ToString();
    }

    void UpdateRoundText()
    {
        roundText.text = waveSpawner.roundCount + "/" + waveSpawner.MAX_ROUNDS;
    }

    void OnSettingsButtonPressed()
    {
        settingsScreen.gameObject.SetActive(!settingsScreen.gameObject.activeSelf);

        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        paused = !paused;
    }

    void OnNewRoundButtonPressed()
    {
        waveSpawner.StartNewRound();
        newRoundButton.gameObject.SetActive(false);
        doubleSpeedButton.gameObject.SetActive(false);
        normalSpeedButton.gameObject.SetActive(true);
    }

    void OnNormalSpeedButtonPressed()
    {
        Time.timeScale = 2f;
        newRoundButton.gameObject.SetActive(false);
        normalSpeedButton.gameObject.SetActive(false);
        doubleSpeedButton.gameObject.SetActive(true);
    }
    void OnDoubleSpeedButtonPressed()
    {
        Time.timeScale = 1f;
        newRoundButton.gameObject.SetActive(false);
        doubleSpeedButton.gameObject.SetActive(false);
        normalSpeedButton.gameObject.SetActive(true);
    }

    void OnRoundOver()
    {
        Time.timeScale = 1f;
        doubleSpeedButton.gameObject.SetActive(false);
        normalSpeedButton.gameObject.SetActive(false);
        newRoundButton.gameObject.SetActive(true);
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

    public void RemovePirateProfile(Vector3Int pirateGridPosition)
    {
        TooltipSystem.Hide();
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
        if (Keyboard.current.escapeKey.wasPressedThisFrame && pausable)
        {
            settingsScreen.gameObject.SetActive(!settingsScreen.gameObject.activeSelf);

            if (paused)
            {
                Time.timeScale = previousTimeScale;
            }
            else
            {
                previousTimeScale = Time.timeScale;
                Time.timeScale = 0f;
            }

            paused = !paused;
        }
    }
}
