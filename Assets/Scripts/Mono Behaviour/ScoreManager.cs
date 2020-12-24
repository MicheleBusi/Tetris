using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent rowDeleted = default;

    [Header("")]
    [SerializeField] IntVariable currentScore = default;
    [SerializeField] int deletedRowBaseValue = 100;

    public int Score { get; private set; }

    private void Awake()
    {
        rowDeleted.RegisterListener(OnRowDeleted);
        currentScore.Value = 0;
    }

    private void OnDestroy()
    {
        rowDeleted.UnregisterListener(OnRowDeleted);
    }

    public void OnRowDeleted()
    {
        int comboIndex = rowDeleted.sentInt;
        int scoreIncrease = (int)Mathf.Pow(comboIndex, 2) * deletedRowBaseValue;
        Score += scoreIncrease;
        currentScore.Value += scoreIncrease;
        //StartCoroutine(scoreDisplay.ScoreChangeAnimation(scoreIncrease));
    }
}
