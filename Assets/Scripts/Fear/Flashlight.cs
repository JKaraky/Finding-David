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

    public static event Action EntityIsHit;

    private float direction = 1;
    #endregion

    // Update is called once per frame
    void Update()
    {
        LightRay();
    }

    void LightRay()
    {
        // So that the ray flips with the player when he is looking the opposite way
        direction = player.transform.localScale.x;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x * direction, 0), 3.5f, entityMask);

        Debug.DrawRay(transform.position, new Vector2(transform.localScale.x * direction, 0) * 3.5f, Color.green);

        if (hit.collider != null)
        {
            entity.SetActive(false);
            EntityIsHit?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            // Game Over
            GameManager.gameManagerInstance.RestartLevel();
        }
    }
}
