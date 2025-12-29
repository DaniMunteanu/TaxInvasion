using UnityEngine;
using UnityEngine.AI;

public class KatanaAgent : Agent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();

        currentTarget = treasurePile.transform;
        SetOrientationForCurrentTarget();

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

    void SetOrientationForCurrentTarget()
    {
        animator.SetBool("isAttacking", false);

        Vector2 direction = transform.position - currentTarget.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180.0f;

        print((int) (angle/60.0));

        switch ( (int) (angle/60.0))
        {
            case 0:
                animator.SetTrigger("walkUpRight");
                break;
            case 1:
                animator.SetTrigger("walkUp");
                break;
            case 2:
                animator.SetTrigger("walkUpLeft");
                break;
            case 3:
                animator.SetTrigger("walkDownLeft");
                break;
            case 4:
                animator.SetTrigger("walkDown");
                break;
            case 5:
                animator.SetTrigger("walkDownRight");
                break;            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentPirateTarget == null && collision.gameObject.tag == "Pirate")
        {   
            Debug.Log("Pirate found!");
            currentPirateTarget = collision.gameObject.transform;
            currentTarget = currentPirateTarget;
            SetOrientationForCurrentTarget();
            agent.stoppingDistance = 1;
        }
        else
        {
            if(collision.gameObject.tag == "Treasure")
            {
                Debug.Log("Treasure found!");
                agent.isStopped = true;
                animator.SetTrigger("stealTreasure");
            }
        }
    }

    void StealTreasure()
    {
        treasurePile.mainUI.treasureHealth.TakeDamage(treasureDamage);
        Destroy(this.gameObject);
    }
}
