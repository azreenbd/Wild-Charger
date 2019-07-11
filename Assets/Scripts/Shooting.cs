using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public Rigidbody bulletPrefab;
	public Transform spawnPosition;
	public AudioClip laserSound;
	public AudioSource soundSource;
	public static bool isShooting;

	// Use this for initialization
	void Start () {
		soundSource.clip = laserSound;
		isShooting = false;
	}

	// Update is called once per frame
	void Update () {
		if (isShooting) {
			Rigidbody bulletRB;
			bulletRB = Instantiate (bulletPrefab, spawnPosition.position, spawnPosition.rotation) as Rigidbody;
			bulletRB.AddForce (spawnPosition.forward * 5000);

			soundSource.Play ();

			isShooting = false;
		}
	}


}
