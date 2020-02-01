using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    /// <summary>
    /// Players can't collect resources for a small amount of time after they're thrown
    /// </summary>
    private float ignorePlayerTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ignorePlayerTimer <= 0)
        {
            
        }
    }

    public void MoveToward(Transform other)
    {
        
    }
}
