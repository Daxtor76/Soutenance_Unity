using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoringView : MonoBehaviour
{
    public TextMeshProUGUI scoringText;

    private void Awake()
    {
        scoringText = transform.Find(Const.SCORING_TEXT_NAME)?.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        FindObjectOfType<ScoreController>()?.OnScoreChange.AddListener(OnScoreChange);
        scoringText.text = 0.ToString();
    }

    private void OnScoreChange(int newValue)
    {
        scoringText.SetText(newValue.ToString());
    }
}