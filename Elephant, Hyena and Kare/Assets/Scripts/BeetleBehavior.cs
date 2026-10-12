using System.Diagnostics;
using NUnit.Framework.Constraints;
using UnityEngine;

public class BeetleBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D _rigidBody2d;
    private SpriteRenderer _spriteRenderer;
    private float timer = 0;

    void Start()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        timer += dt;
        if (timer < Random.Range(0f, 2000f) / 100f) return;
        timer = 0;

        float directionX = Random.Range(1f, 100f) / 100f;
        _rigidBody2d.linearVelocityX = directionX;
        
    }
}
