using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    [SerializeField]
    protected CharacterStatsSO characterStats;
    public Health health;
    [SerializeField]
    protected float damage;
    protected float damageReduction = 0;
    protected Character currentEnemy;
    public UnityEvent<int> characterDead;
    public int creditsDroppedOnDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        animator.SetBool("isAttacking", false);
        health.healthDepleted.AddListener(OnHealthDepleted);
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public void StartAttackingEnemy(Character enemy)
    {
        animator.SetBool("isAttacking", true);
            
        currentEnemy = enemy;
        currentEnemy.characterDead.AddListener(OnEnemyDead);

        Vector2 direction = transform.position - currentEnemy.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180.0f;

        switch ( (int) (angle/60.0))
        {
            case 0:
                animator.SetTrigger("attackUpRight");
                break;
            case 1:
                animator.SetTrigger("attackUp");
                break;
            case 2:
                animator.SetTrigger("attackUpLeft");
                break;
            case 3:
                animator.SetTrigger("attackDownLeft");
                break;
            case 4:
                animator.SetTrigger("attackDown");
                break;
            case 5:
                animator.SetTrigger("attackDownRight");
                break;            
        }
    }

    public void CharacterTakeDamage(float damage)
    {
        health.TakeDamage( (1 - damageReduction/100) * damage);
        if (animator != null)
            animator.SetTrigger("isHurt");
    }

    public void DealDamageToEnemy(Character targetedEnemy)
    {
        if (targetedEnemy == null)
            targetedEnemy = currentEnemy;
        targetedEnemy.CharacterTakeDamage(damage);
    }

    public void OnHealthDepleted()
    {
        characterDead.Invoke(creditsDroppedOnDeath);
        Destroy(this.gameObject);
    }

    public void OnEnemyDead(int creditsEarnedOnDeath)
    {
        animator.SetBool("isAttacking", false);
    }
}
