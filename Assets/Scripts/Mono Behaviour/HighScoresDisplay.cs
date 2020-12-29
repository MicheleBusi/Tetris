using UnityEngine;
using UnityEngine.UI;

public class HighScoresDisplay : MonoBehaviour
{
    [SerializeField] GameEvent loadHighScores = default;
    [SerializeField] HighScores highScores = default;

    Text highScoreText = null;
    private void Awake()
    {
        highScoreText = GetComponent<Text>();
    }

    private void Start()
    {
        loadHighScores.Raise();
        UpdateHighScoresText();
    }

    private void UpdateHighScoresText()
    {
        string newHighScoresText = "";
        for (int i = 0; i < highScores.scores.Length; i++)
        {
            newHighScoresText += (i + 1).ToString() + " - " + highScores.scores[i].ToString() + "\n";
        }
        highScoreText.text = newHighScoresText;
    }
}
