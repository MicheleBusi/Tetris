using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen = default;
    [SerializeField] GameObject loseScreen = default;

    public bool IsPaused { get; private set; } = false;
    public bool IsLost { get; private set; } = false;

    private void Start()
    {
        UnpauseGame();
    }

    public void PauseGame()
    {
        if (IsLost)
        {
            return;
        }

        IsPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        if (IsLost)
        {
            return;
        }

        IsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void GameLost()
    {
        IsLost = true;
        Time.timeScale = 0;
        loseScreen.SetActive(true);
        Cursor.visible = true;
    }
}
