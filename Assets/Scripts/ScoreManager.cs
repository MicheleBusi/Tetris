using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] ScoreDisplay scoreDisplay = default;

    [SerializeField] int deletedRowBaseValue = 100;

    public int Score { get; private set; }

    public void OnRowDeleted(int comboIndex)
    {
        int scoreIncrease = (int)Mathf.Pow(comboIndex, 2) * deletedRowBaseValue;

        Score += scoreIncrease;

        StartCoroutine(scoreDisplay.AddScoreAnimation(scoreIncrease));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Score += 100;
            StartCoroutine(scoreDisplay.AddScoreAnimation(100));
        }
    }
}
