using UnityEngine;

public class CutlassSpinAttack : MonoBehaviour
{
    [SerializeField]
    CutlassPirate cutlassPirateParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckIfEnemy(Collider2D collision)
    {
        if ((collision.gameObject.layer == 8) && (collision.gameObject.tag == "Agent"))
        {
            Character targetedEnemy = collision.gameObject.transform.parent.gameObject.GetComponent<Character>();
            cutlassPirateParent.DealDamageToEnemy(targetedEnemy);
            SoundManager.PlaySound(SoundType.CUTLASS_SPIN);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        CheckIfEnemy(collision);
    }
}
