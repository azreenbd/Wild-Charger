using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public Canvas HUD;
	public Canvas pauseMenu;

	// Use this for initialization
	void Start () {
	}

	public void Resume () {
		pauseMenu.enabled = false;
		HUD.enabled = true;
		Time.timeScale = 1;
	}

	public void MainMenu () {
		SceneManager.LoadScene ("Main Menu");
	}

	public void RestartLevel () {
		Application.LoadLevel(Application.loadedLevel);
	}
}
