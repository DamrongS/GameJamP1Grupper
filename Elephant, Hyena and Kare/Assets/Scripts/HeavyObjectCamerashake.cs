using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class HeavyObjectCamerashake : MonoBehaviour
{
    [Header("Shake Conditions")]
    public float minimumImpactSpeed;
    public float minimumSlideSpeed;
    public bool allowSlideRumble = false;
    public string impactTag;

    [Header("Shake Conditions")]
    public AudioClip groundHitSound;
    public AudioClip groundPushSound;
    public float volume = 0.3f;

    [Header("Shake Settings")]
    public CinemachineImpulseDefinition.ImpulseShapes impulseShape;
    public float shakeDuration = 0.4f;
    public Vector3 shakeIntensity = new Vector3(0, 0.1f, 0);

    private CinemachineImpulseSource impulseSource;
    private AudioSource audioSource;
    private IEnumerator coroutine;
    private bool isSliding = false;

    [SerializeField]
    private AnimationCurve customCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.2f, -0.3f), new Keyframe(0.45f, 1f), new Keyframe(1f, 0f));

    void Start()
    {
        if (impulseSource == null)
        {
            impulseSource = gameObject.AddComponent<CinemachineImpulseSource>();
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        impulseSource.ImpulseDefinition.ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Uniform;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(impactTag))
        {
            float impactSpeed = collision.relativeVelocity.magnitude;

            Debug.Log(impactSpeed);

            if (impactSpeed >= minimumImpactSpeed)
            {
                CameraShake();
                audioSource.clip = groundHitSound;
                audioSource.loop = false;
                audioSource.Play();
            }
        }

        if (collision.gameObject.CompareTag("Elephant") && allowSlideRumble)
        {
            Debug.Log("Reached Slide Logic");
            
            float impactSpeed = collision.relativeVelocity.magnitude;
        
            Debug.Log(impactSpeed);
        
            if (impactSpeed >= minimumSlideSpeed)
            {
                Debug.Log("Reached SlideCameraShake");
                isSliding = true;
                audioSource.clip = groundPushSound;
                audioSource.volume = 0.3f;
                audioSource.loop = true;
                audioSource.Play();
                StartCoroutine(SlideShake());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Elephant"))
        {
            isSliding = false;
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("slideOffTrigger"))
        {
            allowSlideRumble = false;
        }
    }

    public void CameraShake()
    {
        impulseSource.ImpulseDefinition.ImpulseShape = impulseShape;
        // Sendes hvis man har brugt en custom shape
        if (impulseShape == CinemachineImpulseDefinition.ImpulseShapes.Custom)
        {
            impulseSource.ImpulseDefinition.CustomImpulseShape = customCurve;
        }
        
        impulseSource.ImpulseDefinition.ImpulseDuration = shakeDuration;
        impulseSource.DefaultVelocity = shakeIntensity;
        
        impulseSource.GenerateImpulse();
    }

    IEnumerator SlideShake()
    {
        while (isSliding)
        {
            yield return new WaitForSeconds(0.2f);
            
            impulseSource.ImpulseDefinition.ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Rumble;
            impulseSource.ImpulseDefinition.ImpulseDuration = 0.2f;
            impulseSource.DefaultVelocity = shakeIntensity;
            
            impulseSource.GenerateImpulse();
        }
    }
}