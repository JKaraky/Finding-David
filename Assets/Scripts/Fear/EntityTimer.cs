using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTimer : MonoBehaviour
{
    [SerializeField] int minWaitTime;
    [SerializeField] int maxWaitTime;
    [SerializeField] GameObject entity;
    private bool timeDetermined = false;
    private Coroutine timer;
    // Start is called before the first frame update
    void Update()
    {
        if (!entity.activeSelf && !timeDetermined)
        {
            timer = StartCoroutine(SpawnEntity());
        }
    }

    public void PauseEntityTimer()
    {
        Debug.Log("Entered safe area");
        timeDetermined = true;
        if (entity.activeSelf)
        {
            entity.GetComponent<ParticleSystem>().Play();
        }
        entity.SetActive(false);
        StopCoroutine(timer);
    }

    public void RestartEntityTimer()
    {
        Debug.Log("Exited safe area");
        timeDetermined = false;
    }

    private IEnumerator SpawnEntity()
    {
        int entityWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime + 1);
        timeDetermined = true;
        yield return new WaitForSeconds(entityWaitTime);
        entity.SetActive(true);
        timeDetermined = false;
    }
}
