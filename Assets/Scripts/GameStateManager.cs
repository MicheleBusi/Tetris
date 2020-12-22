using UnityEngine;
using UnityEngine.Events;
public class GameStateManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] GameStateEventChannel gameStateEventChannel = default;

    [SerializeField] UnityEvent pauseScreenTransitionIn = default;
    [SerializeField] UnityEvent pauseScreenTransitionOut = default;
    [SerializeField] UnityEvent loseScreenTransitionIn = default;

    public static bool IsPaused { get; private set; } = false;
    public bool IsLost { get; private set; } = false;

    private void Awake()
    {
        gameStateEventChannel.OnGamePause += OnGamePause;
        gameStateEventChannel.OnGameUnpause += OnGameUnpause;
        gameStateEventChannel.OnGameLost += OnGameLost;
    }

    private void Start()
    {
        IsPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void OnGamePause()
    {
        if (IsLost)
        {
            return;
        }

        IsPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;

        pauseScreenTransitionIn.Invoke();
    }

    void OnGameUnpause()
    {
        if (IsLost)
        {
            return;
        }

        IsPaused = false;
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
