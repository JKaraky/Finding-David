using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearGameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> platforms;

    public GameObject GetRandomPlatform()
    {
        GameObject plat = platforms[Random.Range(0, platforms.Count)]; // Get random non breakable platform
        platforms.Remove(plat); // Remove it from the list so we don't get it again
        return plat;
    }
    private void CallEndGame()
    {
        GameManager.gameManagerInstance.EndGame();
    }

    private void OnEnable()
    {
        FearEntity.ReachedGoal += CallEndGame;
    }

    private void OnDisable()
    {
        FearEntity.ReachedGoal -= CallEndGame;
    }
}
