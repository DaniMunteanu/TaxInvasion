using UnityEngine;

public class CombatInitiation : MonoBehaviour
{
    Character targetedEnemy = null;
    Character characterParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterParent = transform.parent.gameObject.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetedEnemy == null && ((collision.gameObject.tag == "Agent" && tag == "Pirate" ) || (collision.gameObject.tag == "Pirate" && tag == "Agent" )))
        {
            targetedEnemy = collision.gameObject.GetComponent<Character>();
            characterParent.startAttackingEnemy(targetedEnemy);
        }
    }
}
