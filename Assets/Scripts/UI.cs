using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void OnStartButton()
    {
        PlayerData data = SaveLoadSystem.Load();
        string sceneName = $"Scenes/Level{data.currentLevel}"; // Scene names as per your attachment
        SceneLoader.LoadSceneWithLoading(sceneName);
    }
    
    public void OnNewGameButton()
    {
        SaveLoadSystem.DeleteSave();
        SceneLoader.LoadSceneWithLoading("Scenes/Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
