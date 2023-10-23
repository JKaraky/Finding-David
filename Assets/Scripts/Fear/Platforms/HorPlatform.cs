using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorPlatform : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] Vector2 positionOne;
    [SerializeField] Vector2 positionTwo;

    float timeElapsed=0;

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed < duration)
        {
            transform.position = Vector2.Lerp(positionOne, positionTwo, timeElapsed / duration);

            timeElapsed += Time.deltaTime;
        }
        else
        {
            FlipPositions();
            timeElapsed = 0;
        }
    }

    void FlipPositions()
    {
        Vector2 temp = positionOne;
        positionOne = positionTwo;
        positionTwo = temp;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
