using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D _rigidbody2d;
    SpriteRenderer _spriteRenderer;
    InputAction _move;
    Animator _animator;
    public string movementType;
    public int movementSpeed;
    public int jumpPower;
    public bool onGround;
    public bool jumping = false;
    public bool canJump;
    
    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _move = InputSystem.actions.FindAction(movementType);
        onGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        if (collision.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onGround = false;
        if (collision.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.deltaTime;
        Vector2 moving = _move.ReadValue<Vector2>();

        bool wantsToJump = moving.y > 0;

        _rigidbody2d.linearVelocityX = moving.x * movementSpeed * dt;

        if (wantsToJump && onGround && !jumping && canJump)
        {
            jumping = true;
            _rigidbody2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
        else if (!wantsToJump)
        {
            jumping = false;
        }

        bool isMoving = math.abs(moving.x) > 0;

        _animator.SetBool("isWalking", isMoving);

        if(isMoving)
        {
            _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        } else if(!isMoving && transform.parent.tag == "Platform")
        {
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
    }
}