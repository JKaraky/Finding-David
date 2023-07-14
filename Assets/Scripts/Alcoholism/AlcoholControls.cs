using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholControls : MonoBehaviour
{
    #region Variables
    [Header("Movement and Jump")]
    private Rigidbody2D playerRb;
    private float xMovement;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("To Check Ground")]
    [SerializeField] private LayerMask platformLayerMask;
    private BoxCollider2D boxCollider;
    #endregion
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        if (isGrounded())
        {
            xMovement = Input.GetAxisRaw("Horizontal");

            playerRb.velocity = new Vector2(xMovement * moveSpeed, playerRb.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            xMovement = Input.GetAxisRaw("Horizontal");

            playerRb.AddForce(new Vector2(xMovement, -1) * jumpForce, ForceMode2D.Impulse);
        }
    }

    // To check if the player is on the ground so he can jump again
    bool isGrounded()
    {
        float extraHeight = 0.2f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, extraHeight, platformLayerMask);
        return raycastHit.collider != null;
    }
}
