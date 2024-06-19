using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	public GameObject launchPanel, mainPanel;

	public void OnLaunchPlayButton()
	{
		launchPanel.SetActive(false);
		mainPanel.SetActive(true);
	}
}
