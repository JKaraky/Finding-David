using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float camerSpeed = 0.1f;
    public float offset = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 finalPosition = player.position + new Vector3(0, 1 + offset, -5);

        Vector3 lerpPosition = Vector3.Lerp(transform.position, finalPosition, camerSpeed);

        transform.position = lerpPosition;
    }
}
