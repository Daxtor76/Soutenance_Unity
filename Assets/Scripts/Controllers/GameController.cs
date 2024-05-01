using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Character Character { get; private set; }
    public GameObject characterPrefab;
    public Transform spawnPoint;
    private void Awake()
    {
        Character = new Character(Instantiate(characterPrefab, new Vector3(spawnPoint.position.x, 0.1f, spawnPoint.position.z), Quaternion.identity));
    }
}