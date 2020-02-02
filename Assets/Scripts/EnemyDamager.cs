﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{

    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<EnemyBase>() != null)
        {
            col.GetComponent<EnemyBase>().DecreaseHealth(damage);
            Destroy(gameObject);
        }
    }
}
