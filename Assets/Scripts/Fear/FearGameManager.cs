using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearGameManager : MonoBehaviour
{
    [SerializeField] GameObject entity;
    private FearEntity entityScript;
    [SerializeField] GameObject gameOver;

    void Awake()
    {
        entityScript = entity.GetComponent<FearEntity>();
    }
    // Update is called once per frame
    void Update()
    {
        if(entityScript.timeUntilDeath < entityScript.elapsedTime)
        {
            GameManager.gameManagerInstance.EndGame();
        }
    }
}
