using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerDuplication : MonoBehaviour
{

    public bool playerCanDuplicate = false;
    [SerializeField] private float playerDuplicationLowerLimit;
    [SerializeField] private float defaultPlayerDuplicationLowerLimit;
    [SerializeField] private float playerDuplicationUpperLimit;
    [SerializeField] private float defaultPlayerDuplicationUpperLimit;
    [SerializeField] private float bufferIncrease;
    [SerializeField] private Vector2 duplicationOffset;
    [SerializeField] private float timer = 0f;
    [SerializeField] private Coroutine playerDuplication = null;
    [SerializeField] private Coroutine stopPlayerDuplication = null;
    [SerializeField] private GameObject playerobject = null;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private ActivatePlayerDuplication activatePlayerDuplication;
    public List<GameObject> spawnedPlayers = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // gameManager = this.gameObject;
        playerDuplicationLowerLimit = defaultPlayerDuplicationLowerLimit;
        playerDuplicationUpperLimit = defaultPlayerDuplicationUpperLimit;
    }

    // Update is called once per frame
    void Update()
    {
        playerobject = activatePlayerDuplication.playerObject;
        if (playerCanDuplicate && playerDuplication == null)
        {
            if (stopPlayerDuplication != null) { StopCoroutine(stopPlayerDuplication); }
            playerDuplicationLowerLimit = defaultPlayerDuplicationLowerLimit;
            playerDuplicationUpperLimit = defaultPlayerDuplicationUpperLimit;
            stopPlayerDuplication = null;
            playerDuplication = StartCoroutine(playerDuplicationAddCoroutine());
        }
        if (!playerCanDuplicate && stopPlayerDuplication == null)
        {
            if (playerDuplication != null) { StopCoroutine(playerDuplication); }
            playerDuplicationLowerLimit = defaultPlayerDuplicationLowerLimit;
            playerDuplicationUpperLimit = defaultPlayerDuplicationUpperLimit;
            playerDuplication = null;
            stopPlayerDuplication = StartCoroutine(playerDuplicationRemoveCoroutine());
        }
    }

    IEnumerator playerDuplicationAddCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            timer = Random.Range(playerDuplicationLowerLimit, playerDuplicationUpperLimit);
            yield return new WaitForSeconds(timer);
            GameObject playerInstance = Instantiate(
                playerPrefab,
                new Vector3(
                    playerobject.transform.position.x + (duplicationOffset.x * Random.Range(0.6f, 1.4f) * ((Random.Range(0, 1) > 0.5f) ? 1 : -1)),
                    playerobject.transform.position.y + (duplicationOffset.y * Random.Range(0.8f, 1.6f)),
                    playerobject.transform.position.z
                ),
                Quaternion.Euler(
                    playerobject.transform.rotation.x,
                    playerobject.transform.rotation.y,
                    playerobject.transform.rotation.z
                )
            );

            spawnedPlayers.Add(playerInstance);

            PlayerMovement movementScript = playerInstance.GetComponent<PlayerMovement>();
            //Player Settings
            movementScript.groundJumpforce = Random.Range(movementScript.groundJumpLowerLimit, movementScript.groundJumpUpperLimit);
            movementScript.groundPlayerSpeed = Random.Range(movementScript.groundSpeedLowerLimit, movementScript.groundSpeedUpperLimit);
            movementScript.hitCooldown = Random.Range(movementScript.hitCooldownLowerLimit, movementScript.hitCooldownUpperLimit);
            movementScript.frictionValue = Random.Range(movementScript.frictionValueLowerLimit, movementScript.frictionValueUpperLimit);


            Collider2D playerCollider = playerInstance.GetComponent<Collider2D>();

            playerDuplicationLowerLimit += bufferIncrease;
            playerDuplicationUpperLimit += bufferIncrease;

            yield return null;
        }
    }

    IEnumerator playerDuplicationRemoveCoroutine()
    {
        while (true)
        {
            if (spawnedPlayers.Count > 1)
            {
                yield return new WaitForSeconds(0.5f);
                timer = Random.Range(playerDuplicationLowerLimit, playerDuplicationUpperLimit);
                yield return new WaitForSeconds(timer);
                Destroy(spawnedPlayers[spawnedPlayers.Count - 1]);
                spawnedPlayers.RemoveAt(spawnedPlayers.Count - 1);
                playerDuplicationLowerLimit += bufferIncrease;
                playerDuplicationUpperLimit += bufferIncrease;
                yield return null;
            }
            else
            {
                yield break;
            }
        }
    }
}