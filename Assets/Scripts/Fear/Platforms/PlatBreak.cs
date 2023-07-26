using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlatBreak : MonoBehaviour
{
    [SerializeField] float timeUntilBreak;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitForBreak());
        }
    }

    IEnumerator WaitForBreak()
    {
        yield return new WaitForSeconds(timeUntilBreak);
        gameObject.SetActive(false);
    }
}
