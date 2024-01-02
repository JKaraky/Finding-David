using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacesAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip correctSelection;
    [SerializeField] AudioClip wrongSelection;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCorrectSelection()
    {
        audioSource.PlayOneShot(correctSelection);
    }

    public void PlayWrongSelection()
    {
        audioSource.PlayOneShot(wrongSelection);
    }
}
