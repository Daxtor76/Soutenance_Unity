using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameStates
    {
        MainMenu,
        Playing,
        GameOver
    }

    public GameStates CurrentState { get; private set; }

    private GameObject _levelManagerPrefab;
    private GameObject _uiManagerPrefab;

    public RunData LastRunData { get; private set; }

    public UnityEvent<GameStates> OnGameStateChange = new UnityEvent<GameStates>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        _levelManagerPrefab = Resources.Load(Const.PATH_TO_MANAGERS_FOLDER + Const.LEVEL_MANAGER_NAME) as GameObject;
        _uiManagerPrefab = Resources.Load(Const.PATH_TO_MANAGERS_FOLDER + Const.UI_MANAGER_NAME) as GameObject;

        LastRunData = new RunData(Time.time);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByName("GameScene"))
            LaunchGameScene();
        else
            LaunchGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (CurrentState == GameStates.MainMenu && Input.GetButtonDown(Const.RUN_AXIS_NAME))
                ChangeGameState(GameStates.Playing);
        }
    }

    public void ChangeGameState(GameStates newState)
    {
        CurrentState = newState;
        OnGameStateChange.Invoke(CurrentState);
        Debug.Log($"GAME CHANGED STATE TO {CurrentState}");
    }

    public void RegisterRunData(RunData runData)
    {
        LastRunData = runData;
    }

    public void LaunchGameOver()
    {
        ChangeGameState(GameStates.GameOver);
    }

    public void LaunchGameScene()
    {
        Instantiate(_uiManagerPrefab);
        Instantiate(_levelManagerPrefab);
        ChangeGameState(GameStates.MainMenu);
    }
}
