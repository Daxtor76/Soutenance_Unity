using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    private static LevelController _instance = null;
    public static LevelController Instance => _instance;
    
    private Transform tilesContainer;
    private List<GameObject> tilesModels = new List<GameObject>();
    public List<GameObject> spawnedTiles = new List<GameObject>();
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        
        LoadTiles();
        tilesContainer = GetTilesContainer();
        BuildInitialLevelTiles();
    }

    private void LoadTiles()
    {
        Object[] tiles = Resources.LoadAll(Const.PATH_TO_TILES_FOLDER, typeof(GameObject));
        foreach (Object obj in tiles)
            tilesModels.Add(obj as GameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AddTile(tilesModels[Random.Range(1, tilesModels.Count)]);
    }

    private void BuildInitialLevelTiles()
    {
        AddTile(tilesModels[0]);
    }

    private void AddTile(GameObject tileToAdd)
    {
        Vector3 spawnPosition = new Vector3(
            0.0f,
            0.0f,
            spawnedTiles.Count > 0
                ? spawnedTiles.Last().transform.position.z + spawnedTiles.Last().GetComponent<Tile>().size.z
                : Vector3.zero.z
        );
        
        spawnedTiles.Add(Instantiate(tileToAdd, spawnPosition, Quaternion.identity, tilesContainer));
    }

    private Transform GetTilesContainer()
    {
        return GameObject.Find("TilesContainer").transform;
    }
}
