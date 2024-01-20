using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float maxVolume;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FadeVolume(float time)
    {
        StartCoroutine(FadeVol(time));
    }

    private IEnumerator FadeVol(float time)
    {
        float elapsedTime = 0;
        float start = 0;
        float end = 0;
        float currentVolume = audioSource.volume;

        if(currentVolume == maxVolume)
        {
            start = maxVolume;
            end = 0;
        }
        else
        {
            start = 0;
            end = maxVolume;
        }

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(start, end, elapsedTime / time);
            audioSource.volume = newVolume;
            yield return null;
        }
    }
}
