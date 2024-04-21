using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    IMover mover;
    float speed;
    GameObject go;
    public IMover Mover => mover;
    public float Speed => speed;
    public GameObject Go => go;

    public Character(IMover mover, GameObject go, float speed)
    {
        this.mover = mover;
        this.go = go;
        this.speed = speed;
    }

    public void SetMover(IMover pMover)
    {
        mover = pMover;
    }

    public void SetGo(GameObject pGameObject)
    {
        go = pGameObject;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
}
