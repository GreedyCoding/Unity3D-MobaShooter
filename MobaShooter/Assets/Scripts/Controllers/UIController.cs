using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject crosshairUI;
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (gameIsPaused) {

                ResumeGame();

            } else {

                PauseGame();

            }

        }

	}

    public void PauseGame() {

        pauseMenuUI.SetActive(true);
        crosshairUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 0;
        gameIsPaused = true;

    }
    
    public void ResumeGame() {

        pauseMenuUI.SetActive(false);
        crosshairUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;

    }

    public void ShowSettings() {

        pauseMenuUI.SetActive(false);
        crosshairUI.SetActive(false);
        settingsMenuUI.SetActive(true);

    }

    public void QuitGame() {

        Application.Quit();
        Debug.Log("Quitting Application");

    }

}
