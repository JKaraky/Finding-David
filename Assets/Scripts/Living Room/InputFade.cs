using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer))]
public class InputFade : MonoBehaviour
{
    private enum Behavior {FadeIn, FadeOut, Both};
    private SpriteRenderer spriteRenderer;

    [SerializeField] InputActionReference triggerEvent;
    [SerializeField] Behavior behavior;
    [SerializeField] float fadeDuration;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        triggerEvent.action.performed += ctx => triggeredFade();
    }

    private void triggeredFade()
    {
        if (behavior == Behavior.Both)
        {
            if (spriteRenderer.color.a > 0)
            {
                StopAllCoroutines();
                StartCoroutine(gameObject.FadeOut(fadeDuration));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(gameObject.FadeIn(fadeDuration));
            }
        }
        else if(behavior == Behavior.FadeOut)
        {
            StopAllCoroutines();
            StartCoroutine(gameObject.FadeOut(fadeDuration));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(gameObject.FadeIn(fadeDuration));
        }
    }
}
