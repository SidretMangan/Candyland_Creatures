using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;


public class TransitionManager : MonoBehaviour
{

    public CanvasGroup sceneScreen;
    public bool fadein = false;
    public bool fadeout = false; 

    public float duration = 1.0f;

    private void Update()
    {
        if (fadein == true)
        {
            if (sceneScreen.alpha < 1 )
            {
                sceneScreen.alpha += duration * Time.deltaTime;
                if (sceneScreen.alpha >= 1)
                {
                    fadein = false;
                } 
            }
        }

        if (fadeout == true)
        {
            if (sceneScreen.alpha >= 0)
            {
                sceneScreen.alpha -= duration * Time.deltaTime;
                if (sceneScreen.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }

    }



    public void fadeIn () {

        fadein = true; 
  
    }

    public void fadeOut() {


        fadeout = true; 
    }
    
  
}
