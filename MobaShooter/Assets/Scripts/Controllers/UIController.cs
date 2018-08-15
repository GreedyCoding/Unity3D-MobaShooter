using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    //Using this to store if the game is currently paused
    public static bool gameIsPaused = false;
    
    //References to the UI Elemets
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject crosshairUI;
	
	void Update () {

        //Use ESC key to Pause Game
        if (Input.GetKeyDown(KeyCode.Escape)) {

            //If the game is not pause pause the game
            if (!gameIsPaused) {

                PauseGame();
            
            //If the game is paused resume the game
            } else {

                ResumeGame();

            }

        }

	}
        
    public void PauseGame() {

        //Set timescale to 0 to pause the game
        Time.timeScale = 0;
        //Set the according UI elements to active or inactive
        pauseMenuUI.SetActive(true);
        crosshairUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        //Set the bool according to the pause state
        gameIsPaused = true;

    }

    public void ResumeGame() {

        //Set timescale to 1 to resume the game
        Time.timeScale = 1;
        //Set the according UI elements to active or inactive
        pauseMenuUI.SetActive(false);
        crosshairUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        //Set the bool according to the pause state
        gameIsPaused = false;

    }

    public void ShowSettings() {

        //Set the according UI elements to active or inactive
        pauseMenuUI.SetActive(false);
        crosshairUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        //No need to change timescale or the pause state bool because this still happens in the pause menu
        //As the Menu gets closed the timescale and pause state get set 

    }

    public void ReloadScene() {

        //Load the current Scene again
        SceneManager.LoadScene(0);
        
    }

    public void QuitGame() {

        //Quit the game, only works when built
        Application.Quit();
        //Thus logging to the console for now
        Debug.Log("Quitting Application");

    }

}
