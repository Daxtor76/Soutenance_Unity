using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoringView : MonoBehaviour
{
    public TextMeshProUGUI scoringText;

    private void Awake()
    {
        scoringText = transform.Find(Const.SCORING_TEXT_NAME).GetComponent<TextMeshProUGUI>();
        scoringText.text = "0";
    }

    private void Start()
    {
        FindObjectOfType<ScoreController>(true)?.OnScoreChange?.AddListener(OnScoreChange);
    }

    private void OnScoreChange(int newValue)
    {
        scoringText.SetText(newValue.ToString());
    }
}