using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceThrower : MonoBehaviour
{
    public Dictionary<ResourceObject.ResourceType, int> resourceAmounts = new Dictionary<ResourceObject.ResourceType, int>();
    public ResourceObject woodPrefab;
    public ResourceObject crystalPrefab;
    public ResourceObject metalPrefab;
    //TODO: add others
    public ResourceObject selectedResource;
    /// <summary>
    /// Delay in between throwing resources
    /// </summary>
    public float throwDelayMax = 0.2f;
    /// <summary>
    /// Minimum delay between throwing resources
    /// </summary>
    public float throwDelayMin = 0.02f;
    /// <summary>
    /// How long does it take to reach the minimum throw delay?
    /// </summary>
    public float throwDelayFalloffTime = 4;
    /// <summary>
    /// How much can the resources spin when thrown?
    /// </summary>
    public float spinMax = 100;
    /// <summary>
    /// The force applied as a throw
    /// </summary>
    public float strength = 10;
    /// <summary>
    /// The random variation in throw strength
    /// </summary>
    public float strengthVariation = 2;
    /// <summary>
    /// From how far away can this player collect resources?
    /// </summary>
    public float resourceCollectionDistance = 0.5f;

    private CharacterController characterController;
    private float throwTimer;
    private float throwDelay;
    private float throwDelayFalloffTimer;

    private Collider2D[] resourceColliders;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null) Debug.LogError("No CharacterController on character?");

        selectedResource = woodPrefab;
        resourceAmounts.Add(ResourceObject.ResourceType.Wood, 0);
        resourceAmounts.Add(ResourceObject.ResourceType.Crystal, 0);
        resourceAmounts.Add(ResourceObject.ResourceType.Metal, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Is the character throwing resources?
        if(characterController.IsThrowingResource())
        {
            throwDelayFalloffTimer += Time.deltaTime;
            throwDelay = Mathf.Lerp(throwDelayMax, throwDelayMin, throwDelayFalloffTimer / throwDelayFalloffTime);

            if (throwTimer <= 0 && GetResourceAmount(selectedResource.type) > 0)
            {
                ResourceObject spawnedresource = Instantiate(selectedResource, transform.position, Quaternion.identity);
                spawnedresource.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-spinMax, spinMax), ForceMode2D.Impulse);
                spawnedresource.GetComponent<Rigidbody2D>().velocity = characterController.GetComponent<Rigidbody2D>().velocity;
                spawnedresource.GetComponent<Rigidbody2D>().AddForce(characterController.GetAimDirection() * (strength - Random.Range(0, strengthVariation)), ForceMode2D.Impulse);
                spawnedresource.SetPreventAttractionTimer(0.5f);
                ModifyResourceAmount(spawnedresource.type, -1);

                //set the throw timer to the throw delay (plus any negative value of the timer, so the timer acts the same over time regardless of framerate)
                throwTimer = throwDelay + throwTimer;
            } 
            else if(GetResourceAmount(selectedResource.type) <= 0)
            {
                //TODO: add some kind of indication that you're out?
            }
        } 
        else
        {
            throwDelayFalloffTimer = 0;
        }
        if (throwTimer <= 0)
        {
            throwTimer = 0;
        }
        if (throwTimer > 0)
        {
            throwTimer -= Time.deltaTime;
        }


        //collect nearby resources
        resourceColliders = Physics2D.OverlapCircleAll(transform.position, resourceCollectionDistance, ~LayerMask.NameToLayer("Resource"));
        foreach (Collider2D collider in resourceColliders)
        {
            if (collider.GetComponent<ResourceObject>() != null && collider.GetComponent<ResourceObject>().IsAttractable())
            {
                ModifyResourceAmount(collider.GetComponent<ResourceObject>().type, 1);
                collider.GetComponent<ResourceObject>().Collect();
            }
        }

    }

    public int GetResourceAmount(ResourceObject.ResourceType resource)
    {
        return resourceAmounts[resource];
    }

    public void ModifyResourceAmount(ResourceObject.ResourceType resource, int amount)
    {
        resourceAmounts[resource] += amount;
    }
}
