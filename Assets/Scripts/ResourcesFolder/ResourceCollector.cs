using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceCollector : MonoBehaviour
{

    /// <summary>
    /// From how far away can this object collect resources?
    /// </summary>
    public float resourceCollectionDistance = 0.5f;
    /// <summary>
    /// Optional ResourceHolder to connect this to
    /// </summary>
    private ResourceHolder resourceHolder;

    [System.Serializable]
    public class ResourceCollectEvent : UnityEvent<ResourceObject.ResourceType> { }
    public ResourceCollectEvent onCollect;


    private Collider2D[] resourceColliders;

    // Start is called before the first frame update
    void Start()
    {
        resourceHolder = GetComponent<ResourceHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        //collect nearby resources
        resourceColliders = Physics2D.OverlapCircleAll(transform.position, resourceCollectionDistance, ~LayerMask.NameToLayer("Resource"));
        foreach (Collider2D collider in resourceColliders)
        {
            if (collider.GetComponent<ResourceObject>() != null && collider.GetComponent<ResourceObject>().IsAttractable())
            {
                //if that resourceholder is full (optional), don't collect it
                if (resourceHolder != null && resourceHolder.GetResourceAmount(collider.GetComponent<ResourceObject>().type) >= resourceHolder.maxResources) continue;

                onCollect.Invoke(collider.GetComponent<ResourceObject>().type);
                collider.GetComponent<ResourceObject>().Collect();
            }
        }
    }
}
