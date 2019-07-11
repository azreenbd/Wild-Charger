using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour {

	public int health;

	// Use this for initialization
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Bullet(Clone)") {
			health -= 20;
		}
	}
}
