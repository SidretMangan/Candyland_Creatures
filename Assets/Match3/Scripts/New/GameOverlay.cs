using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverlay : MonoBehaviour
{
	public GameObject LosePanel, InfoPanel;

	public Button retryButton;

	private void Awake()
	{
		retryButton.onClick.AddListener(() =>
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		});
	}
}