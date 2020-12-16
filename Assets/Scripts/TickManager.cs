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
        lastTick = Time.time + 0.5f;
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
        tickIntervalMultiplier *= 0.9f;
    }

    public void SetFastTick()
    {
        tickInterval = fastTickInterval;
    }

    public void SetStandardTick()
    {
        tickInterval = standardTickInterval;
    }

    public IEnumerator PauseTickForSeconds(float duration)
    {
        IsTicking = false;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        IsTicking = true;
    }
}
