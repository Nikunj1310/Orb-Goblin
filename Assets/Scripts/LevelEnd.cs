using UnityEngine;

public class LevelEnd : MonoBehaviour
{

    [SerializeField] int OrbsNeeded;
    public int currentOrbs;
    public bool LevelHasEnded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelHasEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOrbs >= OrbsNeeded)
        {
            Debug.Log("Level Ended!!!");
            LevelHasEnded = true;
        }
    }
}
