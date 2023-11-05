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
        if(player.transform.localScale.x > 0)
        {
            transform.position = player.transform.position - new Vector3(distanceFromPlayer, 0, 0);
        }
        else
        {
            transform.position = player.transform.position + new Vector3(distanceFromPlayer, 0, 0);
        }
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EntityGoal"))
        {
            ReachedGoal?.Invoke();
        }
    }
}
