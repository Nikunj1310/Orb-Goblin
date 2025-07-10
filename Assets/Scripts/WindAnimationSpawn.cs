using UnityEngine;
using System.Collections.Generic;

public class WindAnimationSpawn : MonoBehaviour
{
    [Header("Wind Animation Spawn Settings")]
    [SerializeField] private GameObject windAni;
    [SerializeField] private int minSpawnCount = 9;
    [SerializeField] private int maxSpawnCount = 12;
    [SerializeField] private BoxCollider2D spawnRegion;
    [SerializeField] private string PlayerTag = "Player";

    private List<GameObject> spawnedWinds = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnRegion = this.gameObject.GetComponent<BoxCollider2D>();
        if (!spawnRegion || !spawnRegion.isTrigger)
        {
            Debug.LogWarning("WindSpawner requires a BoxCollider2D with 'Is Trigger' enabled.");
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == PlayerTag)
        {
            SpawnWinds(windAni);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == PlayerTag)
        {
            DestroyAllWinds();
        }
    }

    void SpawnWinds(GameObject animation)
    {
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1);
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPos = GetRandomPostitonInBounds();
            GameObject windInstance = Instantiate(animation, spawnPos, Quaternion.identity);
            spawnedWinds.Add(windInstance);
        }
    }

    void DestroyAllWinds()
    {
        foreach (GameObject wind in spawnedWinds)
        {
            if (wind != null)
                Destroy(wind);
        }
        spawnedWinds.Clear(); // Clear the list after destroying
    }

    Vector2 GetRandomPostitonInBounds()
    {
        Bounds bounds = spawnRegion.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
