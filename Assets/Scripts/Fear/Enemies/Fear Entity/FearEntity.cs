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
    [SerializeField] GameObject entityToMove;
    [SerializeField] GameObject entityGoal;

    [SerializeField] float distanceFromPlayer;
    [SerializeField] float fixedSpeed;

    float variableSpeed;
    int playerFollowOrPlat;
    bool entityCanMove;

    GameObject playerFollow;
    GameObject platformToBreak;
    PlayerClone playerCloneScript;
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
        playerCloneScript = gameObject.GetComponentInParent<PlayerClone>();
    }
    void OnEnable()
    {
        entityCanMove = true;
        // Determine whether entity will attack player or make a platform breakable (0 for player, 1 for platform)
        playerFollowOrPlat = UnityEngine.Random.Range(0, 2);

        DetermineEntityTask(playerFollowOrPlat);

        variableSpeed = fixedSpeed;
        // Getting rotation of entity
        currentRotation = transform.rotation.eulerAngles;

        if (player.transform.localScale.x > 0)
        {
            entityToMove.transform.position = entityGoal.transform.position - new Vector3(distanceFromPlayer, 0, 0);
            // Flipping entity
            currentRotation.y = 0f;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
        else
        {
            entityToMove.transform.position = entityGoal.transform.position + new Vector3(distanceFromPlayer, 0, 0);
            // Flipping it back
            currentRotation.y = 180f;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    void Update()
    {
        if (entityCanMove)
        {
            float step = variableSpeed * Time.deltaTime;
            entityToMove.transform.position = Vector3.MoveTowards(entityToMove.transform.position, entityGoal.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EntityGoal"))
        {
            ReachedGoal?.Invoke();
            //gameObject.SetActive(false);
        }
        // Entity stops moving when it's within a certain distance of the player so it can catch him
        else if (collision.CompareTag("StopEntityTrigger"))
        {
            entityCanMove = false;
        }
        else if (playerFollowOrPlat==1 && collision.gameObject.layer == 8) // Hits a platform
        {
            gameObject.SetActive(false);
            collision.gameObject.AddComponent<PlatBreak>();
        }
    }

    private void DetermineEntityTask(int toggle)
    {
        if (toggle == 0)
        {
            entityGoal = playerFollow;

            //playerCloneScript.enabled = true; // Follow player 
        }
        else
        {
            platformToBreak = gameManager.GetRandomPlatform();
            entityGoal = platformToBreak;

            playerCloneScript.enabled = false; // Don't follow player
        }
    }
}
