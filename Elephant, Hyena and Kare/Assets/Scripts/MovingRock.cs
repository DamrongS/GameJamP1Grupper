using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRock : MonoBehaviour
{

    

   
    Vector3 currentVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Hyeana"))
        {
            transform.Translate(transform.up * -5f * Time.deltaTime);
        }
       

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
