using UnityEngine;
using Unity.Cinemachine;

public class HeavyObjectCamerashake : MonoBehaviour
{
    private Camera mainCamera;
    
    public CinemachineImpulseSource impulseSource;
    
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        
        CameraShake();
    }

    void Update()
    {
         
    }

    public void CameraShake()
    {
        impulseSource.GenerateImpulse();
    }
}