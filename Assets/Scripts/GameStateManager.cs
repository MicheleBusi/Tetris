using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen = default;
    [SerializeField] GameObject loseScreen = default;

    public bool IsPaused { get; private set; } = false;

    private void Start()
    {
        UnpauseGame();
    }

    public void PauseGame()
    {
        IsPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        IsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void GameLost()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
        Cursor.visible = true;
    }
}
