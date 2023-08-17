using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePull : MonoBehaviour
{
    public Collider2D[] collidersToAffect;

    public float effectRadius = 0;

    public float gravityForce = 0.1f;

    public Color gizmoColor = Color.red;

    public LayerMask layers;

    // Update is called once per frame
    void Update()
    {

        collidersToAffect = Physics2D.OverlapCircleAll(transform.position, effectRadius, layers);
        

        foreach (Collider2D c in collidersToAffect)
        {

            Vector2 attractionDir = transform.position - c.transform.position;

            c.attachedRigidbody.AddForce(attractionDir * gravityForce, ForceMode2D.Force);

        }

    }

    void OnDrawGizmos()
    {

        Gizmos.color = gizmoColor;

        Gizmos.DrawWireSphere(transform.position, effectRadius);

    }
}
