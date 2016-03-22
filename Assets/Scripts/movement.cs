using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed = 1.0f;
	public GameObject gameManager;
	public Sprite[] playerSprites = new Sprite[3];


	/*
	 * 0 = paikallaan
	 * 1 = vasemmalle 
	 * 2 = oikealle
	 */
	void Awake() {
		playerSprites = Resources.LoadAll<Sprite> ("bunnysheet339x128");
		// player.GetComponent<SpriteRenderer> ().sprite = playerSprites [1];
	}


	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		// pupua voi liikutella vain pelitilassa 2 (=käynnissä)
		if (gameManager.GetComponent<manager> ().gameState == 2) {
			Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			transform.position += move * speed * Time.deltaTime;

			// kosketusohjaus x-suunnassa
			if (Input.touchCount > 0) {
				Vector3 fingerPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
				transform.position = new Vector3 (fingerPos.x, transform.position.y, transform.position.z);
			}
		}//if gamestate = 2
	}


	void LateUpdate() {
		//TODO: tänne pelaajakuvan vaihtaminen kun suunta vaihtuu
	}
}
