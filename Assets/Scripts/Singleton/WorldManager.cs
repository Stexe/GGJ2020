using System;
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
    public bool gameOver = false;

    public float globalTime = 0.0f;
    public float robotHealth = 1.0f;
    public float endTime = 270.0f;
    float endTimeRound;

    public Text globalHUDText;
    public Text endTimeText;
    public Text gameOverText;

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
        endTime = endTime - Time.deltaTime;
        endTimeRound = (float)Math.Round((double)endTime, 0);
        globalTime = globalTime + Time.deltaTime;
        globalHUDText.text = "Score: " + currentScore;
        endTimeText.text = "Time Left: " + endTimeRound;

        if (endTime <= 0 || robotHealth == 0f)
        {
            gameOver = true;
        }
        if (gameOver)
        {
            Time.timeScale = 0.0f;
            gameOverText.gameObject.SetActive(true);
        }

    }

}