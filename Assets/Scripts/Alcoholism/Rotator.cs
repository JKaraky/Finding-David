using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] int rotationSpeed;
    [SerializeField][Range(-1, 1)] int rotateLeft;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateLeft * rotationSpeed * Time.deltaTime);
    }
}
