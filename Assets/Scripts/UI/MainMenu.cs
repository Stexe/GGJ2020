using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject titlePanel;
    public GameObject selectionPanel;
    public GameObject titleBackground;
    public SpriteRenderer titleBackgroundOld;

    public void Start()
    {
    }

    public void StartGame()
    {
        StartCoroutine(RustTitle(1.1f));

    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main");
        Application.Quit();
        Debug.Log("Game quit!");
    }

    IEnumerator RustTitle(float waitTime)
    {
                // loop over 1 second
                for (float i = 0; i <= 1; i += Time.deltaTime)
                {
                    // set color with i as alpha
                    var tempColor = titleBackgroundOld.color;
                    tempColor.a = i;
                    titleBackgroundOld.color = tempColor;
                    yield return null;
                }
        titlePanel.SetActive(false);
        selectionPanel.SetActive(false);
        SceneManager.LoadScene("Main");
    }	
}
