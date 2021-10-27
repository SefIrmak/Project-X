using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isRunningHash;

    void Start()
    {
        animator = GetComponent<Animator>();

        //increases performance
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey("d");
        float horizontalMovement = Input.GetAxisRaw("Horizontal");



        if (horizontalMovement > 0 || horizontalMovement < 0)
        {
            //then set the isRunning to true
            animator.SetBool(isRunningHash, true);
        }
        if (horizontalMovement == 0f)
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
