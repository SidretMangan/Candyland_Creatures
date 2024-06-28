using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneTransitionButton : MonoBehaviour
{

    private Button thisBtn;
    [SerializeField] private string sceneToTransitionToName;

    private void Awake() {
        thisBtn = GetComponent<Button>();
        thisBtn.onClick.AddListener(() => {
            try {
                SceneManager.LoadScene(sceneToTransitionToName);
            } catch (Exception e) {
                Debug.LogException(e);
            }
        });
    }
}
