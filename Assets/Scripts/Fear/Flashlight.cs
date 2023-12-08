using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flashlight : MonoBehaviour
{
    #region Variables

    public LayerMask entityMask;

    [SerializeField] GameObject entity;
    [SerializeField] GameObject player;
    [SerializeField] float entityLeaveTime;

    public static event Action EntityIsHit;

    private float direction = 1;
    private bool entityProtocolRunning = false;
    private FearEntity fearEntityScript;
    #endregion

    void Start()
    {
        fearEntityScript = entity.GetComponent<FearEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        LightRay();
    }

    #region Lightray Logic
    void LightRay()
    {
        // So that the ray flips with the player when he is looking the opposite way
        direction = player.transform.localScale.x;

        // The actual ray
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x * direction, 0), 3.5f, entityMask);

        // To visualize ray
        Debug.DrawRay(transform.position, new Vector2(transform.localScale.x * direction, 0) * 3.5f, Color.green);

        // Handle entity leave protocol and stop it if it was running and the ray no longer is hitting entity
        if (hit.collider != null)
        {
            StartCoroutine("EntityHitProtocol");
        }
        else
        {
            if(entityProtocolRunning)
            {
                StopCoroutine("EntityHitProtocol");
                entityProtocolRunning = false;
                fearEntityScript.VariableSpeed = fearEntityScript.FixedSpeed;
            }
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            // Game Over
            GameManager.gameManagerInstance.RestartLevel();
        }
    }

    #region Protocol to Handle Entity When Hit by Light
    IEnumerator EntityHitProtocol()
    {
        if (entityProtocolRunning)
        {
           yield return null;
        }
        else
        {
            // Mark that protocol is running
            entityProtocolRunning = true;
            EntityIsHit?.Invoke();

            // Half entity speed and start timer until entity leaves
            FearEntity fearEntity = entity.GetComponent<FearEntity>();
            fearEntity.VariableSpeed /= 2;
            yield return new WaitForSeconds(entityLeaveTime);

            // When timer is done deactivate entity and mark protocol as finished
            entity.SetActive(false);
            entityProtocolRunning = false;
        }
    }
    #endregion
}
