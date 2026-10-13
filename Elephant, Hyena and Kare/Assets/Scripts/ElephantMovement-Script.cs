using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class ElephantMovement : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private InputAction movement;
    private Animator _animator;
    public float movementSpeed = 10;
    public float jumpSpeed = 10;
    public float stopJump = 0;
    bool onGround;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        movement = InputSystem.actions.FindAction("ElephantMovement");
    }
    private Rigidbody2D _rigidbody2D;

    private void OnTriggerEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        else if (collision2D.gameObject.CompareTag("Hyeana"))
        {
            onGround = true;
        }

    }
    
    private void OnTriggerExit2D(Collision2D collision2D)
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
        _rigidbody2D.AddForce(new Vector2(5, 0), ForceMode2D.Force); //Pushes

        _animator.SetBool("isRunning", moving.magnitude > 1);

        if (moving.x < 0)
        {
            _spriteRenderer.flipX = true;
        } else if(moving.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

    }
}