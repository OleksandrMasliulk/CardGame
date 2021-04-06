using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private int resolution;
    public Dropdown resolutionDropdown;
    private int graphicsQuality;
    public Dropdown graphicsQualityDropdown;
    private bool fullscreen;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;

    private void Start()
    {
        InitResolution();
        if (Screen.fullScreen)
            fullscreenToggle.isOn = true;
        else
            fullscreenToggle.isOn = false;

        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();

        GetSettings();
    }

    public void ApplyNewSettings()
    {
        GetSettings();
        SetGraphicsQuality();
        SetFullscreen();
        SetResolution(resolution);
    }

    public void RevertSettings()
    {
        if (fullscreen)
            fullscreenToggle.isOn = true;
        else
            fullscreenToggle.isOn = false;

        resolutionDropdown.value = resolution;
        graphicsQualityDropdown.value = graphicsQuality;
    }

    void GetSettings()
    {
        if (fullscreenToggle.isOn)
        {
            fullscreen = true;
        }
        else
        {
            fullscreen = false;
        }
        resolution = resolutionDropdown.value;
        graphicsQuality = graphicsQualityDropdown.value;
    }

    void InitResolution()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && fullscreen)
                resolution = i;
            else if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height && !fullscreen)
                resolution = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolution;
        resolutionDropdown.RefreshShownValue();
    }

    void SetGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsQualityDropdown.value);
    }

    void SetFullscreen()
    {
        Screen.fullScreen = fullscreen;
    }

    void SetResolution(int index)
    {
        Resolution newResolution = resolutions[index];
        Screen.SetResolution(newResolution.width, newResolution.height, fullscreen);
    }

    private void OnDisable()
    {
        RevertSettings();
    }
}

