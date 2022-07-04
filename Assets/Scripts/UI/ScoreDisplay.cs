using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<int> score;
    [SerializeField]
    private TextMeshProUGUI scoreDisplay;

    private void Awake()
    {
        score.Value = 0;
    }

    private void OnEnable()
    {
        score.ValueChangeEvent.AddListener(OnScoreChanged);
    }

    private void OnDisable()
    {
        score.ValueChangeEvent.RemoveListener(OnScoreChanged);
    }

    private void OnScoreChanged(int newScore)
    {
        scoreDisplay.text = newScore.ToString();
    }
}
