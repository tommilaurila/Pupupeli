using UnityEngine;
using System.Collections;

public class movement2 : MonoBehaviour {

	public float speed = 1.0f;
	public GameObject manager;
	public Sprite[] playerSprites = new Sprite[4];
	public float passOutTime = 1f;
	private float hitLightTime = 0f;

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
		manager = GameObject.Find ("manager");
		oldPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		// pupua voi liikutella vain pelitilassa 2 (=käynnissä)
		if (manager.GetComponent<manager> ().gameState == 2 && Time.time > hitLightTime + passOutTime) {
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
		if (coll.gameObject.CompareTag ("light")) {
			hitLightTime = Time.time;
			coll.gameObject.SetActive (false);
			GetComponent<SpriteRenderer> ().sprite = playerSprites [3];
		} else if (coll.gameObject.CompareTag ("bonuslife")) {
			// osuttiin lisäelämään
			manager.GetComponent<manager> ().addBonusLife ();
			Debug.Log ("osui elämään");
			coll.gameObject.SetActive (false);
		} else if (coll.gameObject.CompareTag ("water")) {
			hitLightTime = Time.time;
			coll.gameObject.SetActive (false);
			GetComponent<SpriteRenderer> ().sprite = playerSprites [3];
		}
	}

}