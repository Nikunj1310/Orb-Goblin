using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class TweakPlayerGravity : MonoBehaviour
{
    [SerializeField] private float VerticalSpeed = 10f;
    [SerializeField] private float lowerLimitforTweak = 2f;
    [SerializeField] private float upperLimitforTweak = 4f;
    [SerializeField] private float timeGapForNextGravityCheck = 3f;

    [SerializeField] private GameObject playerFollowCameraGameObject;
    [SerializeField] private KeyCode[] upwardsMovement = new KeyCode[2] { KeyCode.W, KeyCode.UpArrow };
    [SerializeField] private KeyCode[] downwardsMovement = new KeyCode[2] { KeyCode.S, KeyCode.DownArrow };

    // Track each player's coroutine and vertical movement state
    [SerializeField]private Dictionary<GameObject, Coroutine> playerCoroutines = new Dictionary<GameObject, Coroutine>();
    [SerializeField]private HashSet<GameObject> canMoveVertically = new HashSet<GameObject>();

    void Start()
    {
        if (playerFollowCameraGameObject == null)
            playerFollowCameraGameObject = GameObject.Find("MainPlayerCamera");
    }

    void Update()
    {
        foreach (var player in canMoveVertically)
        {
            var rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            // Handle vertical movement input
            if (upwardsMovement.Any(key => Input.GetKey(key)))
            {
                rb.linearVelocityY = VerticalSpeed;
            }
            else if (downwardsMovement.Any(key => Input.GetKey(key)))
            {
                rb.linearVelocityY = -VerticalSpeed;
            }
            else
            {
                // Smoothly decelerate vertical movement
                rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, 0, Time.deltaTime * 3f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var playerObj = collision.gameObject;
        if (playerObj == playerFollowCameraGameObject.GetComponent<CinemachineCamera>().Follow?.gameObject)
        {
            if (!playerCoroutines.ContainsKey(playerObj))
            {
                var coroutine = StartCoroutine(AffectPlayerGravity(playerObj));
                playerCoroutines[playerObj] = coroutine;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var playerObj = collision.gameObject;
        if (Random.Range(0f, 1f) > 0.7f)
        {
            if (playerCoroutines.TryGetValue(playerObj, out var coroutine))
            {
                StopCoroutine(coroutine);
                playerCoroutines.Remove(playerObj);
                canMoveVertically.Remove(playerObj);
                var rb = playerObj.GetComponent<Rigidbody2D>();
                rb.gravityScale = 1;
            }
        }
        else
        {
            if (playerCoroutines.Count > 0 && Random.value < 0.5f)
            {
                var randomEntry = playerCoroutines.ElementAt(Random.Range(0, playerCoroutines.Count));
                StopCoroutine(randomEntry.Value);
                playerCoroutines.Remove(randomEntry.Key);
                canMoveVertically.Remove(randomEntry.Key);
                var rb = randomEntry.Key.GetComponent<Rigidbody2D>();
                rb.gravityScale = 1;
            }
        }
    }

    IEnumerator AffectPlayerGravity(GameObject player)
    {
        var rb = player.GetComponent<Rigidbody2D>();
        while (true)
        {
            int rand = Random.Range(0, 5);
            Debug.Log("deciding on what to do!");
            yield return new WaitForSeconds(3f);

            if (rand % 2 == 0)
            {
                Debug.Log("Time to tweak fr!");

                float timer = Random.Range(lowerLimitforTweak, upperLimitforTweak);
                float previousGravity = rb.gravityScale;
                int luck = Random.Range(0, 11);

                if (luck < 5)
                {
                    Debug.Log("Invert gravity");
                    rb.gravityScale = -1;
                }
                else
                {
                    Debug.Log("Enable top-down movement");
                    canMoveVertically.Add(player);
                }

                yield return new WaitForSeconds(timer);

                // Reset states
                canMoveVertically.Remove(player);
                rb.gravityScale = previousGravity;
            }
            else
            {
                Debug.Log("Nah you safe for now!");
                yield return new WaitForSeconds(timeGapForNextGravityCheck);
            }
        }
    }
}
