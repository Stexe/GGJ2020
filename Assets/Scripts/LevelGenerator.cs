using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<SpawnObject> objects = new List<SpawnObject>();
    [System.Serializable]
    public struct SpawnObject
    {
        public GameObject prefab;
        public int amount;

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Generate(0 + 40 * i, 0, 60 + 40 * i, 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(float minX, float minY, float maxX, float maxY)
    {
        foreach(SpawnObject spawnObject in objects)
        {
            for (int i = 0; i < spawnObject.amount; i++) {
                Instantiate(spawnObject.prefab, new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);
            }
        }
    }
}
