using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyubiFX : IEffect
{
    GameObject effectObject;

    public KyubiFX(GameObject fxObj)
    {
        effectObject = fxObj;
    }

    public void Trigger()
    {
        foreach (Transform obj in effectObject.GetComponentInChildren<Transform>(true))
            obj.gameObject.SetActive(true);
    }

    public void Disable()
    {
        foreach (Transform obj in effectObject.GetComponentInChildren<Transform>(true))
            obj.gameObject.SetActive(false);
    }
}
public class DeathFX : IEffect
{
    GameObject effectObject;

    public DeathFX(GameObject fxObj)
    {
        effectObject = fxObj;
    }

    public void Trigger()
    {
        foreach (Transform obj in effectObject.GetComponentInChildren<Transform>(true))
            obj.gameObject.SetActive(true);
    }

    public void Disable()
    {
        foreach (Transform obj in effectObject.GetComponentInChildren<Transform>(true))
            obj.gameObject.SetActive(false);
    }
}
