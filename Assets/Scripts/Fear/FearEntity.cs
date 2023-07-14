using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEntity : MonoBehaviour
{
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject player;
    public float losingDistance;
    public int timeUntilDeath;
    [HideInInspector] public float elapsedTime;
    void OnEnable()
    {
        elapsedTime = 0;
        transform.position = player.transform.position - new Vector3 (distanceFromPlayer, 0, 0);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / timeUntilDeath;
        transform.position = Vector3.Lerp(transform.position, player.transform.position - new Vector3(losingDistance, 0, 0), percentageComplete/100);
    }
}
