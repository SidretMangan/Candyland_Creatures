using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverlay : MonoBehaviour
{
	public GameObject LosePanel, InfoPanel,WinPanel;

	public Button retryButton;
	public Button levelMapButton;

    private void Awake()
	{
		retryButton.onClick.AddListener(() =>
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		});
        levelMapButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelMap");
        });
    }
}