using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public enum ResourceType { Wood, Crystal, Metal };

    public ResourceType type;
    public GameObject collectParticlesPrefab;
    public Collider2D spawningCollider;

    private Rigidbody2D rb;
    /// <summary>
    /// Players can't collect resources for a small amount of time after they're thrown
    /// </summary>
    private float preventAttractionTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(preventAttractionTimer > 0)
        {
            preventAttractionTimer -= Time.deltaTime;
        }

        if (spawningCollider != null)
        {
            if (!IsAttractable())
            {
                Physics2D.IgnoreCollision(spawningCollider, GetComponent<Collider2D>());
            }
            else
            {
                Physics2D.IgnoreCollision(spawningCollider, GetComponent<Collider2D>(), false);
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Can this resource be attracted? False if it was just thrown by the player
    /// </summary>
    /// <returns></returns>
    public bool IsAttractable()
    {
        return preventAttractionTimer <= 0;
    }

    /// <summary>
    /// Prevent attraction of this resource for an amount of time
    /// </summary>
    /// <param name="time"></param>
    public void SetPreventAttractionTimer(float time)
    {
        preventAttractionTimer = time;
    }

    /// <summary>
    /// Collect this resource object (destroys it, plays particles?)
    /// </summary>
    public void Collect()
    {
        if(collectParticlesPrefab != null) Instantiate(collectParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
