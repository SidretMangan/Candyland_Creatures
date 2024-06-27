using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    TransitionManager fade;
    private int nextScene;

    // Start is called before the first frame update
     private void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1; 
        fade = FindObjectOfType<TransitionManager>();
    }


    public IEnumerator _changeScene()
    {
        fade.fadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);
    }

    public void Change()
    {
        StartCoroutine(_changeScene());
    }

} 
