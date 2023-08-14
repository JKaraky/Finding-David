using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float limitAngle;
    public float swaySpeed;
    private int inverse = 1;
    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(this.transform.position + new Vector3(5f, 0, 0), new Vector3(0, 0, 1f), swaySpeed * inverse * Time.deltaTime);

        if(Math.Abs(this.transform.rotation.eulerAngles.z) > limitAngle && Math.Abs(this.transform.rotation.eulerAngles.z) < 360 - limitAngle)
        {
            inverse *= -1;
        }

    }
}
