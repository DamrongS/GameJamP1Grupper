// Expose center of mass to allow it to be set from
// the inspector.
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    public Vector2 com;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = com;
    }
}
