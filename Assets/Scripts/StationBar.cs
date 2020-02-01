using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBar : MonoBehaviour
{

    public GameObject woodBar;
    public GameObject crystalBar;
    public GameObject metalBar;

    public ResourceHolder resourceHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        woodBar.transform.localScale = new Vector3((float)resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Wood) / resourceHolder.maxResources, 1, 1);
        crystalBar.transform.localScale = new Vector3((float)resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Crystal) / resourceHolder.maxResources, 1, 1);
        metalBar.transform.localScale = new Vector3((float)resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Metal) / resourceHolder.maxResources, 1, 1);
    }
}
