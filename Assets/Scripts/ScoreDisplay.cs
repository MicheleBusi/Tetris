using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Lean.Transition;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager = default;
    [SerializeField] GameObject addScoreTextPrefab = default;
    [SerializeField] Canvas canvas = default;
    [SerializeField] UnityEvent OnScoreIncreaseAnimation = default;

    Text scoreText = null;

    int currentlyDisplayedScore = 0;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    bool isAnimatingScore = false;
    public IEnumerator ScoreChangeAnimation(int scoreChange)
    {
        if (scoreChange >= 0)
        {
            // Instantiate Add Score prefab
            GameObject scoreIncreaseText = Instantiate(addScoreTextPrefab, canvas.transform, false);
            RectTransform rt = scoreIncreaseText.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector3.zero;
            Text text = scoreIncreaseText.GetComponent<Text>();
            text.text = "+" + scoreChange.ToString();
            text.fontSize += scoreChange / 25;
            yield return new WaitForSeconds(.3f);

            // Slide towards score
            rt.SetParent(this.transform, worldPositionStays: true);
            rt.anchoredPositionTransition(new Vector2(0, 0), .7f, LeanEase.Accelerate);
            yield return new WaitForSeconds(.7f);
            OnScoreIncreaseAnimation.Invoke();
            Destroy(scoreIncreaseText);
        }

        // Slowly increase displayed score
        if (isAnimatingScore)
        {
            // If another instance of this coroutine is running the score animation code, let it take care of it.
            // But give it more time by increasing ftMax
            yield break;
        }
        isAnimatingScore = true;

        for (float ft = 0f; ft <= 1f; ft += 0.01f)
        {
            int displayScore = Mathf.RoundToInt(
                Mathf.Lerp(currentlyDisplayedScore, scoreManager.Score, ft));
        
            scoreText.text = displayScore.ToString();
            yield return new WaitForSeconds(0.03f);
        }

        scoreText.text = scoreManager.Score.ToString();
        currentlyDisplayedScore = scoreManager.Score;

        isAnimatingScore = false;
    }
}
