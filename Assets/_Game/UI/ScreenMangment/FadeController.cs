using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    TransitionManager fade; 
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<TransitionManager> ();
        fade.fadeOut(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
