using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
    private static LevelController _instance = null;
    public static LevelController Instance => _instance;
    private GameObject tilesContainer;
    public  Queue<GameObject> tiles { get; private set; }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        //DontDestroyOnLoad(this.gameObject);
        tilesContainer = GetTilesContainer();
        tiles = BuildInitialLevelTiles();
        
        foreach (GameObject go in tiles)
            Debug.Log(go.name);
    }

    private Queue<GameObject> BuildInitialLevelTiles()
    {
        Queue<GameObject> tiles = new Queue<GameObject>();
        tiles.Enqueue(Resources.Load(Const.PATH_TO_TILES_FOLDER + Const.INITIAL_TILE_NAME) as GameObject);
        tiles.Enqueue(Resources.Load(Const.PATH_TO_TILES_FOLDER + Const.COMMON_TILE_NAME) as GameObject);
        tiles.Enqueue(Resources.Load(Const.PATH_TO_TILES_FOLDER + Const.COMMON_TILE_NAME) as GameObject);
        tiles.Enqueue(Resources.Load(Const.PATH_TO_TILES_FOLDER + Const.COMMON_TILE_NAME) as GameObject);
        tiles.Enqueue(Resources.Load(Const.PATH_TO_TILES_FOLDER + Const.COMMON_TILE_NAME) as GameObject);

        return tiles;
    }

    private GameObject GetTilesContainer()
    {
        return GameObject.Find("TilesContainer");
    }
}
