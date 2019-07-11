using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackTrigger : MonoBehaviour {
	public AudioSource healthPack;

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			healthPack.Play ();
		}
	}
}
