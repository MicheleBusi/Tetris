using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public void LaunchGame(float delay)
    {
        StartCoroutine(LoadSceneAfterDelay("Game", delay));
    }

    public void ReturnToMenu()
    {
        StartCoroutine(LoadSceneAfterDelay("Main Menu", 1f));
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
