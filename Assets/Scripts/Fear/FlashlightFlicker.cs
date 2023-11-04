using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightFlicker : MonoBehaviour
{
    public float minIntensity = 0.1f;
    public float flickerSpeed = 1.0f;
    public float flickerDuration = 1.5f; // The duration in seconds.

    private Light2D flashlight;
    private float flickerTime;
    private float maxIntensity;
    private bool isFlickering = true;

    void Start()
    {
        flashlight = GetComponent<Light2D>();
        maxIntensity = flashlight.intensity;
        flickerTime = Random.Range(0.0f, 1.0f); // Randomize the starting time for variation.
        Invoke("StopFlicker", flickerDuration); // Invoke the method to stop flickering after the specified duration.
    }

    void Update()
    {
        if (isFlickering)
        {
            flickerTime += Time.deltaTime * flickerSpeed;
            float flickerValue = Mathf.PerlinNoise(flickerTime, 0);
            float newIntensity = Mathf.Lerp(minIntensity, maxIntensity, flickerValue);
            flashlight.intensity = newIntensity;
        }
    }

    void StopFlicker()
    {
        isFlickering = false;
        flashlight.intensity = maxIntensity; // Set the intensity to its maximum value when stopping the flicker.
    }
}
