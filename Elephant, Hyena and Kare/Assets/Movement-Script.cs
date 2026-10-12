using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class ElephantMovement : MonoBehaviour
{
    private InputAction movement;
    public float movementSpeed = 10;
    public float jumpSpeed = 10;
    public float stopJump = 0;
    bool onGround;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        movement = InputSystem.actions.FindAction("Movement");
    }
    private Rigidbody2D _rigidbody2D;

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 moving = movement.ReadValue<Vector2>();
        _rigidbody2D.linearVelocityX = moving.x * movementSpeed;
        bool isInteracting = movement.IsPressed();
        
        Vector2 jumping = movement.ReadValue<Vector2>();
        if (onGround == true)
        {
            _rigidbody2D.linearVelocityY = moving.y * jumpSpeed;
        }
        else if (onGround == false)
        {
            
        }
    }
}
