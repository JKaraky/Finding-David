using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVControl : MonoBehaviour
{
    #region Variables

    [SerializeField] private AudioSource TvSound;
    [SerializeField] private ChangeLightColor TvLightController;
    private bool TvOn = true;

    #endregion

    #region Tv Control Method
    private void ToggleTv()
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
}
