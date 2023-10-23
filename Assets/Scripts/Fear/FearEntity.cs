using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FearEntity : MonoBehaviour
{
    #region Variables

    [SerializeField] GameObject player;

    [SerializeField] float speed;
    [SerializeField] float distanceFromPlayer;

    public static event Action ReachedGoal;

    #endregion

    void OnEnable()
    {
        transform.position = player.transform.position - new Vector3 (distanceFromPlayer, 0, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EntityGoal"))
        {
            ReachedGoal?.Invoke();
        }
    }
}
