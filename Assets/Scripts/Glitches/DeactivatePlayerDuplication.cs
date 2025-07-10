using UnityEngine;

public class DeactivatePlayerDuplication : MonoBehaviour
{
    [SerializeField] private PlayerDuplication playerDuplication;
    [SerializeField] private GameObject gameManager;
    Player_Health player_Health;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player_Health = gameManager.GetComponent<Player_Health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&& player_Health.playerHealth > 0)
        {
            playerDuplication.playerCanDuplicate = false;
        }
    }
}
