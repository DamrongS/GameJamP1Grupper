using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D _rigidbody2d;
    SpriteRenderer _spriteRenderer;
    SpriteRenderer _spriteSignifier;
    InputAction _move;
    Animator _animator;
    public string movementType;
    public int movementSpeed;
    public float jumpPower;
    public bool onGround;
    public bool jumping = false;
    public bool canJump;
    public bool showSignifier = true;
    public bool isMoving;
    public float showSignifierAfter;

    private float signifierTimer = 0;
    private bool touchingPlatform = false;
    private bool allowMovement = true;

    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteSignifier = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _move = InputSystem.actions.FindAction(movementType);
        onGround = false;
        _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Elephant") || collision.CompareTag("Hyeana")) return;

        if(collision.CompareTag("Ground"))
        {
            onGround = true;
        }

        if (collision.CompareTag("Platform") && movementType != "ElephantMovement")
        {
            transform.SetParent(collision.transform);
            touchingPlatform = true;
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if((collision.CompareTag("Elephant") || collision.CompareTag("Hyeana")) && touchingPlatform) return;

        if(collision.CompareTag("Ground"))
        {
            onGround = false;
        }

        if (collision.CompareTag("Platform") && movementType != "ElephantMovement")
        { 
            transform.SetParent(null);
            touchingPlatform = false;
            onGround = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Water") && this.gameObject.CompareTag("Hyeana"))
        {
            
        }
    }
    
    void FixedUpdate()
    {
        float dt = Time.deltaTime;
        Vector2 moving = _move.ReadValue<Vector2>();

        bool wantsToJump = moving.y > 0;

        _rigidbody2d.linearVelocityX = moving.x * movementSpeed * dt;

        if (wantsToJump && onGround && !jumping && canJump)
        {
            jumping = true;
            _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
        else if (!wantsToJump)
        {
            jumping = false;
        }

        isMoving = math.abs(moving.x) > 0;

        _animator.SetBool("isWalking", isMoving);
        _animator.SetBool("isJumping", _rigidbody2d.linearVelocityY > 0);
        _animator.SetBool("isFalling", _rigidbody2d.linearVelocityY < 0 && !touchingPlatform);

        if (isMoving)
        {
            showSignifier = false;
            signifierTimer = 0;
            _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        } else if(!isMoving && transform.parent && transform.parent.tag == "Platform")
        {
            onGround = true;
            jumping = false;
            _rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        }

        if (moving.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (moving.x < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if(touchingPlatform && !isMoving)
        {
            _rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        } 
        else
        {
            _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        }
        // Debug.Log($"velocityY: {_rigidbody2d.linearVelocityY}");
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if (showSignifier)
        {
            _spriteSignifier.enabled = true;
            signifierTimer = 0;
        }
        else
        {
            _spriteSignifier.enabled = false;
            signifierTimer += dt;
        }

        if (signifierTimer > showSignifierAfter)
        {
            showSignifier = true;
        }
    }
}