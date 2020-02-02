using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Station))]
public class Station_Points : MonoBehaviour
{
    public float delay;
    public int pointsPerResource = 1;
    float cooldownTimer;

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
        if (resourceHolder.HasResources() && cooldownTimer <= 0)
        {

            station.ConsumeTotalResourceAmount(1);
            WorldManager.Instance.AddScore(pointsPerResource);
            cooldownTimer += delay;
        }
        if (cooldownTimer < 0) cooldownTimer = 0;
    }
}
