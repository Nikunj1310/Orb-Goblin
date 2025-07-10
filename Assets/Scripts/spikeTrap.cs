using UnityEngine;

public class spikeTrap : MonoBehaviour
{
    [SerializeField] private Player_Health player_Health;
    [SerializeField] private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player_Health = gameManager.GetComponent<Player_Health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // player = collision.gameObject;
            // playerMovementOfPlayerInTrigger = player.GetComponent<PlayerMovement>();
            // playerRigidbodyOfPlayerInTrigger = player.GetComponent<Rigidbody2D>();
            player_Health.hasBeenHit = true;
            
        }
    }
}
