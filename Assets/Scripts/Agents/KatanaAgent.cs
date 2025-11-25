using UnityEngine;
using UnityEngine.AI;

public class KatanaAgent : MonoBehaviour
{
    [SerializeField]
    Transform goldTarget;
    Transform currentPirateTarget = null;
    Transform currentTarget;
    
    NavMeshAgent agent;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTarget = goldTarget;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(currentTarget.position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentPirateTarget == null && collision.gameObject.tag == "Pirate")
        {   
            Debug.Log("Pirate found!");
            currentPirateTarget = collision.gameObject.transform;
            currentTarget = currentPirateTarget;
        }

    }
}
