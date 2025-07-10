using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ReverseControls : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Coroutine reverseControls = null;

    [Header("Player Settings")]
    // public KeyCode[] HorizontalAndJumpA = { KeyCode.D, KeyCode.LeftArrow, KeyCode.A, KeyCode.RightArrow, KeyCode.Space };
    // public KeyCode[] HorizontalAndJumpB = { KeyCode.D, KeyCode.RightArrow, KeyCode.A, KeyCode.LeftArrow, KeyCode.Space };
    // public KeyCode[] HorizontalAndJumpC = { KeyCode.D, KeyCode.RightArrow, KeyCode.Space, KeyCode.LeftArrow, KeyCode.A };
    // public KeyCode[] HorizontalAndJumpD = { KeyCode.A, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.D, KeyCode.Space };
    public KeyCode[][] PlayerControlsEdit;


    [SerializeField] private float timer = 0f;
    [SerializeField] private float upperBondTimeLimit = 0f;
    [SerializeField] private float lowerBondTimeLimit = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeyCode A = playerMovement.defaultHorizontalKeys[0];
        KeyCode leftArrow = playerMovement.defaultHorizontalKeys[1];
        KeyCode D = playerMovement.defaultHorizontalKeys[2];
        KeyCode rightArrow = playerMovement.defaultHorizontalKeys[3];
        KeyCode jump = playerMovement.defaultJumpkey;

        PlayerControlsEdit = new KeyCode[][]
        {
    new KeyCode[] {D, leftArrow, A, rightArrow, jump},
    new KeyCode[] {D, rightArrow, A, leftArrow, jump},
    new KeyCode[] {D, rightArrow, jump, leftArrow, A},
    new KeyCode[] {A, rightArrow, leftArrow, D, jump}
};

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // player = gameManager.GetComponent<Spawn_Player>().playerinstance;
        // playerMovement = player.GetComponent<PlayerMovement>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            playerMovement = player.GetComponent<PlayerMovement>();
            if (reverseControls == null)
            {
                reverseControls = StartCoroutine(reverseControlsCoroutine());
            }
        }
    }

    IEnumerator reverseControlsCoroutine()
    {
        yield return new WaitForSeconds(1f);
        timer = Random.Range(lowerBondTimeLimit, upperBondTimeLimit);
        int number = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
            playerMovement.CurrentHorizontalKeys[i] = PlayerControlsEdit[number][i];
        }
        playerMovement.CurrentJumpKey = PlayerControlsEdit[number][4];
        Debug.Log($"The random numebr for player controls is:\n{number}\nAnd the timer is of {timer}s");
        yield return new WaitForSeconds(timer);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.gameObject;
        playerMovement = player.GetComponent<PlayerMovement>();
        if (player.tag == "Player")
        {

            if (reverseControls != null)
            {
                StopCoroutine(reverseControls);
                reverseControls = null;
            }

            for (int i = 0; i < playerMovement.defaultHorizontalKeys.Length; i++)
            {
                playerMovement.CurrentHorizontalKeys[i] = playerMovement.defaultHorizontalKeys[i];
            }
            playerMovement.CurrentJumpKey = playerMovement.defaultJumpkey;
        }
    }
}
