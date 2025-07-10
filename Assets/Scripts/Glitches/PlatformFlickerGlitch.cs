using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformFlickerGlitch : MonoBehaviour
{

    [SerializeField] private float upperBond;
    [SerializeField] private float lowerBond;
    // [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject playerFollowCameraGameObject;
    
    [Header("Inspect")]
    [SerializeField] private GameObject tileMap;
    [SerializeField] private Spawn_Player spawn_Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerFollowCameraGameObject = GameObject.Find("MainPlayerCamera");
        // gameManager = GameObject.Find("GameManager");
        // spawn_Player = gameManager.GetComponent<Spawn_Player>();
        tileMap = this.gameObject;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == /*spawn_Player.playerinstance*/playerFollowCameraGameObject.GetComponent<CinemachineCamera>().Follow?.gameObject)
        {
            int luckyGuess = Random.Range(0, 5);
            Debug.Log($"The Lucky number is{luckyGuess}.");
            if (luckyGuess % 2 == 0)
            {
                tileMap.GetComponent<TilemapCollider2D>().isTrigger = true;
            }
        }

        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == /*spawn_Player.playerinstance*/playerFollowCameraGameObject.GetComponent<CinemachineCamera>().Follow)
        {
            tileMap.GetComponent<TilemapCollider2D>().isTrigger = false;
        }
    }
}