using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 size;
    public Transform nextSpawnDummy;

    private void Awake()
    {
        size = GetTileSize();
        nextSpawnDummy = GetDummy();
    }

    private Transform GetDummy()
    {
        return transform.Find(Const.TILE_NEXT_SPAWN_DUMMY);
    }

    private Vector3 GetTileSize()
    {
        return GetTileMesh().transform.localScale;
    }

    private GameObject GetTileMesh()
    {
        return transform.Find("Mesh").gameObject;
    }
}
