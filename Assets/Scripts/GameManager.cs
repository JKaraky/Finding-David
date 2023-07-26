using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public GameObject gameOver;
    public Transform playerStart;
    public GameObject player;
    public GameObject Entity;

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

    public void RestartLevel()
    {
        Time.timeScale = 1;
        gameOver.SetActive(false);
        player.transform.position = playerStart.position;
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        if (Entity != null)
        {
            Entity.SetActive(false);
        }
        gameOver.SetActive(true);
    }
}
