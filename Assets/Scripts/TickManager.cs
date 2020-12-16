using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    BoardManager boardManager = default;

    [SerializeField] float standardTickRate = 0.35f;
    [SerializeField] float fastTickRate = 0.1f;

    public bool IsTicking { get; set; } = true;

    float tickRate;
    float lastTick;

    private void Awake()
    {
        boardManager = GetComponent<BoardManager>();
        tickRate = standardTickRate;
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

        if (Time.time > lastTick + tickRate)
        {
            lastTick = Time.time;
            boardManager.Tick();
        }
    }

    public void SetFastTick()
    {
        tickRate = fastTickRate;
    }

    public void SetStandardTick()
    {
        tickRate = standardTickRate;
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
