using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    #region Variables

    public LayerMask entityMask;

    [SerializeField] GameObject entity;
    [SerializeField] GameObject player;
    [SerializeField] float entityLeaveTime = 1;

    [Tooltip("How long flashlight stays off after being hit by faces enemy")]
    [SerializeField] float offTimeFlashlight = 2;

    public static event Action EntityIsHit;

    private float direction = 1;
    private bool entityProtocolRunning = false;
    private bool flashlightOn = true;
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

        if(flashlightOn)
        {
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
                if (entityProtocolRunning)
                {
                    StopCoroutine("EntityHitProtocol");
                    entityProtocolRunning = false;
                    fearEntityScript.VariableSpeed = fearEntityScript.FixedSpeed;
                }
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

    #region Method and Behavior When Faces Enemy Reaches Player

    public void TurnOffFlashlight()
    {
        StartCoroutine(FlashlightBehavior());
    }

    IEnumerator FlashlightBehavior()
    {
        flashlightOn = false;
        Light2D light = GetComponent<Light2D>();
        FlashlightFlicker flickerer = GetComponent<FlashlightFlicker>();

        light.enabled = false;
        flickerer.enabled = false;

        yield return new WaitForSeconds(offTimeFlashlight);

        flashlightOn = true;
        light.enabled = true;
        flickerer.enabled = true;
    }
    #endregion

    #region Subscription to Events

    private void OnEnable()
    {
        FacesMove.Hitplayer += TurnOffFlashlight;
    }

    private void OnDisable()
    {
        FacesMove.Hitplayer -= TurnOffFlashlight;
    }

    #endregion
}
