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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealthBar = GameObject.Find("playerHealthBar");
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
            levelEnd.orbCollected = true;
            destroyObject = true;
        }
    }

}
