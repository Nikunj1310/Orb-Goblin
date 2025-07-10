using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{

    public int OrbsNeeded;
    public int currentOrbs = 0;
    public bool LevelHasEnded = false;
    public bool orbCollected = false;
    public TextMeshProUGUI orbCollectedText;
    [SerializeField] GameObject playerHealthBar;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        playerHealthBar = GameObject.Find("playerHealthBar");
        orbCollectedText = playerHealthBar.transform.Find("OrbCollected") ?.GetComponent<TextMeshProUGUI>();
        orbCollectedText.text = ($"{currentOrbs}/{OrbsNeeded}");
        LevelHasEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (orbCollected)
        {
            animator.SetBool("HasCollectedOrb", true);
            orbCollected = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (currentOrbs >= OrbsNeeded)
            {
                Debug.Log("Level Ended!!!");
                LevelHasEnded = true;
                animator.SetBool("HasCollectedAllOrbs", true);
            }
        }
    }
}
