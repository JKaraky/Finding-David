using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    #region Variables

    // Action references
    [SerializeField]
    private InputActionReference moveAction, jumpAction, interact;

    // To store horizontal movement
    private float _xMovement;
    private Action<InputAction.CallbackContext> spaceButtonTrigger;
    private Action<InputAction.CallbackContext> enterTrigger;
    public float XMovement
    {
        get
        {
            return _xMovement;
        }
    }

    // To know when buttons were pressed
    public static event Action Jumped;
    public static event Action Interacted;

    #endregion

    // Update is called once per frame
    void Update()
    {
        WalkingInput();
    }

    #region Input functions
    private void WalkingInput()
    {
        _xMovement = moveAction.action.ReadValue<Vector2>().x;
    }

    private void SpaceButtonPressed()
    {
        Jumped?.Invoke();
    }

    private void EnterPressed()
    {
        Interacted?.Invoke();
    }

    #endregion

    #region Subscribtion to events
    private void OnEnable()
    {
        jumpAction.action.performed += spaceButtonTrigger;
        interact.action.performed += enterTrigger;
    }
    private void OnDisable()
    {
        jumpAction.action.performed -= spaceButtonTrigger;
        interact.action.performed -= enterTrigger;
    }
    private void Awake()
    {
        spaceButtonTrigger = (ctx) => SpaceButtonPressed();
        enterTrigger = (ctx) => EnterPressed();
    }

    #endregion
}
