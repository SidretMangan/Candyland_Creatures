using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip tenseClip;
    [SerializeField] private AudioClip neutralClip;
    [SerializeField] private AudioClip happyClip;
    [SerializeField] private Button testBtn;

    private void Awake() {
        testBtn.onClick.AddListener(() => {
            AudioManager.Instance.TransitionLevelMusic();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayLevelMusic(tenseClip, neutralClip, happyClip);
    }
}
