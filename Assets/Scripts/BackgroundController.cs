using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length, steps;
    [Range(0,1)]
    public float scrollSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        steps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(transform.position.x > length)
        {
            transform.position = new Vector3(-2 * length, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + scrollSpeed, transform.position.y, transform.position.z);
        }   
    }
}
