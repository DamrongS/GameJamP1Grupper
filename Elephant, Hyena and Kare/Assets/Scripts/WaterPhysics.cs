using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.U2D;


public class WaterPhysics : MonoBehaviour
{

    private float velocity = 0;
    private float force = 0;
    private float height = 0f;
    private float targetHeight = 0f;
    private float springStiffnes = 0.1f;
    public float spread = 0.006f;
    [SerializeField]
    private float dampening = 0.03f;
    private int cornersCount = 2;   
    [SerializeField]
    private SpriteShapeController spriteShapeController;
    [SerializeField]
    private int wavesCount = 10;
    private void SetWaves()
    {
        Spline waterSpline = spriteShapeController.spline;
        int waterPointsCount = waterSpline.GetPointCount();
        for (int i = cornersCount; i < waterPointsCount - cornersCount; i++)
        {
            waterSpline.RemovePointAt(cornersCount);
    
        }
        Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
        Vector3 waterTopRightCorner = waterSpline.GetPosition(2);
        float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x;
        float spacingPerWave = waterWidth / (wavesCount+1);
        for (int i = wavesCount; i > 0; i--)
        {
            int index = cornersCount;

            float xPosition = waterTopLeftCorner.x + (spacingPerWave * i);
            Vector3 wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
            waterSpline.InsertPointAt(index, wavePoint);
            waterSpline.SetHeight(index, 0.1f);
            waterSpline.SetCorner(index, false);
        }
 
    }

    private void Update()
    {
        SetWaves();
        
    }

    private List<WaterPhysics> springs = new();



    public void WaveSpringUpdate(float springStiffness)
    {
        
        height = transform.localPosition.y;
        var x = height - targetHeight;
        var loss =  -dampening * velocity;
        
        force = - springStiffness * x + loss;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y + velocity, transform.localPosition.z);
   
    }

    private void Splash(int index, float speed)
    {
        if (index >= 0 && index < springs.Count)
        {
            springs[index].velocity += speed;
        }

    }


    private void FixedUpdate()
    {
        foreach (WaterPhysics waterSpringComponent in springs)
        {
            waterSpringComponent.WaveSpringUpdate(springStiffnes);
        }
        UpdateSprings();
    }

    private void UpdateSprings()
    {
        int count = springs.Count;
        float[] left_deltas = new float[count];
        float[] right_deltas = new float[count];
        for (int i = 0; i < count; i++)
        {
            if (i > 0)
            {
                left_deltas[i] = spread * (springs[i].height - springs[i - 1].height);
                springs[i - 1].velocity += left_deltas[i];
            }
            if (i < springs.Count - 1)
            {
                right_deltas[i] = spread * (springs[i].height - springs[i - 1].height);
                springs[i + 1].velocity += right_deltas[i];
            }
        }


    }

    

}
