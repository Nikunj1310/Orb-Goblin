using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    public int playerHealth = 4;
    [SerializeField] private Slider playerHealthSlider;
    public bool hasBeenHit = false;
    [SerializeField] private PlayerDuplication playerDuplication;
    Coroutine turnItBackOff = null;

    // Update is called once per frame
    void Update()
    {
        if (hasBeenHit && turnItBackOff == null)
        {
            playerHealthSlider.value--;
            turnItBackOff = StartCoroutine(turnOffHasBeenHit());
            playerHealth = (int)playerHealthSlider.value;
        }
        else
        if (playerHealth <= 0)
        {
            playerDuplication.playerCanDuplicate = false;
            for (int i = playerDuplication.spawnedPlayers.Count - 1; i >= 0; i--)
            {
                Debug.Log("Destroying Players");
                Destroy(playerDuplication.spawnedPlayers[i]);
                playerDuplication.spawnedPlayers.RemoveAt(i);
            }
        }
    }

    IEnumerator turnOffHasBeenHit()
    {
        yield return new WaitForEndOfFrame();
        hasBeenHit = false;
        turnItBackOff = null;
    }

    public void playerHealthUpdate()
    {

    }
}
