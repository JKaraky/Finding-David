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

    [SerializeField] float distanceFromPlayer;
    [SerializeField] float fixedSpeed;

    float variableSpeed;
    int playerFollowOrPlat;

    GameObject playerFollow;
    GameObject platformToBreak;
    PlayerClone playerCloneScript;

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
        // Determine whether entity will attack player or make a platform breakable (0 for player, 1 for platform)
        playerFollowOrPlat = UnityEngine.Random.Range(0, 2);

        DetermineEntityTask(playerFollowOrPlat);

        variableSpeed = fixedSpeed;

        if(player.transform.localScale.x > 0)
        {
            transform.position = entityGoal.transform.position - new Vector3(distanceFromPlayer, 0, 0);
        }
        else
        {
            transform.position = entityGoal.transform.position + new Vector3(distanceFromPlayer, 0, 0);
        }
    }

    void Update()
    {
        float step = variableSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, entityGoal.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EntityGoal"))
        {
            ReachedGoal?.Invoke();
        }
        else if (collision.gameObject.layer == 8) // Hits a platform
        {
            gameObject.SetActive(false);
            collision.gameObject.AddComponent<PlatBreak>();
        }
    }

    private void DetermineEntityTask(int toggle)
    {
        if (playerFollowOrPlat == 0)
        {
            entityGoal = playerFollow;

            playerCloneScript.enabled = true; // Follow player 
        }
        else
        {
            platformToBreak = gameManager.GetRandomPlatform();
            entityGoal = platformToBreak;

            playerCloneScript.enabled = false; // Don't follow player
        }
    }
}
