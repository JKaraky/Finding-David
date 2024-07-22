using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FearEntity : MonoBehaviour
{
    #region Variables

    [SerializeField] FearGameManager gameManager;
    [SerializeField] GameObject player;
    [SerializeField] GameObject entityGoal;

    [SerializeField] float distanceFromPlayerX;
    [SerializeField] float distanceFromPlayerY;
    [SerializeField] float fixedSpeed;

    float variableSpeed;

    GameObject playerFollow;
    Vector3 currentRotation;

    public float VariableSpeed
    {
        get { return variableSpeed; }
        set { variableSpeed = value; }
    }

    public float FixedSpeed
    {
        get { return fixedSpeed; }
    }

    public static event Action ReachedGoal;

    #endregion

    private void Awake()
    {
        playerFollow = entityGoal; // Saving the player destination
    }
    void OnEnable()
    {
        variableSpeed = fixedSpeed;
        // Getting rotation of entity
        currentRotation = transform.rotation.eulerAngles;

        if (player.transform.localScale.x > 0)
        {
            transform.position = entityGoal.transform.position - new Vector3(distanceFromPlayerX, distanceFromPlayerY, 0);
            // Flipping entity
            currentRotation.y = 0f;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
        else
        {
            transform.position = entityGoal.transform.position + new Vector3(distanceFromPlayerX, 0, 0) - new Vector3(0, distanceFromPlayerY, 0);
            // Flipping it back
            currentRotation.y = 180f;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    void Update()
    {
        float step = variableSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, entityGoal.transform.position - new Vector3(0, distanceFromPlayerY, 0), step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EntityGoal"))
        {
            ReachedGoal?.Invoke();
            Debug.Log("Game Over Man");
            gameObject.SetActive(false);
        }
    }
}
