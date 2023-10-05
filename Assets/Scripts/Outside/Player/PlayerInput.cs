using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private InputActionReference moveAction, jumpAction;
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
    }

    private void WalkingInput()
    {
        _xMovement = moveAction.action.ReadValue<Vector2>().x;
    }

    private void SpaceButtonPressed()
    {
        Jumped?.Invoke();
    }
    private void OnEnable()
    {
        jumpAction.action.performed += ctx => SpaceButtonPressed();
    }
    private void OnDisable()
    {
        jumpAction.action.performed -= ctx => SpaceButtonPressed();
    }
}
