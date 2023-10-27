using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlayer : MonoBehaviour
{
    #region Variables

    [SerializeField] float holdTime;
    private BoxCollider2D handCollider;

    #endregion

    #region Hold Logic

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get reference to movement script
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();

            // Get player rigidbody and stop it from moving further
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            playerRb.velocity = new Vector2(0, 0);

            // Prevent player from moving for a limited time
            StartCoroutine(HoldTime(playerMove));
        }
    }

    #endregion

    #region Enumerator for Hold Time

    IEnumerator HoldTime(PlayerMove playerMove)
    {
        playerMove.enabled = false;

        yield return new WaitForSeconds(holdTime);

        playerMove.enabled = true;
    }

    #endregion
}
