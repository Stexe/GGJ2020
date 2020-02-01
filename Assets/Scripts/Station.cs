using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceHolder))]
public class Station : MonoBehaviour
{
    ResourceHolder resourceHolder;
    float partialResourceConsumption = 0;
    ResourceObject.ResourceType currentResourceType;

    // Start is called before the first frame update
    void Start()
    {
        resourceHolder = GetComponent<ResourceHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConsumeTotalResourceAmount(float amount)
    {
        amount += partialResourceConsumption;
        while(amount >= 1)
        {
            if (!resourceHolder.HasResources()) break;
            ConsumeNextResource();
            amount--;
        }
        partialResourceConsumption = amount;

        if (!resourceHolder.HasResources()) partialResourceConsumption = 0;
    }

    private void ConsumeNextResource()
    {
        resourceHolder.ModifyResourceAmount(currentResourceType, -1);
        switch(currentResourceType)
        {
            case ResourceObject.ResourceType.Wood:
                if (resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Crystal) > 0) currentResourceType = ResourceObject.ResourceType.Crystal;
                else if (resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Metal) > 0) currentResourceType = ResourceObject.ResourceType.Metal;
                break;
            case ResourceObject.ResourceType.Crystal:
                if (resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Metal) > 0) currentResourceType = ResourceObject.ResourceType.Metal;
                else if (resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Wood) > 0) currentResourceType = ResourceObject.ResourceType.Wood;
                break;
            case ResourceObject.ResourceType.Metal:
                if (resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Wood) > 0) currentResourceType = ResourceObject.ResourceType.Wood;
                else if (resourceHolder.GetResourceAmount(ResourceObject.ResourceType.Crystal) > 0) currentResourceType = ResourceObject.ResourceType.Crystal;
                break;
        }
    }


}
