using UnityEngine;
using UnityEngine.AI;

public class KatanaAgent : Agent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();

        currentTarget = treasureTarget;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        
        agent.SetDestination(currentTarget.position);

        /*
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.5)
            agent.speed = 0;
        else
            agent.speed = moveSpeed;
        */
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentPirateTarget == null && collision.gameObject.tag == "Pirate")
        {   
            Debug.Log("Pirate found!");
            currentPirateTarget = collision.gameObject.transform;
            currentTarget = currentPirateTarget;
        }
        else
        {
            if(collision.gameObject.tag == "Treasure")
            {
                Debug.Log("Treasure found!");
                agent.speed = 0;
                collision.gameObject.GetComponent<TreasurePile>().mainUI.treasureHealth.TakeDamage(treasureDamage);
                
                //Destroy(this.gameObject); //instead we play teleportation animation
            }
        }
    }
}
