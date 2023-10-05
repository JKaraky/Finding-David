using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public float offset;
    private Collider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerLocation = player.position;
        transform.position = playerLocation + new Vector3(0, offset, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, boxCollider);
        }
    }
}
