using UnityEngine;

public class HighScoresManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent gameLost = default;
    [SerializeField] GameEvent saveHighScores = default;
    [SerializeField] GameEvent newHighScore = default;

    [Header("Variable References")]
    [SerializeField] IntVariable currentScore = default;
    [SerializeField] HighScores highScores = default;

    private void OnEnable()
    {
        gameLost.RegisterListener(CheckHighScores);
    }

    private void OnDisable()
    {
        gameLost.UnregisterListener(CheckHighScores);
    }

    void CheckHighScores()
    {
        int currentScoreRank = -1;
        int arrayLength = highScores.scores.Length;
        for (int i = arrayLength - 1; i >= 0; i--)
        {
            if (currentScore.Value > highScores.scores[i])
            {
                currentScoreRank = i;
            }
            else
            {
                break;
            }
        }

        if (currentScoreRank != -1)
        {
            for (int i = arrayLength - 1; i > currentScoreRank; i--)
            {
                highScores.scores[i] = highScores.scores[i - 1];
            }
            highScores.scores[currentScoreRank] = currentScore.Value;

            newHighScore.Raise();
            saveHighScores.Raise();
        }
    }
}
