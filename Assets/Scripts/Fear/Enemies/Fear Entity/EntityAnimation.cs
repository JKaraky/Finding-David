using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimation : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform rightArmPosition;
    [SerializeField] Transform leftArmPosition;
    [SerializeField] Transform rightArmTarget;
    [SerializeField] Transform leftArmTarget;

    Animator animator;
    bool holdingPlayer;

    public static event Action GameOver;
    #endregion

    private void OnEnable()
    {
        FearEntity.ReachedGoal += HeldAnimation;
    }
    private void OnDisable()
    {
        FearEntity.ReachedGoal -= HeldAnimation;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void HeldAnimation()
    {
        animator.SetTrigger("GrabbingPlayer");

        // Invoke GameOver after animation is over
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StopEntityTrigger"))
        {
            rightArmTarget = rightArmPosition;
            leftArmTarget = leftArmPosition;
        }
    }
}
