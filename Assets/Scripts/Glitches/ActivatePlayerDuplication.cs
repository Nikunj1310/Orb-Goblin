using UnityEngine;

public class ActivatePlayerDuplication : MonoBehaviour
{
    [SerializeField] private PlayerDuplication playerDuplication;
    public GameObject playerObject;
    [SerializeField] GameObject gameManager;
    Player_Health player_Health;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player_Health = gameManager.GetComponent<Player_Health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && player_Health.playerHealth > 0)
        {
            playerObject = collision.gameObject;
            playerDuplication.playerCanDuplicate = true;
        }
    }
}
