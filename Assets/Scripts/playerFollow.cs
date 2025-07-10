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
    private int currenPlayerIndex;
    private PlayerDuplication playerDuplication;
    private Coroutine coroutine = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cinemachineCamera = (CinemachineCamera)this.gameObject.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        playerDuplicationObject = GameObject.Find("Player Duplication");
        playerDuplication = playerDuplicationObject.GetComponent<PlayerDuplication>();
    }

    void Update()
    {
        if (playerDuplication && cinemachineCamera && playerDuplicationObject && playerDuplication.spawnedPlayers.Count > 0 && coroutine == null)
        {
            coroutine = StartCoroutine(startFindingTargets());
        }
        if (playerDuplication.spawnedPlayers[currenPlayerIndex] == null)
        {
            cinemachineCamera.Follow = playerDuplication.spawnedPlayers[0].transform;
        }
    }

    IEnumerator startFindingTargets()
    {
        while (true)
        {
            float timer = Random.Range(2, 6);
            Debug.Log(timer);
            currenPlayerIndex = Random.Range(0, playerDuplication.spawnedPlayers.Count);
            cinemachineCamera.Follow = playerDuplication.spawnedPlayers[currenPlayerIndex].transform;
            yield return new WaitForSeconds(timer);
        }
    }

}
