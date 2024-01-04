using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacesAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip[] correctSelections;
    [SerializeField] AudioClip wrongSelection;

    private int correctCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCorrectSelection()
    {
        audioSource.PlayOneShot(correctSelections[correctCount]);

        correctCount++;

        if(correctCount == correctSelections.Length)
        {
            correctCount = 0;
        }
    }

    public void PlayWrongSelection()
    {
        audioSource.PlayOneShot(wrongSelection);
    }
}
