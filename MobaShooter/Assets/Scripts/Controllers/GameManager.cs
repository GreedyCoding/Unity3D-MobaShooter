using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager GM;

    public KeyCode shootKey { get; set; }
    public KeyCode shootAlternateFireKey { get; set; }
    public KeyCode jumpKey { get; set; }
    public KeyCode crouchKey { get; set; }
    public KeyCode abilityOneKey { get; set; }
    public KeyCode abilityTwoKey { get; set; }
    public KeyCode ultimateKey { get; set; }


    void Awake() {

        //If there is no gamemanager assinged
        if (GM == null) {

            //Assign this GameManager; DontDestroyOnLoad makes the object target not
            //to be destroyed automatically when loading a new scene
            DontDestroyOnLoad(gameObject);
            GM = this;

        } else if (GM != this) {

            //Destroy the GameManager if it is not this one
            Destroy(gameObject);

        }

        shootKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shootKey", "Mouse0"));
        shootAlternateFireKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shootAlternateFireKey", "Mouse1"));
        jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        crouchKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouchKey", "LeftControl"));
        abilityOneKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("abilityOneKey", "LeftShift"));
        abilityTwoKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("abilityTwoKey", "E"));
        ultimateKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ultimateKey", "Q"));


    }

}
