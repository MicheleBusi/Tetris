using System.Collections;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent tick = default;
    [SerializeField] GameEvent tickPaused = default;
    [SerializeField] GameEvent tickUnpaused = default;
    [SerializeField] GameEvent tickDelayed = default;
    [SerializeField] GameEvent tickSetFast = default;
    [SerializeField] GameEvent tickSetStandard = default;
    [SerializeField] GameEvent levelUp = default;

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
        tickPaused.RegisterListener(OnTickDelay);
        tickUnpaused.RegisterListener(OnTickUnpause);
        tickPaused.RegisterListener(OnTickPause);
        tickSetFast.RegisterListener(OnSetFastTick);
        tickSetStandard.RegisterListener(OnSetStandardTick);
        levelUp.RegisterListener(IncreaseTickRate);

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
            tick.sentInt++;
            tick.Raise();
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

    void IncreaseTickRate()
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

    void OnTickDelay()
    {
        float delay = tickDelayed.sentFloat;
        lastTick += delay;
    }
}
