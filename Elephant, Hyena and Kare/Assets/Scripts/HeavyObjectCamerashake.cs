using UnityEngine;
using Unity.Cinemachine;

public class HeavyObjectCamerashake : MonoBehaviour
{
    [Header("Shake Conditions")]
    public float minimumImpactSpeed;
    public string impactTag;

    [Header("Shake Conditions")]
    public AudioClip groundHitSound;
    public float volume = 0.3f;

    [Header("Shake Settings")]
    public CinemachineImpulseDefinition.ImpulseShapes impulseShape;
    public float shakeDuration = 0.4f;
    public Vector3 shakeIntensity = new Vector3(0, 0.1f, 0);

    private CinemachineImpulseSource impulseSource;
    private AudioSource audioSource;

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
        if (!collision.gameObject.CompareTag(impactTag))
        {
            return;
        }

        float impactSpeed = collision.relativeVelocity.magnitude;

        Debug.Log(impactSpeed);

        if (impactSpeed >= minimumImpactSpeed)
        {
            CameraShake();
            audioSource.clip = groundHitSound;
            audioSource.Play();
        }
    }

    public void CameraShake()
    {
        impulseSource.ImpulseDefinition.ImpulseShape = impulseShape;

        impulseSource.ImpulseDefinition.ImpulseDuration = shakeDuration;

        // Sendes hvis man har brugt en custom shape
        if (impulseShape == CinemachineImpulseDefinition.ImpulseShapes.Custom)
        {
            impulseSource.ImpulseDefinition.CustomImpulseShape = customCurve;
        }

        impulseSource.DefaultVelocity = shakeIntensity;

        impulseSource.GenerateImpulse();
    }
}