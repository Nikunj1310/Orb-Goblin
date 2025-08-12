using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera cinemachineCamera;
    [SerializeField]
    private GameObject playerDuplicationObject;
    [SerializeField]
    public int currenPlayerIndex;
    private PlayerDuplication playerDuplication;
    private Coroutine coroutine = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Player Follow Script Started");
        cinemachineCamera = (CinemachineCamera)this.gameObject.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        playerDuplicationObject = GameObject.Find("Player Duplication");
        playerDuplication = playerDuplicationObject.GetComponent<PlayerDuplication>();
    }

    void Update()
    {        cinemachineCamera = (CinemachineCamera)this.gameObject.GetComponent<CinemachineBrain>().ActiveVirtualCamera;


        Debug.Log("Player Follow Script Update");
        if (playerDuplication && cinemachineCamera && playerDuplicationObject && playerDuplication.spawnedPlayers.Count > 0 && coroutine == null)
        {
            coroutine = StartCoroutine(startFindingTargets());
        }
        
        
            if (playerDuplication.spawnedPlayers[currenPlayerIndex] == null)
            {
                cinemachineCamera.Follow = playerDuplication.spawnedPlayers[0].transform;
            }
        
        
        
            // Game Over
        
    }

    IEnumerator startFindingTargets()
    {
        while (true)
        {
            Debug.Log("Finding Targets Coroutine Running");
            float timer = Random.Range(2, 6);
            currenPlayerIndex = Random.Range(0, playerDuplication.spawnedPlayers.Count);
            cinemachineCamera.Follow = playerDuplication.spawnedPlayers[currenPlayerIndex].transform;
            yield return new WaitForSeconds(timer);
        }
    }

}
