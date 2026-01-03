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

    void OnCharacterDead(int creditsEarnedOnDeath)
    {
        targetedEnemy = null;
    }

    void CheckIfEnemy(Collider2D collision)
    {
        if ((collision.gameObject.layer == 8) && ((collision.gameObject.tag == "Agent" && tag == "Pirate" ) || (collision.gameObject.tag == "Pirate" && tag == "Agent" )))
            {
                targetedEnemy = collision.gameObject.transform.parent.gameObject.GetComponent<Character>();
                targetedEnemy.characterDead.AddListener(OnCharacterDead);
                characterParent.StartAttackingEnemy(targetedEnemy);
            }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetedEnemy == null)
        {
            CheckIfEnemy(collision);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (targetedEnemy == null)
        {
            CheckIfEnemy(collision);
        }
    }
}
