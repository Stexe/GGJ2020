using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject titlePanel;
    public GameObject selectionPanel;

    public void StartGame()
    {

        titlePanel.SetActive(false);
        selectionPanel.SetActive(false);
        SceneManager.LoadScene("Main");

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit!");
    }
	
}
