using Unity.Cinemachine;
using UnityEngine;

public class Spawn_Player : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject spwanPoint;
    public GameObject playerinstance = null;
    public bool hasGameStarted = false;
    [SerializeField] private PlayerDuplication playerDuplication;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        hasGameStarted = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGameStarted && playerinstance == null)
        {
            playerinstance = Instantiate(playerPrefab, spwanPoint.transform);
            hasGameStarted = false;
            playerDuplication.spawnedPlayers.Add(playerinstance);
        }
    }
}
