using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    public PlayerController controls;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controls = GameObject.FindObjectOfType<PlayerController>();

        // increases performance
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);

        bool mvmtKeyPressed;

        // get player input
        if ((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")))
        {
            mvmtKeyPressed = true;
        }
        else
        {
            mvmtKeyPressed = false;
        }

        // if player presses wasd key
        if (!isWalking && mvmtKeyPressed)
        {
            // then set the isWalking boolean to be true
            animator.SetBool(isWalkingHash, true);
        }

        // if player is not pressing wasd key
        if (isWalking && !mvmtKeyPressed)
        {
            // then set the isWalking boolean to be false
            animator.SetBool(isWalkingHash, false);
        }
    }
}
