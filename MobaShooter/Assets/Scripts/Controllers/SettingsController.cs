using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

    //Reference to the Audio Mixer
    public AudioMixer audioMixer;

    //Refernce to the Settings Input fields
    public InputField volumeInputField;
    public InputField sensitivityInputField;

    //Reference to the Settings Sliders
    public Slider volumeSlider;

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
        volumeSlider.value = volumeSlider.maxValue;
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(volumeSlider.value); });
     
      
    //Sensitivity Stuff
        

    }

    public void SetVolume(float volume) {

        float mixerVolume = ReturnMixerValue(volume);
        audioMixer.SetFloat("volume", mixerVolume);
        volumeInputField.text = volumeSlider.value.ToString();

    }

    private static float ReturnMixerValue(float value) {

        //Remapping the Range of the slider(0-100) to the range of the mixer(-80-0) by doing the following calculation
        //return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        return (value - 0) / (100 - 0) * (0 - -80) + -80;
    }


    public void SetQuality(int qualityIndex) {

        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen(bool isFullscreen) {

        Screen.fullScreen = isFullscreen;

    }

}
