using System.Collections;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] TickEventChannel tickEventChannel = default;
    [SerializeField] BoardEventChannel boardEventChannel = default;

    [Header("Parameters")]
    [SerializeField] float standardTickInterval = 0.35f;
    [SerializeField] float fastTickInterval = 0.1f;
    [Tooltip("How much faster will next level be? In percentage")]
    [SerializeField][Range(.01f, .3f)] float tickRateIncrease = 0.2f;

    public static bool IsTicking { get; set; } = true;

    float tickInterval;
    float lastTick;
    float tickIntervalMultiplier = 1f;

    private void Awake()
    {
        tickEventChannel.OnTickDelay += OnTickDelay;
        tickEventChannel.OnTickPause += OnTickPause;
        tickEventChannel.OnTickUnpause += OnTickUnpause;
        tickEventChannel.OnSetFastTick += OnSetFastTick;
        tickEventChannel.OnSetStandardTick += OnSetStandardTick;

        boardEventChannel.OnLevelUp += IncreaseTickRate;

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
            tickEventChannel.RaiseTick();
        }
    }

    void OnTickPause()
    {
        IsTicking = false;
    }
    void OnTickUnpause()
    {
        IsTicking = true;
    }

    void IncreaseTickRate(int level)
    {
        tickIntervalMultiplier *= (1f - tickRateIncrease);
    }

    void OnSetFastTick()
    {
        tickInterval = fastTickInterval;
    }

    void OnSetStandardTick()
    {
        tickInterval = standardTickInterval;
    }

    void OnTickDelay(float delay)
    {
        lastTick += delay;
    }
}
