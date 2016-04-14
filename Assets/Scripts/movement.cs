using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed = 1.0f;
	public GameObject gameManager;
	public GameManager gm;
	public Sprite[] playerSprites = new Sprite[4];
	public float passOutTime = 1f;
	private float hitConeTime = 0f;

	Vector3 oldPos;

	/*
	 * 0 = paikallaan
	 * 1 = vasemmalle 
	 * 2 = oikealle
	 * 3 = pyörryksissä
	 */
	void Awake() {
		playerSprites = Resources.LoadAll<Sprite> ("bunnysheet452x128");
		// GetComponent<SpriteRenderer> ().sprite = playerSprites [1];
	}


	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("Taso01Manager");
		gm = GameManager.instance;
		if (gm == null)
			Debug.Log ("gm on null");
		oldPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// pupua voi liikutella vain pelitilassa 2 (=käynnissä)
		if (gm.gameState == 2 && Time.time > hitConeTime + passOutTime) {
			Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			transform.position += move * speed * Time.deltaTime;

			// kosketusohjaus x-suunnassa
			if (Input.touchCount > 0) {
				Vector3 fingerPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
				transform.position = new Vector3 (fingerPos.x, transform.position.y, transform.position.z);
			}

			// vaihdetaan pupun kuvaa liikesuunnan mukaan
			if (transform.position.x > oldPos.x) {
				GetComponent<SpriteRenderer> ().sprite = playerSprites [2];
			} else if (transform.position.x < oldPos.x) {
				GetComponent<SpriteRenderer> ().sprite = playerSprites [1];
			} else if (transform.position.x == oldPos.x) {
				GetComponent<SpriteRenderer> ().sprite = playerSprites [0];
			}

			oldPos = transform.position;
		}//if gamestate = 2
	}


	void OnCollisionEnter2D(Collision2D coll) {
		// jos muna törmäsi maahan
		if (coll.gameObject.CompareTag ("pinecone")) {
			hitConeTime = Time.time;
			GetComponent<SpriteRenderer> ().sprite = playerSprites [3];
			coll.gameObject.SetActive (false);
		} else if (coll.gameObject.CompareTag ("bonuslife")) {
			// osuttiin lisäelämään
			//TODO: korjaa tämä: gameManager.GetComponent<taso01manager>().addBonusLife();
			Debug.Log("osui elämään");
			coll.gameObject.SetActive (false);
		}
	}

}
