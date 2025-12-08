using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        animator.SetBool("isAttacking", false);
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public void startAttackingEnemy(Character enemy)
    {
        animator.SetBool("isAttacking", true);

            Vector2 direction = transform.position - enemy.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180.0f;

            print((int) (angle/60.0));

            switch ( (int) (angle/60.0))
            {
                case 0:
                    animator.SetTrigger("attackUpRight");
                    break;
                case 1:
                    animator.SetTrigger("attackUpCenter");
                    break;
                case 2:
                    animator.SetTrigger("attackUpLeft");
                    break;
                case 3:
                    animator.SetTrigger("attackDownLeft");
                    break;
                case 4:
                    animator.SetTrigger("attackDownCenter");
                    break;
                case 5:
                    animator.SetTrigger("attackDownRight");
                    break;            
            }

            Debug.Log("Pirate found enemy!");
    }
}
