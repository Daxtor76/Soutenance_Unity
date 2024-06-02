using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject _uiPrefab;
    private Transform _ui;

    private GameObject mainMenuUI;
    private GameObject gameUI;

    private void Awake()
    {
        _uiPrefab = Resources.Load(Const.PATH_TO_UI_FOLDER + Const.UI_NAME) as GameObject;

        _ui = Instantiate(_uiPrefab).transform;
        mainMenuUI = _ui.transform.Find("MainMenu").gameObject;
        gameUI = _ui.transform.Find("Game").gameObject;

        GameManager.Instance.OnGameStateChange.AddListener(AdaptUIOnGameState);
    }

    private void AdaptUIOnGameState(GameManager.GameStates state)
    {
        switch (state)
        {
            case GameManager.GameStates.MainMenu:
                mainMenuUI.SetActive(true);
                gameUI.SetActive(false);
                break;
            case GameManager.GameStates.Playing:
                mainMenuUI.SetActive(false);
                gameUI.SetActive(true);
                break;
            case GameManager.GameStates.GameOver:
                mainMenuUI.SetActive(false);
                gameUI.SetActive(false);
                break;
        }
    }
}
