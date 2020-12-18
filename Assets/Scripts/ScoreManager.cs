using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] ScoreDisplay scoreDisplay = default;

    [SerializeField] int deletedRowBaseValue = 100;

    public int Score { get; private set; }

    public void ResetScore()
    {
        
    }

    public void OnRowDeleted(int comboIndex)
    {
        int scoreIncrease = (int)Mathf.Pow(comboIndex, 2) * deletedRowBaseValue;

        Score += scoreIncrease;

        StartCoroutine(scoreDisplay.ScoreChangeAnimation(scoreIncrease));
    }
}
