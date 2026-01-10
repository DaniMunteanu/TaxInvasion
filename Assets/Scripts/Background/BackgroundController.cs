using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float repeatWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        repeatWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > repeatWidth)
        {
            transform.position = new Vector3(-repeatWidth, transform.position.y, transform.position.z);
        }
    }
}
