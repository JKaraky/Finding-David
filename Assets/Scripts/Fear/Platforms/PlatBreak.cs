using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlatBreak : MonoBehaviour
{
    [SerializeField] float timeUntilBreak = 2;

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

        // Disabling children lol
        foreach (Transform childTransform in this.transform)
        {
            GameObject child = childTransform.gameObject;
            child.SetActive(false);
        }

        this.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(6f);

        // Enabling children boo
        foreach (Transform childTransform in this.transform)
        {
            GameObject child = childTransform.gameObject;
            child.SetActive(true);
        }

        this.GetComponent<Collider2D>().enabled = true;
    }
}
