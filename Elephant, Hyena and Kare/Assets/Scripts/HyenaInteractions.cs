using UnityEngine;

public class HyenaInteractions : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    bool triggered;
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Hyeana"))
        {
            triggered = true;
            {
                _rigidbody2D.linearVelocity += new Vector2(0, -3);
            }
        }




    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
