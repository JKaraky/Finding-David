using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyLight : MonoBehaviour
{
    [SerializeField]
    GameObject _fearMnaager;

    private EntityTimer _timer;
    private void Start()
    {
        _timer = _fearMnaager.GetComponent<EntityTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _timer.PauseEntityTimer();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _timer.RestartEntityTimer();
        }
    }
}
