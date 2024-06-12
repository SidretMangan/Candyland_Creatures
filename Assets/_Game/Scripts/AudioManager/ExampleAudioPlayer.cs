using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//An example class of how to play background music using the Audio Manager. It will be the same for SFX.
public class ExampleAudioPlayer : MonoBehaviour
{

    //All game objects that make sound will contain references to the sounds that they will make as fields which will be set in the inspector.
    [SerializeField] private AudioClip bgmToPlay;

    // Start is called before the first frame update
    void Start()
    {
        if (bgmToPlay != null) AudioManager.Instance.PlayBGM(bgmToPlay);
    }
}
