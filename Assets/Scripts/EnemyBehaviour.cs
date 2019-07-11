using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {

	public Image healthBar;
	int health;
	public Rigidbody player;

	const float maxSpeed = 10.0f;
	//const float reverseSpeed = 10.0f;
	const float accelerationSpeed = 0.1f;
	private float currentSpeed;
	private bool decelerate;
	public float directionChangeTimer = 2.0f;
	float directionChangeCounter;
	int randomDirection;

	//FOR SHOOTING
	public Rigidbody bulletPrefab;
	public Transform spawnPosition;
	public AudioClip laserSound;
	public AudioSource bulletSource;
	public float shootingRate = 1.0f;
	float shootingRateCounter;

	// Use this for initialization
	void Start () {
		health = 100;
		currentSpeed = 0f;
		decelerate = false;
		bulletSource.clip = laserSound;
		shootingRateCounter = shootingRate;
		directionChangeCounter = directionChangeTimer;
		randomDirection = Random.Range (1, 4);
	}
	
	// Update is called once per frame
	void Update () {
		//HEALTH RELATED STUFF
		healthBar.fillAmount = health/100f;
		if(health <= 0) {
			Destroy (gameObject);
		}

		//ENEMY AI
		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		//to check angle between enemy and player
		float angle = Vector3.Angle (direction, this.transform.forward);

		if (Vector3.Distance (player.position, this.transform.position) < 55) {

			//move forward
			if (Vector3.Distance (player.position, this.transform.position) > 10) {
				currentSpeed += accelerationSpeed;
				if (currentSpeed > maxSpeed) {
					currentSpeed = maxSpeed;
				}

				this.transform.position += this.transform.forward * Time.deltaTime * currentSpeed;
			}

			//SHOOTING WITH TIMER
			shootingRateCounter -= Time.deltaTime;

			if (shootingRateCounter <= 0f) {
				Shoot ();
				shootingRateCounter = shootingRate;
			}

			//rotate to see player
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.05f);
		} else { //keep moving if not detecting player
			currentSpeed += accelerationSpeed;
			if (currentSpeed > maxSpeed) {
				currentSpeed = maxSpeed;
			}

			this.transform.position += this.transform.forward * Time.deltaTime * currentSpeed;

			//CHANGE DIRECTION ON TIMER
			directionChangeCounter -= Time.deltaTime;

			if (directionChangeCounter <= 0f) {
				directionChangeCounter = directionChangeTimer;
				randomDirection = Random.Range (1, 4); //random 1 to 3, 1 go right, 2 go left, 3 skip rotate and go straight.
			}

			//CHANGE DIRECTION
			if (randomDirection == 1) {
				transform.Rotate(Vector3.up); //right
			} else if (randomDirection == 2) {
				transform.Rotate(Vector3.down); //left
			}
		}
	}

	//SHOOTING function
	void Shoot() {
		Rigidbody bulletRB;
		bulletRB = Instantiate (bulletPrefab, spawnPosition.position, spawnPosition.rotation) as Rigidbody;
		bulletRB.AddForce (spawnPosition.forward * 5000);

		bulletSource.Play ();
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Bullet(Clone)") {
			health -= 20;
		}
	}
}
