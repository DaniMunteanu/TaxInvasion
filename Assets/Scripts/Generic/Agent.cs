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

        agent = gameObject.GetComponent<NavMeshAgent>();

        currentTarget = treasurePile.transform;
        agent.SetDestination(currentTarget.position);

        SetOrientationForCurrentTarget();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }

    void SetOrientationForCurrentTarget()
    {
        animator.SetBool("isAttacking", false);

        Vector2 direction = transform.position - currentTarget.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180.0f;

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

    void OnEnemyPirateDead(int creditsEarnedOnDeath)
    {
        currentPirateTarget = null;
        currentTarget = treasurePile.transform;
        agent.SetDestination(currentTarget.position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentPirateTarget == null && collision.gameObject.tag == "Pirate")
        {   
            currentPirateTarget = collision.gameObject.transform;
            currentPirateTarget.gameObject.GetComponent<Character>().characterDead.AddListener(OnEnemyPirateDead);
            currentTarget = currentPirateTarget;
            agent.SetDestination(currentTarget.position);
            SetOrientationForCurrentTarget();
            agent.stoppingDistance = 1;
        }
        else
        {
            if(collision.gameObject.tag == "Treasure")
            {
                agent.isStopped = true;
                animator.SetTrigger("stealTreasure");
            }
        }
    }

    void StealTreasure()
    {
        treasurePile.treasureHealth.TakeDamage(treasureDamage);
        Destroy(this.gameObject);
    }
}
