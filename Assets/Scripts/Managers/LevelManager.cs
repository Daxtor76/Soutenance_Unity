using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private Actor _character;
    private Camera _camera;
    private Transform _tilesContainer;
    private List<GameObject> _tilesModels = new List<GameObject>();
    [SerializeField]private List<GameObject> spawnedTiles = new List<GameObject>();
    private void Awake()
    {
        LoadTiles();
        _tilesContainer = GetTilesContainer();
    }

    private void Start()
    {
        BuildInitialTiles();
        _character = CreateCharacter();
        _character.CollisionController.OnCollisionWithTileSpawner.AddListener(BuildPathTile);

        _camera = CreateCamera();
    }

    private void BuildPathTile()
    {
        AddTile(_tilesModels[Random.Range(1, _tilesModels.Count)]);
        RemoveFirstTile();
    }

    private Character CreateCharacter()
    {
        GameObject characterPrefab = Resources.Load(Const.PATH_TO_CHARACTER_FOLDER + Const.CHARACTER_NAME) as GameObject;
        Transform spawnPoint = spawnedTiles.First().transform;
        
        GameObject chara = Instantiate(characterPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y + 0.05f, spawnPoint.position.z + 2.0f), Quaternion.identity);
        chara.name = Const.CHARACTER_NAME;
        return chara.GetComponent<Character>();
    }

    private Camera CreateCamera()
    {
        GameObject camPrefab = Resources.Load(Const.PATH_TO_CAMERA_FOLDER + Const.CAMERA_NAME) as GameObject;
        Transform spawnPoint = spawnedTiles.First().transform;
        
        GameObject cam = Instantiate(camPrefab, new Vector3(spawnPoint.position.x + 0.3f, spawnPoint.position.y + 0.75f, spawnPoint.position.z + 0.75f), Quaternion.identity);
        cam.name = Const.CAMERA_NAME;
        return cam.GetComponent<Camera>();
    }

    private void LoadTiles()
    {
        Object[] tiles = Resources.LoadAll(Const.PATH_TO_TILES_FOLDER, typeof(GameObject));
        foreach (Object obj in tiles)
            _tilesModels.Add(obj as GameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AddTile(_tilesModels[Random.Range(1, _tilesModels.Count)]);
            RemoveFirstTile();
        }
    }

    private void BuildInitialTiles()
    {
        AddTile(_tilesModels[0]);
        AddTile(_tilesModels[Random.Range(1, _tilesModels.Count)]);
        AddTile(_tilesModels[Random.Range(1, _tilesModels.Count)]);
        AddTile(_tilesModels[Random.Range(1, _tilesModels.Count)]);
        AddTile(_tilesModels[Random.Range(1, _tilesModels.Count)]);
    }

    private void AddTile(GameObject tileToAdd)
    {
        /*Vector3 spawnPositionfrgegr = new Vector3(
            0.0f,
            0.0f,
            spawnedTiles.Count > 0
                ? spawnedTiles.Last().transform.position.z + spawnedTiles.Last().GetComponent<Tile>().size.z
                : Vector3.zero.z
        );*/

        Transform spawnTransform;
        if (spawnedTiles.Count > 0)
            spawnTransform = spawnedTiles.Last().GetComponent<Tile>().nextSpawnDummy;
        else
            spawnTransform = null;
        
        spawnedTiles.Add(Instantiate(
            tileToAdd, 
            spawnTransform ? spawnTransform.position : Vector3.zero, 
            spawnTransform ? spawnTransform.rotation : Quaternion.identity, 
            _tilesContainer
        ));
    }

    private void RemoveFirstTile()
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
