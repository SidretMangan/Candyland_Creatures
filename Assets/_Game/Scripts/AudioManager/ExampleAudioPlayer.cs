using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAudioPlayer : MonoBehaviour
{

    [SerializeField] private AudioClip bgmToPlay;
    // Start is called before the first frame update

    void Start()
    {
        if (bgmToPlay != null) AudioManager.Instance.PlayBGM(bgmToPlay);
    }
}
