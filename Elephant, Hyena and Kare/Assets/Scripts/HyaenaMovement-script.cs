using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class HyaenaMovement : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private InputAction movement;
    public float movementSpeed = 10;
    public float jumpSpeed = 10;
    public float stopJump = 0;
    bool onGround;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        movement = InputSystem.actions.FindAction("HyaenaMovement");
    }
    private Rigidbody2D _rigidbody2D;

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }




    }




    private void OnTriggerExit2D(Collider2D collision2D)
    {
        onGround = false;
    }

    void FixedUpdate()
    {
        Vector2 moving = movement.ReadValue<Vector2>();
        _rigidbody2D.linearVelocityX = moving.x * movementSpeed;

        Vector2 jumping = movement.ReadValue<Vector2>();
        if (onGround == true)
        {
            _rigidbody2D.linearVelocityY = jumping.y * jumpSpeed;
        }
        else if (onGround == false)
        {

        }

        _rigidbody2D.AddForce(new Vector2(0, 0), ForceMode2D.Force); //Pushes
        if (moving.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (moving.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}