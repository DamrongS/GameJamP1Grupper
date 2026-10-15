using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour
{
    [Header("Water Settings")]
    public float pushForce = 3f;
    public float drownForce = 2f;
    public float drownTimeElephant = 5f;
    public float drownTimeHyena = 1f;
    
    private Coroutine drownCoroutineE;
    private Coroutine drownCoroutineH;
    private BoxCollider2D watercol2d;
    private bool willDrownElephant = false;
    private bool willDrownHyena = false;

    private void Start()
    {
        watercol2d = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Elephant"))
        {
            // Debug.Log("Elephant entered Water!");
            
            Rigidbody2D rB2D = collider.attachedRigidbody;

            float waterSurface = watercol2d.bounds.max.y;

            if (collider.bounds.center.y < waterSurface && !willDrownElephant)
            {
                // Debug.Log("Floating elephant!");
                rB2D.linearVelocity = (new Vector2(rB2D.linearVelocityX, pushForce));
            } else if (collider.bounds.center.y < waterSurface && willDrownElephant)
            {
                Debug.Log("Drowning elephant!");
                rB2D.linearVelocity = (new Vector2(rB2D.linearVelocityX, -drownForce));
            }
        } else if (collider.gameObject.CompareTag("Hyeana"))
        {
            // Debug.Log("Hyena entered Water!");
            
            Rigidbody2D rB2D = collider.attachedRigidbody;
            
            float waterSurface = watercol2d.bounds.max.y;

            if (collider.bounds.center.y < waterSurface && !willDrownHyena)
            {
                // Debug.Log("Floating hyena!");
                rB2D.linearVelocity = (new Vector2(rB2D.linearVelocityX, pushForce));
            } else if (collider.bounds.center.y < waterSurface && willDrownHyena)
            {
                Debug.Log("Drowning hyena!");
                rB2D.linearVelocity = (new Vector2(rB2D.linearVelocityX, -drownForce));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Elephant"))
        {
            drownCoroutineE = StartCoroutine(DrownTimerElephant(drownTimeElephant));
        } else if (col.gameObject.CompareTag("Hyeana"))
        {
            drownCoroutineH = StartCoroutine(DrownTimerHyena(drownTimeHyena));
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Elephant"))
        {
            if (drownCoroutineE != null)
            {
                StopCoroutine(drownCoroutineE);
                drownCoroutineE = null;
            }

            willDrownElephant = false;
        }
        else if (col.gameObject.CompareTag("Hyeana"))
        {
            if (drownCoroutineH != null)
            {
                StopCoroutine(drownCoroutineH);
                drownCoroutineH = null;
            }

            willDrownHyena = false;
        }
    }

    IEnumerator DrownTimerElephant(float drownTime)
    {
        Debug.Log("Starting timer");
        yield return new WaitForSeconds(drownTime);

        willDrownElephant = true;
        Debug.Log($"Timer ended: {willDrownElephant}");
    }
    
    IEnumerator DrownTimerHyena(float drownTime)
    {
        Debug.Log("Starting timer");
        yield return new WaitForSeconds(drownTime);

        willDrownHyena = true;
        Debug.Log($"Timer ended: {willDrownHyena}");
    }
}