using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearBehavior : MonoBehaviour
{
    #region Variables
    [Header("Movement and Jump")]
    private bool isFacingRight = true;

    [Header("Ray")]
    public LayerMask entityMask;
    [SerializeField] GameObject entity;

    public static event Action EntityIsHit;
    #endregion

    // Update is called once per frame
    void Update()
    {
        LightRay();
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    void LightRay()
    {
        if (!isFacingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 2f, entityMask);

            Debug.DrawRay(transform.position, Vector2.left * 2f, Color.green);

            if (hit.collider != null)
            {
                entity.SetActive(false);
                EntityIsHit?.Invoke();
            }
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
