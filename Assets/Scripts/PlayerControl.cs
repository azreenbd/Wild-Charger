using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	Rigidbody player;
	int health;

	//MOVEMENT
	const float maxSpeed = 30.0f;
	const float reverseSpeed = 10.0f;
	const float accelerationSpeed = 0.5f;
	private float currentSpeed;
	private bool decelerate;
	bool moving;
	public AudioClip accelerateSound;
	AudioSource soundSource;

	//GUI
	public Canvas pauseMenu;
	public Canvas HUD;
	public Canvas winScreen;
	public Canvas loseScreen;
	public Text enemyAlive;
	public Text timeText;
	public Text finishTimeText;
	public Text bestTimeText;
	public Image healthBar;

	//FOR SHOOTING
	public Rigidbody bulletPrefab;
	public Transform spawnPosition;
	public AudioClip laserSound;
	public AudioSource bulletSource;

	//Timer
	float timer;

	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody>();
		moving = false;
		currentSpeed = 0f;
		decelerate = false;
		health = 100;
		Time.timeScale = 1;
		timer = 0.0f;

		pauseMenu.enabled = false;
		winScreen.enabled = false;
		loseScreen.enabled = false;
		HUD.enabled = true;

		soundSource = GetComponent<AudioSource> ();
		soundSource.clip = accelerateSound;
		bulletSource.clip = laserSound;
	}

	void Update () {
		//HEALTH RELATED STUFF
		healthBar.fillAmount = health/100f;

		//Timer
		timer += Time.deltaTime;
		timer = (float) System.Math.Round (timer, 2);

		//PAUSE MENU
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (pauseMenu.enabled == false) {
				pauseMenu.enabled = true;
				HUD.enabled = false;
				Time.timeScale = 0;
			} else {
				pauseMenu.enabled = false;
				HUD.enabled = true;
				Time.timeScale = 1;
			}
		}

		//DRIVING
		if (Input.GetKey (KeyCode.UpArrow)) {
			currentSpeed += accelerationSpeed;
			if (currentSpeed > maxSpeed) {
				currentSpeed = maxSpeed;
			}

			transform.position += transform.forward * Time.deltaTime * currentSpeed;
			moving = true;
			decelerate = false;

		} else if (Input.GetKey (KeyCode.DownArrow) && !decelerate) {
			
			currentSpeed += accelerationSpeed;
			if (currentSpeed > reverseSpeed) {
				currentSpeed = reverseSpeed;
			}

			transform.position += transform.forward * -1 * Time.deltaTime * reverseSpeed;
			moving = true;
			decelerate = false;

		} else {
			moving = false;
		}

		//to control sound to play and loop once, not keep restart when a key is pressed
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			soundSource.Play ();
			soundSource.loop = true;
		}

		//to control deccelerate
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			decelerate = true;
			soundSource.loop = false;
		}

		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			currentSpeed = 0f;
		}

		if (decelerate) {
			currentSpeed -= accelerationSpeed;
			if (currentSpeed <= 0f) {
				currentSpeed = 0f;
				decelerate = false;
			}

			transform.position += transform.forward * Time.deltaTime * currentSpeed;
		}

		//to limit rotation only when moving
		if(Input.GetKey(KeyCode.RightArrow) && moving) {
			transform.Rotate(Vector3.up);
		}

		if(Input.GetKey(KeyCode.LeftArrow) && moving) {
			transform.Rotate(Vector3.down);
		}

		//SHOOTING
		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot();
		}

		//CHECKING HOW MANY ENEMY LEFT
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		enemyAlive.text = enemies.Length.ToString();

		//timer
		timeText.text = timer.ToString();

		//win
		if (enemies.Length <= 0) {
			if (PlayerPrefs.GetFloat (SceneManager.GetActiveScene ().name + "best") > timer || PlayerPrefs.GetFloat (SceneManager.GetActiveScene ().name + "best") <= 0.0f) {
				PlayerPrefs.SetFloat (SceneManager.GetActiveScene ().name + "best", timer);
			}

			finishTimeText.text = timer.ToString();
			bestTimeText.text = PlayerPrefs.GetFloat (SceneManager.GetActiveScene ().name + "best").ToString();

			PlayerPrefs.Save ();

			winScreen.enabled = true;
			Time.timeScale = 0;
			HUD.enabled = false;
		}

		//lose
		if(health <= 0) {
			loseScreen.enabled = true;
			Time.timeScale = 0;
			HUD.enabled = false;
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
			health -= 10;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "HealthPack") {
			health = 100;
		}
	}
}
