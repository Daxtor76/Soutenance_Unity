using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance = null;
    public static GameController Instance => _instance;

    public Character Character { get; private set; }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        
        CreateLevelController();
        Character = CreateCharacter();
    }

    private Character CreateCharacter()
    {
        GameObject characterPrefab = Resources.Load(Const.PATH_TO_CHARACTER_FOLDER + Const.CHARACTER_NAME) as GameObject;
        Transform spawnPoint = LevelController.Instance.spawnedTiles.First().transform;
        
        GameObject characterGo = Instantiate(characterPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 2.0f), Quaternion.identity);
        return characterGo.AddComponent<Character>();
    }

    private void CreateLevelController()
    {
        GameObject levelController = Resources.Load(Const.PATH_TO_CONTROLLERS_FOLDER + Const.LEVEL_CONTROLLER_NAME) as GameObject;
        Instantiate(levelController);
    }
}