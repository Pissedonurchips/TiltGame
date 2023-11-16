using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Volume : MonoBehaviour
{
    public Slider volume;
    public TextMeshProUGUI volumeLevelText;
    public float volumeLevel;

    private void Start()
    {
        LoadVolume();
    }

    public void VolumeSliderDisplay(float volume)
    {
        volumeLevelText.text = volume.ToString("0.0");
    }

    public void SaveVolume()
    {
        volumeLevel = volume.value;
        PlayerPrefs.SetFloat("VolumeLevel", volumeLevel);
        LoadVolume();
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("VolumeLevel"))
        {
            volumeLevel = PlayerPrefs.GetFloat("VolumeLevel");
            volume.value = volumeLevel;
            AudioListener.volume = volumeLevel;
        }
        else
        {
            volumeLevel = 0.5f;
            volume.value = volumeLevel;
            AudioListener.volume = volumeLevel;

            PlayerPrefs.SetFloat("VolumeLevel", volumeLevel);
        }
    }
}