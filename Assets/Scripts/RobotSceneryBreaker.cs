using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSceneryBreaker : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground") || col.gameObject.layer == LayerMask.NameToLayer("Breakable"))
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (col.GetComponent<EnemyBase>() != null)
            {
                WorldManager.Instance.robotHealth -= col.GetComponent<EnemyBase>().damage;
            }
            Destroy(col.gameObject);
        }
    }
}
