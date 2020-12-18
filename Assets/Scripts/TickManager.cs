using System.Collections;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    BoardManager boardManager = default;

    [SerializeField] float standardTickInterval = 0.35f;
    [SerializeField] float fastTickInterval = 0.1f;

    public bool IsTicking { get; set; } = true;

    float tickInterval;
    float lastTick;

    float tickIntervalMultiplier = 1f;

    private void Awake()
    {
        boardManager = GetComponent<BoardManager>();
        tickInterval = standardTickInterval;
        IsTicking = false;
    }

    private void Start()
    {
        // Delay the first tick
        lastTick = Time.time + 1f;
    }

    private void Update()
    {
        if (!IsTicking)
        {
            return;
        }

        if (Time.time > lastTick + (tickInterval * tickIntervalMultiplier))
        {
            lastTick = Time.time;
            boardManager.Tick();
        }
    }

    public void IncreaseTickRate()
    {
        tickIntervalMultiplier *= 0.8f;
    }

    public void SetFastTick()
    {
        tickInterval = fastTickInterval;
    }

    public void SetStandardTick()
    {
        tickInterval = standardTickInterval;
    }

    public void DelayNextTickBy(float delay)
    {
        lastTick += delay;
    }
}
