using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] int rotationSpeed;
    [SerializeField][Range(-1, 1)] int rotation;
    [SerializeField] bool altRotation = false;
    [SerializeField] float rotInterval;

    float timeElapsed = 0;
    int rotSwitcher = 1;
    // Update is called once per frame
    void Update()
    {
        if (altRotation)
        {
            if (timeElapsed < rotInterval)
            {
                transform.Rotate(Vector3.forward * rotation * rotSwitcher * rotationSpeed * Time.deltaTime);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0;
                rotSwitcher *= -1;
            }
        }
        else
        {
            transform.Rotate(Vector3.forward * rotation * rotationSpeed * Time.deltaTime);
        }
    }
}
