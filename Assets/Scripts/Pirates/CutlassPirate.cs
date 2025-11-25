using System;
using UnityEngine;

public class CutlassPirate : MonoBehaviour
{
    GameObject targetedEnemy;
    
    [SerializeField]
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetBool("isAttacking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetedEnemy != null)
        {
            Vector2 direction = transform.position - targetedEnemy.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if(angle < 0)
                angle += 180;

            String previousDirection = "";
            switch ( (int) angle/60.0)
            {
                case 0:
                    animator.SetBool("attackUpRight", true);
                    //animator.SetBool(previousDirection, false);
                    previousDirection = "attackUpRight";
                    
                    break;
                case 1:
                    animator.SetBool("attackUpCenter", true);
                    //animator.SetBool(previousDirection, false);
                    previousDirection = "attackUpCenter";
                    break;
                case 2:
                    animator.SetBool("attackUpLeft", true);
                    //animator.SetBool(previousDirection, false);
                    previousDirection = "attackUpLeft";
                    break;
                case 3:
                    animator.SetBool("attackDownLeft", true);
                    //animator.SetBool(previousDirection, false);
                    previousDirection = "attackDownLeft";
                    break;
                case 4:
                    animator.SetBool("attackDownCenter", true);
                    //animator.SetBool(previousDirection, false);
                    previousDirection = "attackDownCenter";
                    break;
                case 5:
                    animator.SetBool("attackDownRight", true);
                    //animator.SetBool(previousDirection, false);
                    previousDirection = "attackDownRight";
                    break;            
            }

            Debug.Log("Animation should be playing");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetedEnemy == null && collision.gameObject.tag == "Agent")
        {
            targetedEnemy = collision.gameObject;
            animator.SetBool("isAttacking", true);
            Debug.Log("Pirate found enemy!");
        }
    }
}
