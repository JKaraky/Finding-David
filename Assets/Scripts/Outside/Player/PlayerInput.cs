using System;
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

    public static event Action Jumped;

    // Update is called once per frame
    void Update()
    {
        WalkingInput();
        SpaceButtonPressed();
    }

    private void WalkingInput()
    {
        _xMovement = Input.GetAxis("Horizontal");
    }

    private void SpaceButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumped?.Invoke();
        }
    }
}
