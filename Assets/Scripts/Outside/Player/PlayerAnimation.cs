using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMove playerMove;
    Animator animator;
    float horizontalMove;
    [SerializeField] private bool allowedJump;

    public void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }
    // Update is called once per frame
    void Update()
    {
        WalkingAnimation();
    }

    private void WalkingAnimation()
    {
        // To activate animation
        horizontalMove = playerMove.movement;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // To flip sprite
        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void JumpingAnimation()
    {
        if(allowedJump)
        {

        }
    }
}
