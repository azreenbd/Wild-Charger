using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void IndustrialArena () {
		SceneManager.LoadScene ("Industrial");
	}

	public void ParkArena () {
		SceneManager.LoadScene ("Park");
	}

	public void MainMenu () {
		SceneManager.LoadScene ("Main Menu");
	}
}
