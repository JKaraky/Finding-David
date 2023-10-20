using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class ChangeLightColor : MonoBehaviour
{
    #region Variables

    private Light2D tvLight;
    private bool coroutineDone = true;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tvLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineDone)
        {
            StartCoroutine("ChangeColor");
        }
    }

    #region Change Color Logic
    private IEnumerator ChangeColor()
    {
        coroutineDone = false;
        Color startColor = Random.ColorHSV();
        Color endColor = Random.ColorHSV();
        float timeToNextChange = Random.Range(0.5f, 5);
        float currentTime = 0;

        while( currentTime < timeToNextChange )
        {
            tvLight.color = Color.Lerp(startColor, endColor, currentTime/timeToNextChange);
            currentTime += Time.deltaTime;
            yield return null;
        }

        coroutineDone = true;
    }
    #endregion
}
