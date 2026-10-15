using System;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    [Header("Water Settings")]
    public float pushForce = 3f;
    public float drownForce = 2f;

    // public SpriteRenderer hyenaSR;

    private BoxCollider2D watercol2d;

    private void Start()
    {
        watercol2d = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Elephant"))
        {
            Debug.Log("lephant entered Water!");
            
            Rigidbody2D rB2D = collider.attachedRigidbody;

            float waterSurface = watercol2d.bounds.max.y;

            if (collider.bounds.center.y < waterSurface)
            {
                Debug.Log("Floating elephant!");
                rB2D.linearVelocity = (new Vector2(rB2D.linearVelocityX, pushForce));
            }
        } else if (collider.gameObject.CompareTag("Hyeana"))
        {
            Debug.Log("Hyena entered Water!");
            
            Rigidbody2D rB2D = collider.attachedRigidbody;
            // Color orignalColor = hyenaSR.color;
            
            float waterSurface = watercol2d.bounds.max.y;

            if (collider.bounds.center.y < waterSurface)
            {
                Debug.Log("Floating hyena!");
                rB2D.linearVelocity = (new Vector2(rB2D.linearVelocityX, -drownForce));
            }
        }
    }
}
