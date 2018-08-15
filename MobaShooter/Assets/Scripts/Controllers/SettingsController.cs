using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {


    private string volumeInputText;

    //Reference to the Audio Mixer
    public AudioMixer audioMixer;

    //Refernce to the Settings Input fields
    public InputField volumeInputField;
    public InputField sensitivityInputField;

    //Reference to the Settings Sliders
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    //Reference to the Settings Dropdowns
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start() {
    
    //Resolution Stuff
        
        //Store all available resolutions in the resolutions array
        resolutions = Screen.resolutions;
        //Clear all options in the resolution dropdown so we can refill it with our resolutions
        resolutionDropdown.ClearOptions();

        //Creating a List of strings for all the options because the Dropdown only takes in strings
        List<string> options = new List<string>();

        //Creating an variable to store the index of the resolution the system is currently running on
        int currentResolutionIndex = 0;

        //Looping through all the resolutions
        for (int i = 0; i < resolutions.Length; i++) {

            //Create the corresponding string
            string option = resolutions[i].width + " x " + resolutions[i].height;
            //and add it to the options array
            options.Add(option);

            //If the resolution we look at matches the current screen resolution
            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height) {

                //set the currentResolutionIndex to the resolution index we are currently looking at
                currentResolutionIndex = i;

            }

        }

        //Add all created options to the dropdown
        resolutionDropdown.AddOptions(options);
        //Set the value of the dropdown to the current resolution
        resolutionDropdown.value = currentResolutionIndex;
        //Refresh the value so it represents the actual value
        resolutionDropdown.RefreshShownValue();

    //VolumeStuff

        //Set Volume to the saved value, if there is no saved value it gets set to max value
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeSetting", volumeSlider.maxValue);
        //Adding a listener to the Volumeslider which calls SetVolume if the value of the slider is changed
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(volumeSlider.value); });
        //Setting the text of the input field to the current value
        volumeInputField.text = volumeSlider.value.ToString();
        //Adding a listener to the Volumeinputfield which calls Setvolume
        volumeInputField.onValueChanged.AddListener(delegate { SetVolume(StringToFloat(volumeInputField.text)); });
       
    //Sensitivity Stuff

        //Set Sensitivity to the saved Value, if there is no saved value it gets set to max value
        sensitivitySlider.value = PlayerPrefs.GetFloat("SensitivitySetting", sensitivitySlider.maxValue);
        //And set the mouseSensitivity to the value of the slider
        PlayerController.mouseSensitivity = sensitivitySlider.value;
        //Adding a listener to the Sensitivityslider which calls SetSensitivity if the value of the slider is changed
        sensitivitySlider.onValueChanged.AddListener(delegate { SetSensitivity(sensitivitySlider.value); });
        //Setting the text of the input field to the current value
        sensitivityInputField.text = sensitivitySlider.value.ToString();
        //Adding a listener to the Sensitivityinputfield which calls SetSensitivity
        sensitivityInputField.onValueChanged.AddListener(delegate { SetSensitivity(StringToFloat(sensitivityInputField.text)); });

    }

    private void SetVolume(float volume) {

        //Set the float "volume" from the AudioMixer to the corrected volume value calculatedd by ReturnMixerValue()
        audioMixer.SetFloat("volume", ReturnMixerValue(volume));
        //Change the text of the input field to disply the current setting
        volumeInputField.text = volumeSlider.value.ToString();
        //Save the current volume to the Playerpreferences
        PlayerPrefs.SetFloat("VolumeSetting", volume);

    }

    private static float ReturnMixerValue(float value) {

        //Remapping the Range of the slider(0-100) to the range of the mixer(-80-0) by doing the following 
        //calculation (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        return (value - 0) / (100 - 0) * (0 - -80) + -80;

    }

    private void SetSensitivity(float sensitivity) {

        //Setting mouseSens of the Playercontroller to the input sensitivity
        PlayerController.mouseSensitivity = sensitivity;
        //and changin the text of the input field to represent the value
        sensitivityInputField.text = sensitivitySlider.value.ToString();
        //Save the current sensitivity to the Playerpreferences
        PlayerPrefs.SetFloat("SensitivitySetting", sensitivity);
    }

    public void SetQuality(int qualityIndex) {

        //Setting the quality of the game to the input qualityIndex from qualityDropdown in the Settings UI
        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen(bool isFullscreen) {

        //Setting the game fullscreen or windowed according to a toggle in the SettingsUI
        Screen.fullScreen = isFullscreen;

    }
    
    private float StringToFloat(string inputString) {

        float output;

        if(float.TryParse(inputString, out output)) {
            return output;
        } else {
            return 0f;
        }

    }

}
