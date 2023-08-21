using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float limitAngle;
    public float swaySpeed;
    private float varSwaySpeed;
    private int inverse = 1;
    public float transitionTime = 2;
    private float timeToSlow = 0;
    private float timeToAcc = 0;

    // Phases for transition to reverse rotation
    private bool endPhaseOne = false;
    private bool endPhaseTwo = false;

    void Start()
    {
        varSwaySpeed = swaySpeed;
    }
    // Update is called once per frame
    void Update()
    {
        // reference to camera rotation
        float cameraRotation = this.transform.rotation.eulerAngles.z;

        // to store angle as a negative number when exceeding halfway of circle
        if (cameraRotation > 180)
        {
            cameraRotation -= 360;
        }

        // if camera goes beyond the angle limit it starts transitioning
        if(cameraRotation > limitAngle || cameraRotation < - limitAngle)
        {
            Debug.Log("Transition takes over");
            RotationTransition();
        }
        else
        {
            Debug.Log("standard takes over");
            // Method that keeps roating camera
            RotateCamera(varSwaySpeed);
            ResetTransition();
        }
    }

    private void RotateCamera(float rotationSpeed)
    {
        this.transform.RotateAround(this.transform.position + new Vector3(5f, 0, 0), new Vector3(0, 0, 1f), rotationSpeed * inverse * Time.deltaTime);
    }

    private void RotationTransition()
    {
        // Gradually slow camera to a stop
        if (!endPhaseOne)
        {
            SlowCamera();
        }
        else if (!endPhaseTwo)
        {
            ReverseRotation();

        }
        else
        {
            ReRotateCamera();
        }
    }

    private void SlowCamera()
    {
        if (timeToSlow < transitionTime)
        {
            varSwaySpeed = Mathf.Lerp(swaySpeed, 0, timeToSlow / transitionTime);

            RotateCamera(varSwaySpeed);

            timeToSlow += Time.deltaTime;
        }
        else
        {
            endPhaseOne = true;
        }
    }

    private void ReverseRotation()
    {
        inverse *= -1;
        endPhaseTwo = true;
    }

    private void ReRotateCamera()
    {
        if (timeToAcc < transitionTime)
        {
            varSwaySpeed = Mathf.Lerp(0, swaySpeed, timeToAcc / transitionTime);

            RotateCamera(varSwaySpeed);

            timeToAcc += Time.deltaTime;
        }
        else
        {
            varSwaySpeed = swaySpeed;
            RotateCamera(varSwaySpeed);
        }
    }

    private void ResetTransition()
    {
        endPhaseOne = false;
        endPhaseTwo = false;
        timeToSlow = 0;
        timeToAcc = 0;
    }
}
