using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LaneWave : ScriptableObject
{
    [Range(0,16)]
    public int laneIndex;
    public int maxSpawnedAgents;
    public List<Agent> agentsList;
    public Queue<Agent> agents;

    void OnEnable()
    {
        agents = new Queue<Agent>(agentsList);
    }
}
