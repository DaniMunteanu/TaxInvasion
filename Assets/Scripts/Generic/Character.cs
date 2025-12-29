using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    [SerializeField]
    Health health;
    [SerializeField]
    float damage;
    Character currentEnemy;
    public UnityEvent characterDead;
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

        print((int) (angle/60.0));

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

        Debug.Log("Pirate found enemy!");
    }

    public void CharacterTakeDamage(float damage)
    {
        health.TakeDamage(damage);
        if (animator != null)
            animator.SetTrigger("isHurt");
    }

    public void DealDamageToEnemy()
    {
        currentEnemy.CharacterTakeDamage(damage);
    }

    public void OnHealthDepleted()
    {
        characterDead.Invoke();
        Destroy(this.gameObject);
    }

    public void OnEnemyDead()
    {
        Debug.Log("EnemyDead");
        animator.SetBool("isAttacking", false);
    }
}
