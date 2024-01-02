using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacesMove : MonoBehaviour
{
    #region Variables

    private Vector3 originalPosition;

    private GameObject player;

    private Animator animator;

    private bool engaged = false;

    [SerializeField] private float speed;

    public static event Action Hitplayer;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (engaged)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
    }

    #region Collision Collider Behavior

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hitplayer?.Invoke();
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region Triggered Collider Behavior
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            engaged = true;
            animator.SetBool("Engaged", true);
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            engaged = false;
            animator.SetBool("Engaged", false);
        }
    }
    #endregion
}
