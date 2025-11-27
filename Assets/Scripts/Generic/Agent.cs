using UnityEngine;

public class Agent : Character
{
    public int laneIndex;
    [SerializeField]
    public Transform goldTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
