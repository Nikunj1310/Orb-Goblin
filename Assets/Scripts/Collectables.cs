using System.Security;
using TMPro;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] LevelEnd levelEnd;
    [SerializeField] Animator animator;
    public bool destroyObject = false;
    public TextMeshProUGUI orbCollectedText;
    [SerializeField] GameObject playerHealthBar;
    [SerializeField] AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealthBar = GameObject.Find("playerHealthBar");
        audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        orbCollectedText = playerHealthBar.transform.Find("OrbCollected") ?.GetComponent<TextMeshProUGUI>();
        levelEnd = GameObject.Find("End Point").GetComponent<LevelEnd>();
        animator.SetBool("HasBeenCollected", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            levelEnd.currentOrbs++;
            orbCollectedText.text = ($"{levelEnd.currentOrbs}/{levelEnd.OrbsNeeded}");
            animator.SetBool("HasBeenCollected", true);
            audioManager.PlaySFX(audioManager.collectSound);
            levelEnd.orbCollected = true;
            destroyObject = true;
        }
    }

}
