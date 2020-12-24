using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] GameEvent scoreChanged = default;
    [SerializeField] IntVariable currentScore = default;

    [SerializeField] UnityEvent OnScoreIncreaseAnimation = default;

    Text scoreText = null;
    int currentlyDisplayedScore = 0;
    bool isAnimatingScore = false;

    private void Awake()
    {
        scoreText = GetComponent<Text>();

        scoreChanged.RegisterListener(OnScoreChanged);
    }

    private void OnDestroy()
    {
        scoreChanged.UnregisterListener(OnScoreChanged);
    }

    private void OnScoreChanged()
    {
        StartCoroutine(ScoreChangeAnimation());
    }

    // Slowly increase displayed score
    public IEnumerator ScoreChangeAnimation()
    {
        OnScoreIncreaseAnimation.Invoke();

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
                Mathf.Lerp(currentlyDisplayedScore, currentScore.Value, ft));
        
            scoreText.text = displayScore.ToString();
            yield return new WaitForSeconds(0.03f);
        }

        scoreText.text = currentScore.Value.ToString();
        currentlyDisplayedScore = currentScore.Value;

        isAnimatingScore = false;
    }
}
