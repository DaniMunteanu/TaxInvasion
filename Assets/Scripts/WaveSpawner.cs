using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject laneMarkers;

    [SerializeField]
    TreasurePile treasureTarget;

    [SerializeField]
    EconomySystem economySystem;

    private int NUMBER_OF_LANES = 17;
    public int MAX_ROUNDS = 5;
    public int roundCount = 0;
    public UnityEvent newRoundStarted;
    public UnityEvent roundOver;
    private int[] agentsSpawned;
    private Vector3[] lanePositions;
    private Round currentRound;
    private int currentRoundTotalEnemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lanePositions = new Vector3[NUMBER_OF_LANES];

        for(int index = 0; index < NUMBER_OF_LANES; index++)
            lanePositions[index] = laneMarkers.transform.GetChild(index).position;
    }

    public void StartNewRound()
    {
        roundCount++;
        newRoundStarted.Invoke();

        currentRound = Resources.Load<Round>("Round" + roundCount + "/Round" + roundCount);
        CountCurrentRoundEnemies();

        agentsSpawned = new int[NUMBER_OF_LANES];
        
        SpawnAgents();
    }

    void CountCurrentRoundEnemies()
    {
        currentRoundTotalEnemies = 0;
        foreach(var lane in currentRound.lanes)
        {
            currentRoundTotalEnemies += lane.agentsList.Count;
        }
    }

    void SpawnAgents()
    {
        bool keepSpawning = false;
        foreach(var lane in currentRound.lanes)
            keepSpawning = keepSpawning || TryToSpawn(lane);
        
        if(keepSpawning)
            Invoke("SpawnAgents",2);
    }

    bool TryToSpawn(LaneWave lane)
    {
        if(agentsSpawned[lane.laneIndex] < lane.maxSpawnedAgents)
        {
            Agent newAgent = Instantiate(lane.agents.Dequeue());
            newAgent.transform.position = lanePositions[lane.laneIndex];
            newAgent.treasurePile = treasureTarget;
            newAgent.lane = lane;

            newAgent.agentDead.AddListener(OnAgentDead);

            economySystem.RegisterCharacterDeath(newAgent);

            agentsSpawned[lane.laneIndex]++;
            return true;
        }
        return false;
    }

    void OnAgentDead(LaneWave lane)
    {
        agentsSpawned[lane.laneIndex]--;
        currentRoundTotalEnemies--;

        if (currentRoundTotalEnemies == 0)
        {
            roundOver.Invoke();
            return;
        }

        if (lane.agents.Count > 0)
            TryToSpawn(lane);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
