using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer))]
public class FadeOut : MonoBehaviour
{
    #region Variables
    // For fade logic
    [SerializeField] private float fadeDuration;
    [SerializeField] private GameObject AButton;
    [SerializeField] private GameObject RArrowButton;
    [SerializeField] private GameObject LArrowButton;
    [SerializeField] private GameObject DButton;
    [SerializeField] private PlayerInput playerInput;

    IEnumerator currentRoutineOne = null;
    IEnumerator currentRoutineTwo = null;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if(playerInput.XMovement > 0)
        {
            currentRoutineOne = FadeOutLogic(AButton);
            currentRoutineTwo = FadeOutLogic(RArrowButton);
            StartCoroutine(currentRoutineOne);
            StartCoroutine(currentRoutineTwo);
        } else if(playerInput.XMovement < 0)
        {
            currentRoutineOne = FadeOutLogic(DButton);
            currentRoutineTwo = FadeOutLogic(LArrowButton);
            StartCoroutine(currentRoutineOne);
            StartCoroutine(currentRoutineTwo);
        }

        if(AButton.GetComponent<SpriteRenderer>().material.color.a == 0 && DButton.GetComponent<SpriteRenderer>().material.color.a == 0) 
        { 
            this.gameObject.SetActive(false);
        }
    }

    #region Fade Out Logic
    private IEnumerator FadeOutLogic(GameObject objectToFade)
    {
        float elapsedTime = 0;
        Material spriteMaterial = objectToFade.GetComponent<SpriteRenderer>().material;
        float startValue = spriteMaterial.color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0, elapsedTime / fadeDuration);
            spriteMaterial.color = new Color(spriteMaterial.color.r, spriteMaterial.color.g, spriteMaterial.color.b, newAlpha);
            yield return null;
        }
    }
    #endregion
}
