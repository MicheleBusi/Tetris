using UnityEngine;
using UnityEngine.Events;
public class GameStateManager : MonoBehaviour
{
    [SerializeField] UnityEvent pauseScreenTransitionIn = default;
    [SerializeField] UnityEvent pauseScreenTransitionOut = default;
    [SerializeField] UnityEvent loseScreenTransitionIn = default;

    public bool IsPaused { get; private set; } = false;
    public bool IsLost { get; private set; } = false;

    private void Start()
    {
        IsPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void PauseGame()
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

    public void UnpauseGame()
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

    public void GameLost()
    {
        IsLost = true;
        Time.timeScale = 0;
        Cursor.visible = true;

        loseScreenTransitionIn.Invoke();
    }
}
