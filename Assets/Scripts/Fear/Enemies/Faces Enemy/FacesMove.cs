using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacesMove : MonoBehaviour
{
    #region Variables

    private Vector3 originalPosition;

    private GameObject player;

    private bool engaged = false;

    [SerializeField] private float speed;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (engaged)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0,5,0), step);
        }
    }

    #region Collision Collider Behavior

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

    #endregion

    #region Triggered Collider Behavior
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            engaged = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            engaged = false;
        }
    }
    #endregion
}
