using System;
using System.Collections;
using UnityEngine;

public class jumptrough : MonoBehaviour
{
    public bool enabeld = true;

    private void  OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("it detected sum");
        enabeld = false;
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag != "Hyaena") // så that the Hyaena dosen't disapper when the platform is disabled
            {
                child.gameObject.SetActive(enabeld);
            }

            if (child.tag != "Elephant") // -//-
            {
                child.gameObject.SetActive(enabeld);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enabeld = true; // makes the platform reapper
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(enabeld);
        }
    }
}
