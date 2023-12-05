using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
        JumpingAnimation();
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
            animator.SetBool("IsJumping", playerMove.isJumping);
        }
    }

    private void GrabbedAnimation()
    {
        animator.SetTrigger("Held");
    }

    private void ReleasedAnimation()
    {
        animator.SetTrigger("Released");
    }

    private void OnEnable()
    {
        HoldPlayer.GrabbedPlayer += GrabbedAnimation;
        HoldPlayer.ReleasedPlayer += ReleasedAnimation;
    }

    private void OnDisable()
    {
        HoldPlayer.GrabbedPlayer -= GrabbedAnimation;
        HoldPlayer.ReleasedPlayer -= ReleasedAnimation;
    }
}
