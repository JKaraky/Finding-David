using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FearEntity : MonoBehaviour
{
    #region Variables

    [SerializeField] GameObject player;
    [SerializeField] GameObject entityGoal;

    [SerializeField] float distanceFromPlayer;
    [SerializeField] float fixedSpeed;

    float variableSpeed;

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

    void OnEnable()
    {
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
        Debug.Log(variableSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EntityGoal"))
        {
            ReachedGoal?.Invoke();
        }
    }
}
