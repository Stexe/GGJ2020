using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAttractor : MonoBehaviour
{
    public float distance = 2;
    public float strength = 1;
    /// <summary>
    /// Should this attract even if the resource was just thrown? True for things that consume the resources, generally
    /// </summary>
    public bool ignoreThrownAttractCooldown = false;

    private ResourceHolder resourceHolder;

    private Collider2D[] colliders;

    void Start()
    {
        resourceHolder = GetComponent<ResourceHolder>();
    }

    private void FixedUpdate()
    {
        //TODO: this might be perf heavy? think about changing to find stuff every couple of frames?
        colliders = Physics2D.OverlapCircleAll(transform.position, distance, ~LayerMask.NameToLayer("Resource"));
        foreach(Collider2D collider in colliders) {
            if(collider.GetComponent<ResourceObject>() != null)
            {
                if (collider.GetComponent<ResourceObject>().IsAttractable() || ignoreThrownAttractCooldown)
                {
                    //if that resourceholder is full (optional), don't collect it
                    if (resourceHolder != null && resourceHolder.GetResourceAmount(collider.GetComponent<ResourceObject>().type) >= resourceHolder.maxResources) continue;

                    float adjustedStrength = Mathf.Lerp(0, strength, distance - Vector2.Distance(transform.position, collider.transform.position));
                    collider.GetComponent<Rigidbody2D>().AddForce((transform.position - collider.transform.position).normalized * adjustedStrength, ForceMode2D.Force);
                }
            }
        }
    }
}
