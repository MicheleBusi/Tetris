using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Lean.Transition;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager = default;
    [SerializeField] GameObject addScoreTextPrefab = default;
    [SerializeField] Canvas canvas = default;

    Text scoreText = null;

    int currentlyDisplayedScore = 0;
    int targetScore = 0;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    public IEnumerator AddScoreAnimation(int scoreIncrease)
    {
        // Instantiate Add Score prefab and slide it towards the total score
        GameObject scoreIncreaseText = Instantiate(addScoreTextPrefab, canvas.transform, false);
        RectTransform rt = scoreIncreaseText.GetComponent<RectTransform>();
        Text text = scoreIncreaseText.GetComponent<Text>();
        text.text = "+" + scoreIncrease.ToString();
        yield return new WaitForSeconds(.3f);
        rt.anchoredPositionTransition(new Vector2(-690, 380), .7f, LeanEase.Accelerate);
        yield return new WaitForSeconds(.7f);
        Destroy(scoreIncreaseText);

        // Slowly increase displayed score
        int displayScore;
        targetScore = scoreManager.Score;
        for (float ft = 0f; ft <= 1; ft += 0.1f)
        {
            displayScore = Mathf.RoundToInt(
                Mathf.Lerp(currentlyDisplayedScore, targetScore, ft));
        
            scoreText.text = displayScore.ToString();
            yield return new WaitForSeconds(0.03f);
        }

        displayScore = targetScore;
        scoreText.text = displayScore.ToString();
        currentlyDisplayedScore = displayScore;
    }
}
