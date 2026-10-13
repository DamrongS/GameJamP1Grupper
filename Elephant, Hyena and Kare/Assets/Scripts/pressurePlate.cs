using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    private bool hasInteracted = false;

    public Transform objectToMove;
    public float moveDistance;
    public float pressurePlateMoveDis;

    Vector3 currentVelocity;


    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Hyeana") && !hasInteracted)
        {
            objectToMove.Translate(transform.up * moveDistance * Time.deltaTime);

            transform.Translate(transform.up * pressurePlateMoveDis * Time.deltaTime);
            hasInteracted = true;
            Debug.Log(hasInteracted);
        }


    }

}