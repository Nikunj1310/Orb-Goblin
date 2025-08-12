using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraShakeTrigger : MonoBehaviour
{
    public bool shakeTrigger = false;
    public CinemachineImpulseSource impulseSource;

    void Reset()
    {
        // Automatically assign the impulse source if on the same GameObject
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        if (shakeTrigger)
        {
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
            }
            else
            {
                Debug.LogWarning("Impulse Source is not assigned!");
            }

            shakeTrigger = false; // Reset the trigger so it only shakes once
        }
    }

}
