using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcAnimator : MonoBehaviour
{
    PlayerController alcControls;
    Animator animator;
    float horizontalMove;
    bool onGround;

    public void Start()
    {
        animator = GetComponent<Animator>();
        alcControls = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = alcControls.moveInput;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, -1, 1);
        }

        onGround = alcControls.isGrounded;
        animator.SetBool("onGround", onGround);
        Debug.Log(onGround);
    }
}
