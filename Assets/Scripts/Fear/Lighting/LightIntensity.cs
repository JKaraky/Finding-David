using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensity : MonoBehaviour
{
    Light2D objectLight;

    private void Start()
    {
        objectLight = GetComponent<Light2D>();
    }

    private void Update()
    {
        objectLight.intensity = transform.parent.gameObject.GetComponent<SpriteRenderer>().color.a;
    }
}
