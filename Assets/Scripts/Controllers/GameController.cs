using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance = null;
    public static GameController Instance => _instance;

    public Character Character { get; private set; }
    public GameObject characterPrefab;
    public Transform spawnPoint;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
		
        Character = CreateCharacter();
    }

    private Character CreateCharacter()
    {
        GameObject characterGo = Instantiate(characterPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 2.0f), Quaternion.identity);
        return characterGo.AddComponent<Character>();
    }
}