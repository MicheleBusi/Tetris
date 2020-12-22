using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] BoardEventChannel boardEventChannel = default;
    [SerializeField] ScoreDisplay scoreDisplay = default;

    [Header("Parameters")]
    [SerializeField] int deletedRowBaseValue = 100;

    public int Score { get; private set; }

    private void Awake()
    {
        boardEventChannel.OnRowDeleted += OnRowDeleted;
    }

    public void OnRowDeleted(int comboIndex)
    {
        int scoreIncrease = (int)Mathf.Pow(comboIndex, 2) * deletedRowBaseValue;
        Score += scoreIncrease;
        StartCoroutine(scoreDisplay.ScoreChangeAnimation(scoreIncrease));
    }
}
