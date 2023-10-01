using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("For Walking")]
    [SerializeField] private float moveSpeed;
    [HideInInspector] public float movement;

    [Header("For Jumping")]
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float jumpForce;
    [HideInInspector] public bool isJumping;

    [Header("General Components")]
    [SerializeField] private PlayerInput playerInput;
    private BoxCollider2D boxCollider;
    private Rigidbody2D playerRb;
    private bool checkingForLanding;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        isJumping = false;
        // Check collider for usage
        checkingForLanding = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // To handle player walking
    private void Move()
    {
        movement = playerInput.XMovement;
        playerRb.velocity = new Vector2(movement * moveSpeed, playerRb.velocity.y);
    }

    // To check if the player is on the ground so he can jump
    bool isGrounded()
    {
        float extraHeight = 0.2f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        return raycastHit.collider != null;
    }

    // To handle Jumping
    void Jump()
    {
        if (isGrounded())
        {
            // Player jumped, now we must check when he lands using collider
            checkingForLanding = true;
            isJumping = true;

            movement = playerInput.XMovement;

            playerRb.AddForce(new Vector2(movement, 1) * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Platform layer is number 8
        // We only need to switch isJumping during a player jump, hence the flag checkingForLanding
        if (collision.collider.gameObject.layer == 8 && checkingForLanding)
        {
            isJumping = false;
        }
    }

    // In order to jump we subscribe to the spacebar event in the player input class
    private void OnEnable()
    {
        PlayerInput.Jumped += Jump;
    }

    private void OnDisable()
    {
        PlayerInput.Jumped -= Jump;
    }
}
