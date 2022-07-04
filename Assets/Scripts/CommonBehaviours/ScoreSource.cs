using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSource : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<int> score;

    [SerializeField]
    private int scoreIncrease;


    public void TryIncreaseScore(bool isActionFromPlayer)
    {
        if (!isActionFromPlayer)
            return;
        score.Value += scoreIncrease;
    }
}
