using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer))]
public class TriggerFade : MonoBehaviour
{
    // For fade logic
    [SerializeField] private float fadeDuration;
    [SerializeField] private GameObject[] triggerAreas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerAreas.Contains(collision.gameObject))
        {
            StopAllCoroutines();
            StartCoroutine(gameObject.FadeIn(fadeDuration));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerAreas.Contains(collision.gameObject))
        {
            StopAllCoroutines();
            StartCoroutine(gameObject.FadeOut(fadeDuration));
        }
    }
}
