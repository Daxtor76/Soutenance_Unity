using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerGameOver : MonoBehaviour
{
    private Transform _ui;
    public TextMeshProUGUI _statsText;
    public Button _reloadButton;
    public Button _quitButton;

    private void Awake()
    {
        _ui = transform.Find("GameOver").transform;
        _statsText = _ui.Find("StatsText").GetComponent<TextMeshProUGUI>();
        _reloadButton = _ui.Find("ReloadButton").GetComponent<Button>();
        _quitButton = _ui.Find("QuitButton").GetComponent<Button>();
    }

    private void Start()
    {
        RunData lastRunData = GameManager.Instance.LastRunData;
        _reloadButton.onClick.AddListener(OnReloadButtonClick);
        _quitButton.onClick.AddListener(OnQuitButtonClick);
        _statsText.text = $"Run duration: {lastRunData.GetDuration()}\n" +
            $"Distance: {lastRunData.GetDistance()}\n" +
            $"Enemies killed: {lastRunData.GetEnemiesKilled()}\n" +
            $"Total orbs: {lastRunData.GetPatounesCount()}\n" +
            $"Total score: {lastRunData.GetTotalScore()}";
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }

    private void OnReloadButtonClick()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
