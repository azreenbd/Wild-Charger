﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void Play () {
		SceneManager.LoadScene ("Level Selector");
	}
	
	public void ExitGame () {
		Application.Quit ();
	}
}
