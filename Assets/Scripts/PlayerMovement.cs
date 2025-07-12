using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using System.Linq;
using TreeEditor;

public class PlayerMovement : MonoBehaviour
{
    [Header("Assignment Attributes")]
    [SerializeField] GameObject gameManager;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public PhysicsMaterial2D groundFric;
    public bool canMove = false;
    [SerializeField] private Player_Health player_Health;

    [Header("Player Movement Tweeks")]
    public float groundPlayerSpeed;
    public float groundSpeedLowerLimit;
    public float groundSpeedUpperLimit;
    public float currentPlayerSpeed;
    [Space(5)]
    public float groundJumpforce;
    public float groundJumpLowerLimit;
    public float groundJumpUpperLimit;
    public float currentJumpForce;
    [Space(5)]
    public float frictionValue;
    public float frictionValueLowerLimit;
    public float frictionValueUpperLimit;
    [Space(5)]
    public float hitCooldown;
    public float hitCooldownLowerLimit;
    public float hitCooldownUpperLimit;

    [Header("Inspect properties")]
    public bool canJump = false;
    public string groundTagName = "Ground";
    [SerializeField] private float timerJump = 0f;
    [SerializeField] private float canMoveTimer = 0f;

    [Header("Player Keys")]
    public KeyCode CurrentJumpKey;
    public KeyCode defaultJumpkey = KeyCode.Space;
    public KeyCode[] CurrentHorizontalKeys = new KeyCode[4];
    public KeyCode[] defaultHorizontalKeys = Settings.horizontal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (frictionValueLowerLimit < 5)
        {
            Debug.LogError("Random Friction Value not acceptable, change!");
        }
        playerRigidbody = this.GetComponent<Rigidbody2D>();

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        currentJumpForce = groundJumpforce;
        currentPlayerSpeed = groundPlayerSpeed;
        for (int i = 0; i < 4; i++)
        {
            CurrentHorizontalKeys[i] = defaultHorizontalKeys[i];

        }
        CurrentJumpKey = defaultJumpkey;
        gameManager = GameObject.Find("GameManager");
        player_Health = gameManager.GetComponent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_Health.hasBeenHit)
        {
            canMove = false;
            Vector3 currentDir = playerRigidbody.linearVelocity.normalized;
            playerRigidbody.linearVelocity = -currentDir * currentPlayerSpeed;
            Debug.Log("Has been hit!");
        }

        if (canMove)
        {
            #region player hroizontal movement
            if (CurrentHorizontalKeys.Any(key => Input.GetKey(key)))
            {
                playerRigidbody.linearVelocityX = currentPlayerSpeed * playerFlip();
                // this.transform.position += new Vector3(currentPlayerSpeed,0,0)*Time.deltaTime;
                groundFric.friction = 0;
            }
            else
            {
                groundFric.friction = frictionValue;
            }
            #endregion
            #region player Jump
            if (Input.GetKeyDown(CurrentJumpKey) && canJump)
            {
                StartCoroutine(JumpGravityAffectCoroutine());
                playerRigidbody.AddForce(new Vector2(0, currentJumpForce));
            }

            #endregion
        }
        else
        {
            canMoveTimer += Time.deltaTime;
            if (canMoveTimer >= hitCooldown)
            {
                canMove = true;
                canMoveTimer = 0f;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTagName)
        {
            canJump = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTagName)
        {
            canJump = false;
        }
    }

    #region Methods
    IEnumerator JumpGravityAffectCoroutine()
    {
        timerJump = 0;
        while (!canJump || timerJump == 0)
        {
            timerJump += Time.deltaTime;
            if (playerRigidbody.linearVelocityY > 0)
            {
                playerRigidbody.gravityScale = 1.8f;
            }
            else if (playerRigidbody.linearVelocityY < 1.5)
            {
                playerRigidbody.gravityScale = 3f;
            }
            yield return null;
        }

        playerRigidbody.gravityScale = 1;

    }

    int playerFlip()
    {
        if (Input.GetKey(CurrentHorizontalKeys[0]) || Input.GetKey(CurrentHorizontalKeys[1]))
        {
            spriteRenderer.flipX = true;
            return -1;
        }
        else
        {
            spriteRenderer.flipX = false;
            return 1;
        }
    }
    #endregion

}
