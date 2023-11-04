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
        try
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }catch (System.Exception e)
        {
            Debug.LogException(e);
        }
        // Disabling the colliders and sprite renderers in children as well
        foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = false;
        }
        foreach (Collider2D collider in gameObject.GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(1.25f);
        try
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }catch (System.Exception e)
        {
            Debug.LogException(e);
        }
        foreach (SpriteRenderer renderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = false;
        }
        foreach (Collider2D collider in gameObject.GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = false;
        }
    }
}
