using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scene Management/Scene Loader")]
public class SceneLoaderRequests : ScriptableObject
{
    public UnityAction<string> loadSceneSingle = default;
    public UnityAction<string> loadSceneAdditive = default;

    public void LoadSceneSingle(string sceneName)
    {
        loadSceneSingle.Invoke(sceneName);
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public void LoadSceneAdditive(string sceneName)
    {
        loadSceneAdditive.Invoke(sceneName);
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
