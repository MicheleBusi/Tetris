using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
