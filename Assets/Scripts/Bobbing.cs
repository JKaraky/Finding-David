using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class Bobbing : MonoBehaviour
{
    float originalY;

    [SerializeField] float floatStrength;

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    { 
        transform.position = new Vector3(transform.position.x, originalY + ((float)Math.Sin(Time.time) * floatStrength), transform.position.z);
    }
}
