using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearGameManager : MonoBehaviour
{
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
