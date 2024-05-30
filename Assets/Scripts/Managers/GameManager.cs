using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    public enum GameStates
    {
        MainMenu,
        Playing,
        GameOver
    }

    public GameStates CurrentState { get; private set; }
    public UnityEvent<GameStates> OnGameStateChange = new UnityEvent<GameStates>();
    public GameObject levelManagerPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        levelManagerPrefab = Resources.Load(Const.PATH_TO_MANAGERS_FOLDER + Const.LEVEL_MANAGER_NAME) as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(levelManagerPrefab);
        ChangeGameState(GameStates.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == GameStates.MainMenu && Input.GetButtonDown(Const.RUN_AXIS_NAME))
            ChangeGameState(GameStates.Playing);
    }

    public void ChangeGameState(GameStates newState)
    {
        CurrentState = newState;
        OnGameStateChange.Invoke(CurrentState);
        Debug.Log($"GAME CHANGED STATE TO {CurrentState}");
    }
}
