using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceThrower : MonoBehaviour
{
    public int woodAmount = 0;
    public int crystalAmount = 0;
    public int metalAmount = 0;
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

    private CharacterController characterController;
    private float throwTimer;
    private float throwDelay;
    private float throwDelayFalloffTimer;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null) Debug.LogError("No CharacterController on character?");

        selectedResource = woodPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        //Is the character throwing resources?
        if(characterController.IsThrowingResource())
        {
            throwDelayFalloffTimer += Time.deltaTime;
            throwDelay = Mathf.Lerp(throwDelayMax, throwDelayMin, throwDelayFalloffTimer / throwDelayFalloffTime);

            if (throwTimer <= 0)
            {
                ResourceObject spawnedresource = Instantiate(selectedResource, transform.position, Quaternion.identity);
                spawnedresource.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-spinMax, spinMax), ForceMode2D.Impulse);
                spawnedresource.GetComponent<Rigidbody2D>().velocity = characterController.GetComponent<Rigidbody2D>().velocity;
                spawnedresource.GetComponent<Rigidbody2D>().AddForce(characterController.GetAimDirection() * (strength - Random.Range(0, strengthVariation)), ForceMode2D.Impulse);

                //set the throw timer to the throw delay (plus any negative value of the timer, so the timer acts the same over time regardless of framerate)
                throwTimer = throwDelay + throwTimer;
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
       
    }
}
