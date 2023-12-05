using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlayer : MonoBehaviour
{
    #region Variables

    [SerializeField] float holdTime;

    private BoxCollider2D handCollider;
    private Transform grabPositionOne;
    private Transform grabPositionTwo;
    private bool deactivated = false;

    public static Action GrabbedPlayer;
    public static Action ReleasedPlayer;

    #endregion

    #region Hold Logic

    private void Start()
    {
        Transform[] grabPositions = gameObject.GetComponentsInChildren<Transform>();
        grabPositionOne = grabPositions[1];
        grabPositionTwo = grabPositions[2];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(deactivated)
            {
                return;
            }
            else
            {
                // Notify animator that player got grabbed
                GrabbedPlayer?.Invoke();

                // Get reference to movement script
                PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();

                // Get player rigidbody and stop it from moving further
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                playerRb.velocity = new Vector2(0, 0);

                //// Change player leg order to be behind hand
                //GameObject playerLeg = collision.gameObject.transform.Find("LeftLeg").gameObject;
                //int originalSortingLayer = playerLeg.GetComponent<SpriteRenderer>().sortingOrder;
                //playerLeg.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;

                //Snap player to grab position
                Vector3 playerPosition = collision.transform.position;

                if(playerPosition.x > gameObject.transform.position.x)
                {
                    playerPosition = grabPositionTwo.position;
                    collision.transform.position = playerPosition;
                }
                else
                {
                    playerPosition = grabPositionOne.position;
                    collision.transform.position = playerPosition;
                }

                // Prevent player from moving for a limited time
                StartCoroutine(HoldTime(playerMove));
            }
        }
    }

    #endregion

    #region Enumerator for Hold Time and Reactivation of Collision

    IEnumerator HoldTime(PlayerMove playerMove)
    {
        playerMove.enabled = false;

        yield return new WaitForSeconds(holdTime);

        ReleasedPlayer?.Invoke();

        playerMove.enabled = true;
    }

    #endregion
}
