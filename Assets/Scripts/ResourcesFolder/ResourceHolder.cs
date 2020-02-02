using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of how many resources this object is holding
/// </summary>
public class ResourceHolder : MonoBehaviour
{
    public Dictionary<ResourceObject.ResourceType, int> resourceAmounts = new Dictionary<ResourceObject.ResourceType, int>();
    public int maxResources = 40;

    // Start is called before the first frame update
    void Start()
    {
        resourceAmounts.Add(ResourceObject.ResourceType.Wood, 0);
        resourceAmounts.Add(ResourceObject.ResourceType.Crystal, 0);
        resourceAmounts.Add(ResourceObject.ResourceType.Metal, 0);
    }


    public int GetResourceAmount(ResourceObject.ResourceType resource)
    {
        return resourceAmounts[resource];
    }

    public void ModifyResourceAmount(ResourceObject.ResourceType resource, int amount)
    {
        resourceAmounts[resource] += amount;
    }

    public void AddResource(ResourceObject.ResourceType resource)
    {
        ModifyResourceAmount(resource, 1);
    }

    public ResourceObject.ResourceType GetHighestResource()
    {
        if(resourceAmounts[ResourceObject.ResourceType.Wood] >= resourceAmounts[ResourceObject.ResourceType.Crystal] && resourceAmounts[ResourceObject.ResourceType.Wood] >= resourceAmounts[ResourceObject.ResourceType.Metal])
        {
            return ResourceObject.ResourceType.Wood;
        }
        if (resourceAmounts[ResourceObject.ResourceType.Crystal] >= resourceAmounts[ResourceObject.ResourceType.Wood] && resourceAmounts[ResourceObject.ResourceType.Wood] >= resourceAmounts[ResourceObject.ResourceType.Metal])
        {
            return ResourceObject.ResourceType.Crystal;
        }
        if (resourceAmounts[ResourceObject.ResourceType.Metal] > 0)
        {
            return ResourceObject.ResourceType.Metal;
        }
        //no highest resource
        return ResourceObject.ResourceType.Wood;
    }

    public bool HasResources()
    {
        return resourceAmounts[ResourceObject.ResourceType.Wood] > 0 || resourceAmounts[ResourceObject.ResourceType.Crystal] > 0 || resourceAmounts[ResourceObject.ResourceType.Metal] > 0;
    }
}
