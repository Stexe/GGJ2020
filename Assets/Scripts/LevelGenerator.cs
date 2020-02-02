using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<SpawnObject> groundObjects = new List<SpawnObject>();
    public List<SpawnObject> skyObjects = new List<SpawnObject>();
    public float spaceBetweenObjects = 8;
    [System.Serializable]
    public struct SpawnObject
    {
        public GameObject prefab;
        //doesn't do anything yet
        //public int weight;

    }

    // Start is called before the first frame update
    void Start()
    {
        Generate(groundObjects, 10, 0, 200, 0);
        Generate(skyObjects, 10, 5, 200, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(List<SpawnObject> objects, float minX, float minY, float maxX, float maxY)
    {
        int loopChecker = 0;
        float currentX = minX;
        while(currentX < maxX)
        {
            //get a random object
            SpawnObject spawnObject = objects[Random.Range(0, objects.Count)];
            GameObject spawned = Instantiate(spawnObject.prefab, new Vector2(currentX, Random.Range(minY, maxY)), Quaternion.identity);
            currentX += GetWidth(spawned) + spaceBetweenObjects;
            loopChecker++;

            if(loopChecker > 300)
            {
                Debug.Log("Something's wrong; breaking loop");
                break;
            }
        }
    }

    float GetWidth(GameObject g)
    {
        float minX = float.PositiveInfinity;
        float maxX = float.NegativeInfinity;
        foreach (Transform child in g.transform)
        {
            minX = Mathf.Min(minX, child.position.x);
            maxX = Mathf.Max(maxX, child.position.x);
        }
        return maxX - minX;
    }
}
