using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool top;

        
     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale *= -1;
            Rotation();
        }
    }

    void Rotation()
    {
        if (top == false)
        {
            transform.eulerAngles = new Vector3(0,0,180f);
        }
        else
        {
            transform.eulerAngles =  Vector3.zero;
        }
        top = !top;
    }

}
