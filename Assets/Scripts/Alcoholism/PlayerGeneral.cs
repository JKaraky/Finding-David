using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    public GameObject head;
    CircleCollider2D headCollider;
    // Start is called before the first frame update
    void Awake()
    {
        headCollider = head.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Noose"))
        {
            GameManager.gameManagerInstance.EndGame();
        }
    }
}
