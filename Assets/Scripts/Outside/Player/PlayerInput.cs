using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _xMovement;
    public float XMovement
    {
        get
        {
            return _xMovement;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _xMovement = Input.GetAxis("Horizontal");
    }
}
