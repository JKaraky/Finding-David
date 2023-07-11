using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public GameObject gameOver;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            gameManagerInstance = this;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }
}
