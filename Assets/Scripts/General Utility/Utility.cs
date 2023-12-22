using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public static class Utility
{
    #region FadeIn and FadeOut
    public static IEnumerator FadeIn(this GameObject gameObject, float timeForProcess)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;

        // In order to get percentage of fade for timer accuracy
        float timerPercentage;

        if (startValue == 0)
        {
            timerPercentage = 1;
        }
        else
        {
            timerPercentage = 1 - startValue;
        }

        // Fade process
        while (elapsedTime < timeForProcess)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 1, elapsedTime / (timeForProcess * timerPercentage));
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    public static IEnumerator FadeOut(this GameObject gameObject, float timetimeForProcess)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;

        while (elapsedTime < timetimeForProcess)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0, elapsedTime / (timetimeForProcess * startValue));
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }
    #endregion
}
