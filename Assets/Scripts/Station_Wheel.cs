using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Station))]
public class Station_Wheel : MonoBehaviour
{
    public GameObject robot;
    public float speed;

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
        if (resourceHolder.HasResources())
        {
            station.ConsumeTotalResourceAmount(1 * Time.deltaTime);
            robot.transform.Translate(speed*Time.deltaTime, 0, 0);
        }
        else
        {

        }
    }
}
