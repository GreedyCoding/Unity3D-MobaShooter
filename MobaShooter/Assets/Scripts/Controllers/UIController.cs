using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    //Using this to store if the game is currently paused
    public static bool gameIsPaused = false;

    //Reference to the textboxes we write the information to
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;

    //References to the UI Elemets
    public GameObject crosshairUI;
    public GameObject playerInfoUI;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

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

        if (Time.timeScale == 0) {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        } else {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        RefreshHUDVaules();

	}
        
    public void PauseGame() {

        //Set timescale to 0 to pause the game
        Time.timeScale = 0;
        //Set the according UI elements to active or inactive
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        crosshairUI.SetActive(false);
        playerInfoUI.SetActive(false);
        //Set the bool according to the pause state
        gameIsPaused = true;

    }

    public void ResumeGame() {

        //Set timescale to 1 to resume the game
        Time.timeScale = 1;
        //Set the according UI elements to active or inactive
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        crosshairUI.SetActive(true);
        playerInfoUI.SetActive(true);
        //Set the bool according to the pause state
        gameIsPaused = false;

    }

    public void ShowSettings() {

        //Set the according UI elements to active or inactive
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        crosshairUI.SetActive(false);
        playerInfoUI.SetActive(false);
        //No need to change timescale or the pause state bool because this still happens in the pause menu
        //As the Menu gets closed the timescale and pause state get set 

    }

    public void ReloadScene() {

        //Load the current Scene again
        SceneManager.LoadScene(0);
        //And set the timescale back to 1 so the game is not paused anymore
        Time.timeScale = 1;

    }

    public void QuitGame() {

        //Quit the game, only works when built
        Application.Quit();
        //Thus logging to the console for now
        Debug.Log("Quitting Application");

    }

    void RefreshHUDVaules() {

        healthText.text = PlayerController.health.ToString();
        ammoText.text = Gun.currentAmmo.ToString() + "/" + Gun.maxAmmo.ToString();


    }

}
