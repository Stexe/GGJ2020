using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Station))]
public class Station_Gun : MonoBehaviour
{
    public GameObject robot;
    public float speed;

    public GameObject fireball;
    public GameObject laser;
    public GameObject shield;

    public float fireballCooldown = 0.6f;
    public float laserCooldown = 2;

    public float fireballSpeed = 5;
    public float laserSpeed = 5;

    public Transform spawnPosition;

    public float cooldownTimer;

    public float fireballConsumptionSpeed = 2;
    public float laserConsumptionSpeed = 4;
    public float shieldConsumptionSpeed = 1;

    private Station station;
    private ResourceHolder resourceHolder;

    // Start is called before the first frame update
    void Start()
    {
        station = GetComponent<Station>();
        resourceHolder = GetComponent<ResourceHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (resourceHolder.HasResources())
        {
            //fireballs
            if (resourceHolder.GetHighestResource() == ResourceObject.ResourceType.Wood)
            {
                station.ConsumeTotalResourceAmount(fireballConsumptionSpeed * Time.deltaTime);
                if (cooldownTimer <= 0)
                {
                    GameObject obj = Instantiate(fireball, spawnPosition.position, spawnPosition.rotation);
                    obj.GetComponent<Rigidbody2D>().AddForce(fireballSpeed * (Vector2)obj.transform.right + new Vector2(0, Random.Range(-4, 4)), ForceMode2D.Impulse);
                    cooldownTimer += fireballCooldown;
                }
            }

            //lasers
            if (resourceHolder.GetHighestResource() == ResourceObject.ResourceType.Crystal)
            {
                station.ConsumeTotalResourceAmount(laserConsumptionSpeed * Time.deltaTime);
                if (cooldownTimer <= 0)
                {
                    GameObject obj = Instantiate(laser, spawnPosition.position, spawnPosition.rotation);
                    obj.GetComponent<Rigidbody2D>().AddForce(laserSpeed * obj.transform.right, ForceMode2D.Impulse);
                    cooldownTimer += laserCooldown;
                }
            }

            //shield
            if (resourceHolder.GetHighestResource() == ResourceObject.ResourceType.Metal)
            {
                station.ConsumeTotalResourceAmount(shieldConsumptionSpeed * Time.deltaTime);
                shield.SetActive(true);
            } else
            {
                shield.SetActive(false);
            }
        }
        else
        {
            shield.SetActive(false);
        }
        if (cooldownTimer < 0) cooldownTimer = 0;
    }
}
