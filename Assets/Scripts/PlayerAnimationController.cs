using UnityEngine;
using System.Linq;
using System.Collections;
using System;

public class PlayerAnimationController : MonoBehaviour
{

    public PlayerMovement playerMovScrit;
    [SerializeField] GameObject gameManager;
    public Player_Health player_Health;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovScrit = this.gameObject.GetComponent<PlayerMovement>();
        animator = this.gameObject.GetComponent<Animator>();
        playerRb = this.gameObject.GetComponent<Rigidbody2D>();
        animator.SetInteger("Jump", 0);
        gameManager = GameObject.Find("GameManager");
        player_Health = gameManager.GetComponent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(playerMovScrit.canJump);
        if (playerMovScrit.CurrentHorizontalKeys.Any((Key) => Input.GetKey(Key)))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (playerMovScrit.canJump)
        {
            animator.SetInteger("Jump", 0);
        }
        else
        {
            if (playerRb.linearVelocityY > 0.1f)
            {
                animator.SetInteger("Jump", 5);
            }
            else if (playerRb.linearVelocityY < -0.1f)
            {
                animator.SetInteger("Jump", -5);
            }
            else
            {
                animator.SetInteger("Jump", 0);
            }
        }

        if (player_Health.hasBeenHit)
        {
            animator.SetBool("TookDamage", true);

        }
        else
        {
            animator.SetBool("TookDamage", false);

        }
    }

}