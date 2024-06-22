using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider, sfxVolumeSlider, bgmVolumeSlider;
    [SerializeField] private Button hapticsToggleBtn, openBtn, closeBtn, masterVolumeMuteBtn, sfxVolumeMuteBtn, bgmVolumeMuteBtn;
    [SerializeField] private GameObject settingsMenuObject;
    [SerializeField] private TextMeshProUGUI hapticsTextbox;
    [SerializeField] private bool hapticsActivated, masterMuted, sfxMuted, bgmMuted;
    
    // Start is called before the first frame update
    void Awake()
    {
        hapticsActivated = true;
        masterMuted = false;
        sfxMuted = false;
        bgmMuted = false;

        masterVolumeSlider.onValueChanged.AddListener(delegate {
            AudioManager.Instance.SetMasterVolume(masterVolumeSlider.value);
        });
        sfxVolumeSlider.onValueChanged.AddListener(delegate {
            AudioManager.Instance.SetSFXVolume(sfxVolumeSlider.value);
        });
        bgmVolumeSlider.onValueChanged.AddListener(delegate {
            AudioManager.Instance.SetBGMVolume(bgmVolumeSlider.value);
        });

        openBtn.onClick.AddListener(() => {
            settingsMenuObject.SetActive(true);
        });
        closeBtn.onClick.AddListener(() => {
            settingsMenuObject.SetActive(false);
        });
        hapticsToggleBtn.onClick.AddListener(() => {
            hapticsActivated = !hapticsActivated;
            if (hapticsTextbox != null) {
                hapticsTextbox.text = hapticsActivated ? "Haptics On" : "Haptics Off";
                hapticsToggleBtn.GetComponentInChildren<TextMeshProUGUI>().text = hapticsActivated ? "O" : "X";
            } else {
                Debug.LogError("Haptics textbox not found!");
            }
        });
        masterVolumeMuteBtn.onClick.AddListener(() => {
            masterMuted = !masterMuted;
            AudioManager.Instance.SetMuteOnBGM(masterMuted || bgmMuted);
            AudioManager.Instance.SetMuteOnSFX(masterMuted || sfxMuted);
            masterVolumeMuteBtn.GetComponentInChildren<TextMeshProUGUI>().text = !masterMuted ? "O" : "X";
        });
        sfxVolumeMuteBtn.onClick.AddListener(() => {
            sfxMuted = !sfxMuted;
            AudioManager.Instance.SetMuteOnSFX(masterMuted || sfxMuted);
            sfxVolumeMuteBtn.GetComponentInChildren<TextMeshProUGUI>().text = !sfxMuted ? "O" : "X";
        });
        bgmVolumeMuteBtn.onClick.AddListener(() => {
            bgmMuted = !bgmMuted;
            AudioManager.Instance.SetMuteOnBGM(masterMuted || bgmMuted);
            bgmVolumeMuteBtn.GetComponentInChildren<TextMeshProUGUI>().text = !bgmMuted ? "O" : "X";
        });

        settingsMenuObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
