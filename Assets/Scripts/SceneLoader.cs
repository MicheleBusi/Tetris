using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public void LaunchGameAfterDelay(float delay)
    {
        StartCoroutine(LaunchGame(delay));
    }

    public IEnumerator LaunchGame(float delay)
    {
        yield return new WaitForSeconds(delay);
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
