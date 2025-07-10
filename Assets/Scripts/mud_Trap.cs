using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class mud_Trap : MonoBehaviour
{

    [SerializeField] private float maxMudSpeed;
    [SerializeField] private float maxMudJumpForce;
    [SerializeField] private float mudSlowDuration;
    [SerializeField] private float TimeElapsed_mud = 0f;
    [SerializeField] private GameObject gameManager;

    void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().currentJumpForce = maxMudJumpForce;
            if (collision.gameObject.GetComponent<PlayerMovement>().CurrentHorizontalKeys.Any(key => Input.GetKey(key)))
            {
                if (TimeElapsed_mud < mudSlowDuration)
                {
                    TimeElapsed_mud += Time.deltaTime;
                }
                collision.gameObject.GetComponent<PlayerMovement>().currentPlayerSpeed = Mathf.Lerp(maxMudSpeed / 2, 1, TimeElapsed_mud / mudSlowDuration);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerMovement>().currentPlayerSpeed = maxMudSpeed;
                TimeElapsed_mud = 1f;
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().currentJumpForce = collision.gameObject.GetComponent<PlayerMovement>().groundJumpforce;
            TimeElapsed_mud = 0f;
            collision.gameObject.GetComponent<PlayerMovement>().currentPlayerSpeed = collision.gameObject.GetComponent<PlayerMovement>().groundPlayerSpeed;
        }
    }
}
