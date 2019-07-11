using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		Destroy (gameObject);
	}
}
