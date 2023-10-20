using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer))]
public class Fade : MonoBehaviour
{
    #region Variables
    // For fade logic
    [SerializeField] private float fadeDuration;
    [SerializeField] private GameObject triggerArea;
    private SpriteRenderer spriteMaterial;

    // To check if coroutines are running
    private bool fadeInRunning, fadeOutRunning;
    IEnumerator currentRoutine = null;
    #endregion

    #region Start tasks
    // Start is called before the first frame update
    void Start()
    {
        // Reference sprite material and make it invisible
        spriteMaterial = GetComponent<SpriteRenderer>();
    }
    #endregion

    #region Fade In Logic
    private IEnumerator FadeIn()
    {
        // Fade variables
        fadeInRunning = true;
        float elapsedTime = 0;
        float startValue =  spriteMaterial.color.a;

        // In order to get percentage of fade for timer accuracy
        float timerPercentage;

        if(startValue == 0)
        {
            timerPercentage = 1;
        }
        else
        {
            timerPercentage = 1 - startValue;
        }

        // Fade process
        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 1, elapsedTime / (fadeDuration * timerPercentage));
            spriteMaterial.color = new Color(spriteMaterial.color.r, spriteMaterial.color.g, spriteMaterial.color.b, newAlpha);
            yield return null;
        }

        fadeInRunning = false;
    }
    #endregion

    #region Fade Out Logic
    private IEnumerator FadeOut()
    {
        fadeOutRunning = true;
        float elapsedTime = 0;
        float startValue = spriteMaterial.color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0, elapsedTime / (fadeDuration * startValue));
            spriteMaterial.color = new Color(spriteMaterial.color.r, spriteMaterial.color.g, spriteMaterial.color.b, newAlpha);
            yield return null;
        }

        fadeOutRunning = false;
    }
    #endregion

    #region Fade Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == triggerArea)
        {
            if(fadeOutRunning)
            {
                StopCoroutine(currentRoutine);
            }

            currentRoutine = FadeIn();

            StartCoroutine(currentRoutine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == triggerArea)
        {
            if (fadeInRunning)
            {
                StopCoroutine(currentRoutine);
            }

            currentRoutine = FadeOut();

            StartCoroutine(currentRoutine);
        }
    }
    #endregion
}
