using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Button hapticsToggleBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(delegate {
            AudioManager.Instance.SetMasterVolume(masterVolumeSlider.value);
        });
        sfxVolumeSlider.onValueChanged.AddListener(delegate {
            AudioManager.Instance.SetSFXVolume(sfxVolumeSlider.value);
        });
        bgmVolumeSlider.onValueChanged.AddListener(delegate {
            AudioManager.Instance.SetBGMVolume(bgmVolumeSlider.value);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
