using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed = 1.0f;
	public GameObject gameManager;

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
		}
	}
}
