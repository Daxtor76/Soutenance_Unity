using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 size;

    private void Awake()
    {
        size = GetTileSize();
    }

    private GameObject GetTileMesh()
    {
        return transform.Find("Mesh").gameObject;
    }

    private Vector3 GetTileSize()
    {
        return GetTileMesh().transform.localScale;
    }
}
