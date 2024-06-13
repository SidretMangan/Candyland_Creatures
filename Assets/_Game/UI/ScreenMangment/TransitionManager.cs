using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransitionManager : MonoBehaviour
{   
  
    public RectTransform mainScreen, lvlsuccess, failScreeen; 

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(mainScreen, new Vector3(1.5f, 1.5f, 1.5f), 2f);
    }

   

    public void fadeIn () { 
    
    
    }

    public void fadeOut() {  

    }
    
    public void SwipeScreen()
    {

    }

    public void SlideScreen() { 
    
    
    } 


    
    

}
