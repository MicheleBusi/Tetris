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

    [SerializeField] UnityEvent pauseScreenTransitionIn = default;
    [SerializeField] UnityEvent pauseScreenTransitionOut = default;
    [SerializeField] UnityEvent loseScreenTransitionIn = default;

    public bool IsLost { get; private set; } = false;

    private void Awake()
    {
        TogglePause.RegisterListener(OnTogglePause);
        GameLost.RegisterListener(OnGameLost);
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

        pauseScreenTransitionIn.Invoke();
    }

    void Unpause()
    {
        GameUnpaused.Raise();

        isGamePaused.Value = false;
        Time.timeScale = 1;
        Cursor.visible = false;

        pauseScreenTransitionOut.Invoke();
    }

    public void OnGameLost()
    {
        IsLost = true;
        Time.timeScale = 0;
        Cursor.visible = true;

        loseScreenTransitionIn.Invoke();
    }
}
