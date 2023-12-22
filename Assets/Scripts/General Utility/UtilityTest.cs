using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityTest : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(gameObject.FadeOut(2.5f));
    }
}
