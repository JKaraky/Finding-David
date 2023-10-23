using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearBehavior : MonoBehaviour
{
    #region Variables

    public LayerMask entityMask;

    [SerializeField] GameObject entity;

    public static event Action EntityIsHit;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < 0)
        {
            LightRay();
        }
    }

    void LightRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 2f, entityMask);

        Debug.DrawRay(transform.position, Vector2.left * 2f, Color.green);

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
