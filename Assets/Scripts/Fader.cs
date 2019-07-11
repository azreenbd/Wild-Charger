using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;
	private float alpha = 1.0f;
	private int drawDepth = -1000;
	private int fadeDir = -1;

	void Start() {
		Time.timeScale = 1;
	}

	void OnGUI() {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		Rect dimension = new Rect (0, 0, Screen.width, Screen.height);
		GUI.DrawTexture (dimension, fadeOutTexture);
	}

	public float BeginFade(int direction) {
		fadeDir = direction;
		return 1.0f / fadeSpeed;
	}

	void OnLevelLoaded() {
		BeginFade (-1);
	}

	IEnumerator ChangeLevel() {
		Fader fader = GameObject.Find("MainCamera").GetComponent<Fader>();
		float fadeTime = fader.BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}