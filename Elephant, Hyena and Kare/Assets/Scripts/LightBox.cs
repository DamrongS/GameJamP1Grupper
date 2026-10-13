using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
public class LightPlatform : MonoBehaviour
{
    public bool enabeld = true; 
    void Start()
    {
        enabeld = true;
    }
        private void OnTriggerEnter2D(Collider2D collision) 
        {
            Debug.Log("it detected sum");
            if (collision.gameObject.CompareTag("Elephant"))
            {
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
                // a wait for the platform reappering
                {
                    StartCoroutine(WaitForFunction());
                }
                IEnumerator WaitForFunction()
                {
                    yield return new WaitForSeconds(3);
                
                    enabeld = true; // makes the platform reapper
                    foreach (Transform child in gameObject.transform)
                    {
                        child.gameObject.SetActive(enabeld);
                    }
                }
            }
            
          
        }
        

}
