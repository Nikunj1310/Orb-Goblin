using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GayBackground : MonoBehaviour
{

    Coroutine backgroundColorChanger = null;
    [SerializeField] GameObject Camera;
    [SerializeField] private Camera screenCamera;
    [SerializeField] float countDown = 5f;
    void Start()
    {
        screenCamera = Camera.GetComponent<Camera>();
        backgroundColorChanger = StartCoroutine(rainbowTIme());
    }
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    IEnumerator rainbowTIme()
    {
        while (true)
        {
            for (float i = 0; i < 1; i += Time.deltaTime / countDown)
            {
                Color color = Color.HSVToRGB(i, 1f, 1f);
                screenCamera.backgroundColor = color;
                yield return null;
            }
            yield return null;
        }
    }
    private void OnSceneUnloaded(Scene current)
    {
        if (backgroundColorChanger != null)
        {
            StopCoroutine(backgroundColorChanger);
            backgroundColorChanger = null;
            Debug.Log("Coroutine stopped because scene was unloaded.");
        }
    }
}
