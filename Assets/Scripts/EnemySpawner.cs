using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float height = 20;
    public List<GameObject> enemyPrefabs;
    public float spawnDelay = 5;

    private float spawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer < 0)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector2(transform.position.x, transform.position.y + Random.Range(0, height)), Quaternion.identity);
            spawnTimer = spawnDelay;
        }

        spawnDelay -= Time.deltaTime / 60;    
    }
}
