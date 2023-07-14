using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTimer : MonoBehaviour
{
    [SerializeField] int minWaitTime;
    [SerializeField] int maxWaitTime;
    [SerializeField] GameObject entity;
    private bool timeDetermined = false;
    // Start is called before the first frame update
    void Update()
    {
        Debug.Log(entity.activeSelf);
        if (!entity.activeSelf && !timeDetermined)
        {
            StartCoroutine(SpawnEntity());
        }
    }

    private IEnumerator SpawnEntity()
    {
        int entityWaitTime = Random.Range(minWaitTime, maxWaitTime + 1);
        timeDetermined = true;
        yield return new WaitForSeconds(entityWaitTime);
        entity.SetActive(true);
        timeDetermined = false;
    }
}
