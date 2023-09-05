using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D playerRb;
    [HideInInspector] public float movement;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = playerInput.XMovement;
        playerRb.velocity = new Vector2(movement * moveSpeed, playerRb.velocity.y);
    }
}
