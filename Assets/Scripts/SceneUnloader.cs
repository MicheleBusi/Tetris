using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scene Management/Scene Unloader")]
public class SceneUnloader : ScriptableObject
{
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void UnloadActiveScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
