using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private Transform tilesContainer;
    private List<GameObject> tilesModels = new List<GameObject>();
    [SerializeField]private List<GameObject> spawnedTiles = new List<GameObject>();
    private void Awake()
    {
        LoadTiles();
        tilesContainer = GetTilesContainer();
    }

    private void Start()
    {
        BuildInitialTiles();
        CreateCharacter();
    }

    private void CreateCharacter()
    {
        GameObject characterPrefab = Resources.Load(Const.PATH_TO_CHARACTER_FOLDER + Const.CHARACTER_NAME) as GameObject;
        Transform spawnPoint = spawnedTiles.First().transform;
        
        GameObject characterGo = Instantiate(characterPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y + 0.1f, spawnPoint.position.z + 2.0f), Quaternion.identity);
    }

    private void LoadTiles()
    {
        Object[] tiles = Resources.LoadAll(Const.PATH_TO_TILES_FOLDER, typeof(GameObject));
        foreach (Object obj in tiles)
            tilesModels.Add(obj as GameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AddTile(tilesModels[Random.Range(1, tilesModels.Count)]);
            RemoveTile();
        }
    }

    private void BuildInitialTiles()
    {
        AddTile(tilesModels[0]);
        AddTile(tilesModels[3]);
        AddTile(tilesModels[3]);
        AddTile(tilesModels[3]);
        AddTile(tilesModels[3]);
        /*AddTile(tilesModels[Random.Range(1, tilesModels.Count)]);
        AddTile(tilesModels[Random.Range(1, tilesModels.Count)]);
        AddTile(tilesModels[Random.Range(1, tilesModels.Count)]);
        AddTile(tilesModels[Random.Range(1, tilesModels.Count)]);*/
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

    private void RemoveTile()
    {
        GameObject tileToRemove = spawnedTiles[0];
        spawnedTiles.RemoveAt(0);
        Destroy(tileToRemove);
    }

    private Transform GetTilesContainer()
    {
        return GameObject.Find("TilesContainer").transform;
    }
}