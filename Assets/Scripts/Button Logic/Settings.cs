using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    static public KeyCode[] horizontal = new KeyCode[] { KeyCode.A, KeyCode.LeftArrow, KeyCode.D, KeyCode.RightArrow };
    void Start()
    {

    }

    void Update()
    {

    }

    void ChangeButtonValue()
    {
        if (Input.anyKeyDown)
        {
            
        }
    }

}
