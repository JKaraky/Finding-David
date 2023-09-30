using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearBehavior : MonoBehaviour
{
    #region Variables
    [Header("Movement and Jump")]
    private Rigidbody2D playerRb;
    private float xMovement;
    private bool isFacingRight = true;
    [SerializeField] private float jumpForce;

    [Header("To Check Ground")]
    [SerializeField] private LayerMask platformLayerMask;
    private BoxCollider2D boxCollider;

    [Header("Ray")]
    public LayerMask entityMask;
    [SerializeField] GameObject entity;

    public static event Action EntityIsHit;
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
        LightRay();
    }

    void Movement()
    {
        if (isGrounded())
        {
            xMovement = Input.GetAxisRaw("Horizontal");
        }

        if (xMovement > 0 && !isFacingRight)
        {
            Flip();
        }
        if (xMovement < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            xMovement = Input.GetAxisRaw("Horizontal");

            playerRb.AddForce(new Vector2(xMovement, 1) * jumpForce, ForceMode2D.Impulse);
        }
    }

    // To check if the player is on the ground so he can jump again
    bool isGrounded()
    {
        float extraHeight = 0.2f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        return raycastHit.collider != null;
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    void LightRay()
    {
        if (!isFacingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 2f, entityMask);

            Debug.DrawRay(transform.position, Vector2.left * 2f, Color.green);

            if (hit.collider != null)
            {
                entity.SetActive(false);
                EntityIsHit?.Invoke();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            // Game Over
            GameManager.gameManagerInstance.RestartLevel();
        }
    }
}
