using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Agent : Character
{
    public LaneWave lane;
    public UnityEvent<LaneWave> agentDead;
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
        animator.SetBool("isAttacking", false);
        health.healthDepleted.AddListener(OnHealthDepleted);

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
        SetOrientationForCurrentTarget();

        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    void CheckIfEnemy(Collider2D collision)
    {
        if(currentPirateTarget == null && collision.gameObject.tag == "Pirate")
        {   
            currentPirateTarget = collision.gameObject.transform;
            currentPirateTarget.gameObject.GetComponent<Character>().characterDead.AddListener(OnEnemyPirateDead);
            currentTarget = currentPirateTarget;
            agent.SetDestination(currentTarget.position);
            agent.isStopped = false;
            SetOrientationForCurrentTarget();
            agent.stoppingDistance = 0.75f;
        }
        else
        {
            if(collision.gameObject.tag == "Treasure" && currentTarget == treasurePile.transform)
            {
                agent.isStopped = true;
                animator.SetTrigger("stealTreasure");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        CheckIfEnemy(collision);
    }

    /*
    void OTriggerStay2D(Collider2D collision)
    {
        CheckIfEnemy(collision);
    }
    */

    void StealTreasure()
    {
        treasurePile.treasureHealth.TakeDamage(treasureDamage);
        agentDead.Invoke(lane);
        SoundManager.PlaySound(SoundType.AGENT_TELEPORT);
        Destroy(this.gameObject);
    }

    new public void OnHealthDepleted()
    {
        characterDead.Invoke(creditsDroppedOnDeath);
        agentDead.Invoke(lane);
        SoundManager.PlaySound(SoundType.CHARACTER_DEAD);
        Destroy(this.gameObject);
    }
}
