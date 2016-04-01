using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	public GameObject gameManager;
	public AudioClip powerup;
	public AudioClip explosion;
	AudioSource[] audios;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		audios = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D coll) {
		

		// jos muna törmäsi maahan
		if (coll.gameObject.CompareTag ("Ground")) {
			audios [1].Play ();

			gameManager.GetComponent<manager> ().decreaseLife (1);
			Debug.Log ("osui maahan");
			// disabloidaan tämä muna hetken päästä
			//Invoke("disabloiMuna", 0f);
			gameObject.SetActive(false);

			// jos muna törmäsi pelaajaan (pelaaja sai munan kiinni)
		} else if (coll.gameObject.CompareTag ("Player")) {
			
			audios [0].Play();

			gameManager.GetComponent<manager> ().addPoints (1);

			// kysytään pelaajan pistemäärä, jotta osataan laittaa
			// muna oikeaan kuppiin
			int playerPoints = gameManager.GetComponent<manager>().points;

			Vector3 bottomLeft = Camera.main.ScreenToWorldPoint (Vector3.zero);
			transform.position = new Vector3 (bottomLeft.x + playerPoints / 1.5f, bottomLeft.y + 1f, 0.2f);

			GetComponent<Rigidbody2D> ().isKinematic = true;
		}
	}


	void disabloiMuna() {
		gameObject.SetActive(false);
	}
}
