using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swivel : MonoBehaviour
{

    public Vector3 start;
    public Vector3 end;
    public float duration;

    float centerOffset = 0.1f;
    float timeElapsed;
    bool reverse = false;

    private void Update()
    {
        var centerPivot = (start + end) * 0.5f;
        centerPivot -= new Vector3(0, centerOffset);

        var startRelPosition = start - centerPivot;
        var endRelPosition = end - centerPivot;

        if (timeElapsed < duration)
        {
            if(!reverse)
            {
                transform.position = Vector3.Slerp(startRelPosition, endRelPosition, timeElapsed / duration);

                timeElapsed += Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.Slerp(endRelPosition, startRelPosition, timeElapsed / duration);

                timeElapsed += Time.deltaTime;
            }

        }
        else
        {
            timeElapsed = 0;

            reverse = !reverse;
        }
    }
}
