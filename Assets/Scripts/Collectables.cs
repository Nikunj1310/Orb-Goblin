using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] LevelEnd levelEnd;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            animator.SetBool("HasBeenCollected", true);
        }
    }

}
