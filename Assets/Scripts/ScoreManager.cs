using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent rowDeleted = default;

    [Header("")]
    [SerializeField] ScoreDisplay scoreDisplay = default;
    [SerializeField] int deletedRowBaseValue = 100;

    public int Score { get; private set; }

    private void Awake()
    {
        rowDeleted.RegisterListener(OnRowDeleted);
    }

    public void OnRowDeleted()
    {
        int comboIndex = rowDeleted.sentInt;
        int scoreIncrease = (int)Mathf.Pow(comboIndex, 2) * deletedRowBaseValue;
        Score += scoreIncrease;
        StartCoroutine(scoreDisplay.ScoreChangeAnimation(scoreIncrease));
    }
}
