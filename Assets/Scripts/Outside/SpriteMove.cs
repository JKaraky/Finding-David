using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMove : MonoBehaviour
{
    Vector3 startPosition;
    public Camera cam;
    public float modifier;
    float width;
    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main;
        startPosition = transform.position;
    }
    void Update()
    {
        Vector3 camOffset = cam.transform.position * modifier;
        transform.position = startPosition + new Vector3(camOffset.x, camOffset.y, 0);
        if ((cam.transform.position.x - transform.position.x) > width)
        {
            startPosition += new Vector3(width, 0, 0);
        }
        if ((cam.transform.position.x - transform.position.x) < -width)
        {
            startPosition -= new Vector3(width, 0, 0);
        }
    }
}
