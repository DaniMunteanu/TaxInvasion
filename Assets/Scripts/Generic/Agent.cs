using UnityEngine;
using UnityEngine.AI;

public class Agent : Character
{
    public int laneIndex;
    public TreasurePile treasurePile;
    protected Transform currentPirateTarget = null;
    protected Transform currentTarget;
    
    protected NavMeshAgent agent;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float treasureDamage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }
}
