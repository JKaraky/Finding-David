using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMove playerMove;
    Animator animator;
    float horizontalMove;

    public void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = playerMove.movement;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(horizontalMove > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalMove < 0)
        {
            transform .localScale = new Vector3(-1, 1, 1);
        }
    }
}
