using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVControl : MonoBehaviour
{
    #region Variables

    [SerializeField] private AudioSource TvSound;
    [SerializeField] private ChangeLightColor TvLightController;

    private bool TvOn = true;
    private bool inDistance = false;

    #endregion

    #region Tv Control Method
    private void ToggleTv()
    {
        if (inDistance)
        {
            if (TvOn)
            {
                TvLightController.gameObject.SetActive(false);
                TvSound.Pause();
                TvOn = false;
            }
            else
            {
                TvLightController.gameObject.SetActive(true);
                TvSound.Play();
                TvOn = true;
            }
        }
    }

    #endregion

    #region Subscribe To Interact Button
    private void OnEnable()
    {
        PlayerInput.Interacted += ToggleTv;
    }

    private void OnDisable()
    {
        PlayerInput.Interacted -= ToggleTv;
    }

    #endregion

    #region Check for Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDistance = true;
            Debug.Log(inDistance);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDistance = false;
        }
    }

    #endregion
}
