using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] SceneLoaderRequests loadRequests = default;
    //[SerializeField] SceneLoaderRequests unloadRequests = default;

    private void Awake()
    {
        loadRequests.loadSceneSingle += OnLoadSceneSingle;
        loadRequests.loadSceneAdditive += OnLoadSceneAdditive;
    }

    private void Start()
    {
        if (!SceneManager.GetSceneByName("Main Menu").isLoaded)
        {
            loadRequests.loadSceneAdditive("Main Menu");
        }
    }

    public void OnLoadSceneSingle(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public void OnLoadSceneAdditive(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncAndSetActive(sceneName));
    }

    IEnumerator LoadSceneAsyncAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(
            sceneName, 
            LoadSceneMode.Additive);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}

