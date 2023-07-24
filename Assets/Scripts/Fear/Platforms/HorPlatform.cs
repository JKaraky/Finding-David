using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorPlatform : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] Vector2 positionOne;
    [SerializeField] Vector2 positionTwo;

    float timeElapsed;

    int direction = 1;

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed < duration)
        {
            Vector2 target = currentMovementTarget();

            transform.position = Vector2.Lerp(target * -1, target, timeElapsed/duration);

            timeElapsed += Time.deltaTime;

            Debug.Log(Vector2.Lerp(transform.position, target, timeElapsed / duration));
        }
        else
        {
            timeElapsed = 0;

            direction *= -1;
        }
    }

    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return positionOne;
        }
        else
        {
            return positionTwo;
        }
    }
}
