using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed;
  public float jumpForce;
  private float moveInput;

  private Rigidbody2D rb;

  private bool facingRight = true;

  private bool isGrounded;
  public Transform groundCheck;
  public float checkRadius;
  public LayerMask whatIsGround;

  private int extraJumps;
  public int extraJumpsValue;

    private void Start()
    {
        //Use player Rigid body:
        rb = GetComponent < Rigidbody2D > ();
        extraJumps = extraJumpsValue;
    }

     void FixedUpdate()
    {
        //Input of the movement
        moveInput = Input.GetAxis("Horizontal");
        //Speed in x, and y not to be affected
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0 )
        {
            Flip();

        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

     void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;

        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.down * jumpForce;
            extraJumps--;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.down * jumpForce;
        }
    }

    //Flip the player
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }


}
