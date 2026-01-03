using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject laneMarkers;

    [SerializeField]
    TreasurePile treasureTarget;

    [SerializeField]
    EconomySystem economySystem;

    private int NUMBER_OF_LANES = 17;
    private int MAX_ROUNDS = 1;
    private int roundCount = 1;
    private int[] agentsSpawned;
    private Vector3[] lanePositions;
    private Round currentRound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lanePositions = new Vector3[NUMBER_OF_LANES];

        for(int index = 0; index < NUMBER_OF_LANES; index++)
            lanePositions[index] = laneMarkers.transform.GetChild(index).position;
    }

    public void StartNewRound()
    {
        currentRound = Resources.Load<Round>("Round" + roundCount);

        agentsSpawned = new int[NUMBER_OF_LANES];
        
        SpawnAgents();
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
            newAgent.laneIndex = lane.laneIndex;

            economySystem.RegisterCharacterDeath(newAgent);

            agentsSpawned[lane.laneIndex]++;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
