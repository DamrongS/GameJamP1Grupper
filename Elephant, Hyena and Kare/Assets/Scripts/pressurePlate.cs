using Unity.VisualScripting;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    private bool hasInteracted = false;

    public Transform objectToMove;
    public float moveDistance;
    public float pressurePlateMoveDis;
    private Vector3 originalPosition;
    public float MoveTimescale = 1;
    public AudioSource audioSource;
    public AudioClip[] audioClip;

    Vector3 currentVelocity;

    void Update()
    {
        float dt = Time.deltaTime;
        if (hasInteracted)
        {

            objectToMove.transform.position = Vector2.Lerp(objectToMove.transform.position, originalPosition + new Vector3(0, moveDistance, 0), dt * MoveTimescale);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Hyeana") && !hasInteracted)
        {
            //objectToMove.Translate(Vector2.up * moveDistance * Time.deltaTime);

            //transform.Translate(Vector2.up * pressurePlateMoveDis * Time.deltaTime);
            audioSource = gameObject.GetComponent<AudioSource>();
            originalPosition = objectToMove.transform.position;
            for(int i = 0; i < audioClip.Length; i++){
                audioSource.PlayOneShot(audioClip[i], 1);
            }
            hasInteracted = true;
        }


    }

}