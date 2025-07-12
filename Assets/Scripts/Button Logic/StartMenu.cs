using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingsCanvas;
    public void Start()
    {
        SettingsCanvas = GameObject.Find("Settings");
        SettingsCanvas.SetActive(false);
    }

    public void SettingsEnable()
    {
        SettingsCanvas.SetActive(true);
    }

    public void onClickStart()
    {
        SceneManager.LoadScene("Level1");
    }

    public void onQuitButtonPress()
    {
        Application.Quit();
    }
}
