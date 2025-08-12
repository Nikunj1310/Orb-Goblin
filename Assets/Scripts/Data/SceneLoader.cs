using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static string targetSceneName;
    float timer = 0f;

    public static void LoadSceneWithLoading(string targetScene)
    {
        targetSceneName = targetScene;
        // Show the loading scene first
        SceneManager.LoadScene("Scenes/LoadingScene");
    }

    public static void LoadSceneWithGameOver(string targetScene)
    {
        targetSceneName = targetScene;
        // Show the game over scene first
        SceneManager.LoadScene("Scenes/GameOverScene");
    }

    Coroutine check = null;

    public bool autoLoad = false;


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 8f)
        {
            autoLoad = true;
        }
        if (autoLoad && !string.IsNullOrEmpty(targetSceneName) && check == null)
        {
            check = StartCoroutine(LoadAsync());
        }
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);

        while (!asyncLoad.isDone)
        {
            // Optional: Update a UI loading bar with asyncLoad.progress here
            yield return null;
        }
    }
}
