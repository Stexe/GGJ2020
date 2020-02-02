using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameController : MonoBehaviour
{

    [SerializeField] private HealthBar healthBar;


    private void Start()
    {
        StartCoroutine(LaterStart(0.5f));

    }

    IEnumerator LaterStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
       /*
        FunctionPeriodic.Create(() =>
        {
            if (WorldManager.Instance.robotHealth > 0.001f)
            {
                //WorldManager.Instance.robotHealth -= .01f;  //Testing decay
                healthBar.SetSize(WorldManager.Instance.robotHealth);

                if (WorldManager.Instance.robotHealth < 0.3f)
                {
                    if ((WorldManager.Instance.robotHealth * 100) % 3 == 0)
                    {
                        healthBar.SetColor(Color.white);
                    }
                    else
                    {
                        healthBar.SetColor(Color.red);
                    }
                }
            }
        }, .03f);
        */
    }
}
