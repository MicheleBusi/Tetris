using System;
using UnityEngine;
using UnityEngine.Events;
public class GameStateManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEvent TogglePause = default;
    [SerializeField] GameEvent GamePaused = default;
    [SerializeField] GameEvent GameUnpaused = default;
    [SerializeField] GameEvent GameLost = default;

    [SerializeField] BoolVariable isGamePaused = default;

    public bool IsLost { get; private set; } = false;

    private void Awake()
    {
        TogglePause.RegisterListener(OnTogglePause);
        GameLost.RegisterListener(OnGameLost);
    }

    private void OnDestroy()
    {
        TogglePause.UnregisterListener(OnTogglePause);
        GameLost.UnregisterListener(OnGameLost);
    }

    private void OnTogglePause()
    {
        if (IsLost)
        {
            return;
        }

        if (!isGamePaused.Value)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    private void Start()
    {
        isGamePaused.Value = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void Pause()
    {
        GamePaused.Raise();

        isGamePaused.Value = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    void Unpause()
    {
        GameUnpaused.Raise();

        isGamePaused.Value = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void OnGameLost()
    {
        IsLost = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }
}
