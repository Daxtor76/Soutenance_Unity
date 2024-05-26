using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireLightShaker : MonoBehaviour
{
    private Light pointLight;
    private float timer = 0.0f;
    private float maxTimer = 3.5f;
    private float minIntensity;
    public float maxIntensity;
    private float minRange;
    public float maxRange;
    private AnimationCurve curve = new AnimationCurve(
            new Keyframe(0.0f, 0.0f),
            new Keyframe(1.5f, 1.0f),
            new Keyframe(2.0f, 0.75f),
            new Keyframe(2.5f, 1.0f),
            new Keyframe(3.5f, 0.0f));

    private void Awake()
    {
        pointLight = GetLight();
        timer = Random.Range(0.0f, maxTimer);
        minIntensity = pointLight.intensity;
        minRange = pointLight.range;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTimer)
            timer = 0.0f;
        
        pointLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, curve.Evaluate(timer));
        pointLight.range = Mathf.Lerp(minRange, maxRange, curve.Evaluate(timer));
    }

    private Light GetLight()
    {
        return GetComponent<Light>();
    }
}
