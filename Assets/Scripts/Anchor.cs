using UnityEngine;

public class Anchor : MonoBehaviour
{
    public Animator animator;
    public bool animationFinished = false;
    public bool animationStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (animationStarted == true && animationFinished == false)
            animator.SetTrigger("anchorDown");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimationFinished()
    {
        animationFinished = true;
    }

    void OnAnimationStarted()
    {
        animationStarted = true;
    }
}
