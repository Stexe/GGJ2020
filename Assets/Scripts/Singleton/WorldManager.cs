using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldManager : Singleton<WorldManager>
{

    // Use with:  WorldManager.Instance.

    public bool gameStarted = false;
    public bool isDead = false;

    public float globalTime = 0.0f;
    public float robotHealth = 1.0f;

    public Text globalHUDText;

    //High score variables////////////
    public float highScore;
    public float currentScore;
    public int difficultySettingSpawn = 1;
    public float timeToBeat;
    Scene myScene;
    //////////////////////////////////

    public void Start()
    {
        StartCoroutine(LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        myScene = SceneManager.GetActiveScene();
        // scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    public void Update()
    {

        globalTime = globalTime + Time.deltaTime;
        globalHUDText.text = "Score: " + currentScore;

    }

}